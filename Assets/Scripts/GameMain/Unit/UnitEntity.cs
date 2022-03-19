﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using System.Linq;

namespace Genpai
{
    public enum UnitType
    {
        Monster,    // 怪物，基准单位
        Chara,      // 角色，特殊单位
        Boss        // Boss，特殊单位
    }

    public enum UnitState
    {
        ActiveAttack,       // 主动攻击
        CounterattackAttack,// 反击
        SkillUsing,         // 使用技能
        ChangeChara,        // 更换角色
    }

    /// <summary>
    /// 单位实体mono脚本
    /// </summary>
    public class UnitEntity : MonoBehaviour, IDamageable, IMessageReceiveHandler
    {
        public Animator animator;
        public UnitType unitType;

        public BattleSite ownerSite;
        public GenpaiPlayer owner
        {
            get
            {
                return GameContext.Instance.GetPlayerBySite(ownerSite);
            }
        }
        public BucketEntity carrier;

        public bool isFall;

        /// <summary>
        /// 表示单位状态
        /// </summary>
        public Dictionary<UnitState, bool> ActionState;

        /// <summary>
        /// 在单位实体创建时赋值单位属性
        /// </summary>
        public Unit unit;

        /// <summary>
        /// Buff附着列表
        /// </summary>
        public List<BaseBuff> buffAttachment = new List<BaseBuff>();

        /// <summary>
        /// 元素附着列表
        /// </summary>
        private LinkedList<Element> elementAttachment = new LinkedList<Element>();

        /// <summary>
        /// 元素附着
        /// </summary>
        public Element ElementAttachment
        {
            set
            {
                elementAttachment.AddLast(value);
            }
            get
            {
                if (unit == null)
                {
                    return new Element(ElementEnum.None);
                }
                // 自身无元素 且 存在附着
                if (unit.selfElement == ElementEnum.None && elementAttachment.Count > 0 && !elementAttachment.Last.Value.ElementLock)
                {
                    return elementAttachment.Last.Value;
                }
                else
                {
                    return new Element(unit.selfElement);
                }
            }
        }

        /// <summary>
        /// 血量
        /// </summary>
        public int HP
        {
            get => unit.HP;
            set
            {
                unit.HP = System.Math.Min(value, unit.HPMax);
            }
        }

        /// <summary>
        /// 攻击力
        /// </summary>
        public int ATK
        {
            get
            {
                // 获取攻击Buff
                return unit.baseATK;
            }
        }

        /// <summary>
        /// 攻击元素
        /// </summary>
        public ElementEnum ATKElement
        {
            get
            {
                // 获取附魔Buff
                return unit.baseATKElement;
            }
        }

        /// <summary>
        /// 获取单位造成的伤害结构体
        /// </summary>
        /// <returns>单位造成伤害结构</returns>
        public DamageStruct GetDamage()
        {
            return new DamageStruct(ATK, ATKElement);
        }

        /// <summary>
        /// 是否远程攻击单位
        /// </summary>
        /// <returns></returns>
        public bool IsRemote()
        {
            // 查找固有属性
            return false;
        }

        public virtual void Awake()
        {
            Subscribe();
        }

        /// <summary>
        /// 受伤函数
        /// （待定：恢复是否使用此方法）
        /// </summary>
        /// <param name="damageValue"></param>
        /// <returns></returns>
        public bool TakeDamage(int damageValue)
        {
            List<BaseBuff> ReduceBuffList = buffAttachment.FindAll(buff => buff.buffType == BuffType.DamageReduceBuff);

            // 按依次经过减伤Buff
            // TODO：护盾护甲优先级如何（考虑护盾无条件扣，那就省事了）
            foreach (var reduceBuff in ReduceBuffList)
            {
                damageValue = (reduceBuff as BaseDamageReduceBuff).TakeDamage(damageValue);
            }

            if (damageValue > 0)
            {
                // 播放受击动画
                GetComponent<UnitDisplay>().InjuredAnimation();
            }

            // Boss受伤计分消息
            if (ownerSite == BattleSite.Boss)
            {
                MessageManager.Instance.Dispatch(
                    MessageArea.Context,
                    MessageEvent.ContextEvent.BossScoring,
                    new BossScoringData(GameContext.CurrentPlayer.playerSite, damageValue));
            }

            Debug.Log(unit.unitName + "受到" + damageValue + "点伤害");

            HP -= damageValue;
            if (HP <= 0)
            {
                SetFallWaiting();
            }

            GetComponent<UnitDisplay>().FreshUnitUI();
            return isFall;
        }

