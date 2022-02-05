using System.Collections;
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

        /// <summary>
        /// 表示单位当前是否能攻击
        /// </summary>
        public bool actionState;

        /// <summary>
        /// 在单位实体创建时赋值单位属性
        /// </summary>
        public Unit unit;

        /// <summary>
        /// 元素附着列表
        /// </summary>
        private LinkedList<Element> elementAttachment = new LinkedList<Element>();

        /// <summary>
        /// Buff附着列表
        /// TODO: 改为List可能更合适（方便FindAll）
        /// </summary>
        public LinkedList<BaseBuff> buffAttachment = new LinkedList<BaseBuff>();


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

            // TODO：护盾流程
            if (damageValue >= HP)
            {
                SetFall();
                return true;
            }
            else
            {
                HP -= damageValue;
                // Debug.Log(unit.unitName + "受伤后血量为" + HP);
                return false;
            }

        }

        /// <summary>
        /// 阵亡状态设置
        /// </summary>
        public void SetFall()  // 目前只在UnitEntity.cs, BossEntity.cs, CharaEntity.cs中被调用
        {
            HP = 0;
            unit.WhenFall();
            // 解除场地占用
            BattleFieldManager.Instance.SetBucketCarryFlag(carrier.serial);
        }

        /// <summary>
        /// 用于在回合开始时把单位行动状态设置为“可进行攻击的”
        /// </summary>
        public void FreshActionState(BattleSite site)
        {
            if (ownerSite == site)
            {
                actionState = true;
            }

        }

        /// <summary>
        /// 攻击时由战斗管理器调用
        /// (如果后续固有属性支持多次攻击则调整实现)
        /// </summary>
        public void BeActed()
        {
            actionState = false;
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
        public void Init(UnitCard _unitCard, BattleSite _owner, BucketEntity _carrier)
        {

            this.ownerSite = _owner;
            this.carrier = _carrier;

            // 创建初始行动状态（后续考虑冲锋等
            actionState = false;

            elementAttachment = new LinkedList<Element>();
            buffAttachment = new LinkedList<BaseBuff>();





            // TODO：根据单位卡的类型，新增组件
            this.unit = new Unit(_unitCard);

            //if(_unitCard.cardType == CardType.charaCard)
            //{
            //    gameObject.AddComponent<CharaComponent>();
            //    gameObject.GetComponent<CharaComponent>().Init(unit as Chara);
            //}

            // 草率创建boss形式
            if (_unitCard.cardID == 401)
            {
                this.unit = new Boss(_unitCard, 1, 3, 0, 0);
                gameObject.AddComponent<BossComponent>();
                GetComponent<BossComponent>().Init(unit as Boss);
            }



        }

        public void Init(Unit _unit, BattleSite _owner, BucketEntity _carrier)
        {
            this.ownerSite = _owner;
            this.carrier = _carrier;

            // 创建初始行动状态（后续考虑冲锋等
            actionState = false;

            this.unit = _unit;
        }

    }
}