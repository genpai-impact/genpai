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
        public UnitType unitType;

        public GenpaiPlayer owner;  // 单位所有者
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
        /// </summary>
        public LinkedList<Buff> buffAttachment = new LinkedList<Buff>();


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
                Debug.Log(unit.unitName + "受伤后血量为" + HP);
                return false;
            }

        }

        /// <summary>
        /// 阵亡状态设置
        /// </summary>
        public void SetFall()  // 目前只在UnitEntity.cs, BossEntity.cs, CharaEntity.cs中被调用
        {
            HP = 0;
            // 解除场地占用
            BattleFieldManager.Instance.SetBucketCarryFlag(carrier.serial, false);
        }

        /// <summary>
        /// 用于在回合开始时把单位行动状态设置为“可进行攻击的”
        /// </summary>
        public void FreshActionState(BattleSite site)
        {
            if (owner == null)
            {
                if (site == BattleSite.Boss)
                {
                    actionState = true;
                }

            }
            else if (site == owner.playerSite)
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

            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, Burned);
            
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundEnd, RemoveBuff);
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        public virtual void Init(UnitCard _unitCard, GenpaiPlayer _owner, BucketEntity _carrier)
        {
            this.unit = new Unit(_unitCard);
            this.owner = _owner;

            this.carrier = _carrier;

            // 创建初始行动状态（后续考虑冲锋等
            actionState = false;
        }


        /// <summary>
        /// //回合开始引燃效果
        /// </summary>
        /// <param name="_none"></param>
        public void Burned(bool _none)
        {
            Buff index = this.buffAttachment.FirstOrDefault(buff => buff.BuffType == BuffEnum.Burning);
            if (!index.Equals(null))
            {
                //引燃伤害未确认，暂定为1
                EffectManager.Instance.InsertTimeStep(new List<IEffect> { new Damage(null, this, new DamageStruct(1, ElementEnum.Pyro)) });
            }
        }

        /// <summary>
        /// //回合结束去除感电冻结效果并添加附着
        /// </summary>
        /// <param name="_none"></param>
        public void RemoveBuff(bool _none)
        {
            Buff indexEle = this.buffAttachment.FirstOrDefault(buff => buff.BuffType == BuffEnum.ElectroCharge);
            if(!indexEle.Equals(null))
            {
                this.buffAttachment.Remove(indexEle);
                this.ElementAttachment = new Element(ElementEnum.Electro);
            }
            Buff indexFre = this.buffAttachment.FirstOrDefault(buff => buff.BuffType == BuffEnum.Freeze);
            if(!indexFre.Equals(null))
            {
                this.buffAttachment.Remove(indexFre);
                this.ElementAttachment = new Element(ElementEnum.Cryo);
            }
        }

    }
}