        public void Cured(int cureValue)
        {
            HP += cureValue;
            GetComponent<UnitDisplay>().FreshUnitUI();
        }

        /// <summary>
        /// 阵亡状态设置
        /// </summary>
        public void SetFallWaiting()  // 目前只在UnitEntity.cs, BossEntity.cs, CharaEntity.cs中被调用
        {
            HP = 0;
            // 设置死亡变量
            isFall = true;
        }

        public void SetFall()
        {
            if (isFall)
            {
                // 播放死亡消融动画
                gameObject.SetActive(false);
                // TODO：Delay

                // 解除场地占用
                BattleFieldManager.Instance.SetBucketCarryFlag(carrier.serial);

                unit.WhenFall(ownerSite);
                if (unitType != UnitType.Chara)
                {
                    unit = null;
                }
            }
        }

        /// <summary>
        /// 用于在回合开始时把单位行动状态设置为“可进行攻击的”
        /// </summary>
        public void FreshActionState(BattleSite site)
        {
            if (unit != null && !isFall && ownerSite == site)
            {
                ActionState[UnitState.ActiveAttack] = true;
            }
        }

        /// <summary>
        /// 攻击时由战斗管理器调用
        /// (如果后续固有属性支持多次攻击则调整实现)
        /// </summary>
        public void Acted()
        {
            ActionState[UnitState.ActiveAttack] = false;
        }

        /// <summary>
        /// 订阅回合开始事件, 若新回合开始，则调用FreshActionState
        /// </summary>
        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init(UnitCard unitCard, BattleSite owner, BucketEntity carrier)
        {
            ownerSite = owner;
            isFall = false;
            this.carrier = carrier;
            unitType = EnumUtil.CardTypeToUnitType(unitCard.cardType);

            // 创建初始行动状态（后续考虑冲锋等
            //actionState = false;
            //初始化字典
            ActionState = new Dictionary<UnitState, bool>
            {
                {UnitState.ActiveAttack,false },
                {UnitState.CounterattackAttack,true },
                {UnitState.SkillUsing, false },
                {UnitState.ChangeChara,false }
            };

            elementAttachment = new LinkedList<Element>();
            buffAttachment = new List<BaseBuff>();
            GenerateUnitByCard(unitCard);
        }

        private void GenerateUnitByCard(UnitCard unitCard)
        {
            switch (unitCard.cardType)
            {
                case CardType.monsterCard:
                    unit = new Unit(unitCard);
                    break;
                case CardType.charaCard:
                    unit = new Chara(unitCard, Chara.DefaultMP);
                    AddCharaCompment(ownerSite);
                    break;
                case CardType.bossCard:
                    unit = new Boss(unitCard, 1, 3, 0, 0);
                    gameObject.AddComponent<BossComponent>();
                    GameContext.BossComponent = GetComponent<BossComponent>();
                    GameContext.BossComponent.Init(unit as Boss);
                    ActionState[UnitState.SkillUsing] = true;
                    break;
                default:
                    throw new System.Exception("错误的卡牌类型");
            }
        }

        public void AddCharaCompment(BattleSite owner)
        {
            CharaComponent charaComponent = gameObject.GetComponent<CharaComponent>();
            if (charaComponent == null)
            {
                gameObject.AddComponent<CharaComponent>();
                charaComponent = gameObject.GetComponent<CharaComponent>();
            }
            GenpaiPlayer genpaiPlayer = GameContext.Instance.GetPlayerBySite(owner);
            genpaiPlayer.CharaComponent = charaComponent;
            genpaiPlayer.CharaComponent.Init(unit as Chara);
        }

        /// <summary>
        /// 更换单位形式Init
        /// </summary>
        /// <param name="_unit"></param>
        /// <param name="_owner"></param>
        /// <param name="_carrier"></param>
        public void Init(Unit _unit, BattleSite _owner, BucketEntity _carrier)
        {
            ownerSite = _owner;
            carrier = _carrier;
            isFall = false;
            ActionState = new Dictionary<UnitState, bool>
            {
                {UnitState.ActiveAttack,false },
                {UnitState.CounterattackAttack,true },
                {UnitState.SkillUsing, false },
                {UnitState.ChangeChara,false }
            };

            elementAttachment = new LinkedList<Element>();
            buffAttachment = new List<BaseBuff>();
            unitType = _unit.unitType;
            unit = _unit;
        }
    }
}