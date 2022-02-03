using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 单位实体mono脚本
    /// </summary>
    public class UnitEntity : MonoBehaviour, IDamageable, IMessageReceiveHandler
    {
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
        private LinkedList<Element> elementAttachment;

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
                if (unit.selfElement == ElementEnum.None && elementAttachment.Count > 0)
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
        public void FreshActionState(bool _none)
        {
            actionState = true;
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
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init(UnitCard _unitCard, GenpaiPlayer _owner, BucketEntity _carrier)
        {
            this.unit = new Unit(_unitCard);
            this.owner = _owner;

            this.carrier = _carrier;

            // 创建初始行动状态（后续考虑冲锋等
            actionState = false;
        }



    }
}