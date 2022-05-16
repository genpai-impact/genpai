using Messager;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Genpai
{
    public class Unit : IDamageable, IMessageReceiveHandler
    {

        public string UnitName { get => BaseUnit.UnitName; }
        public CardType UnitType { get => BaseUnit.UnitType; }
        
        public BaseUnit BaseUnit;
        public Bucket Carrier;

        // >>> 战场性质
        public BattleSite OwnerSite => Carrier.ownerSite;
        public GenpaiPlayer Owner => Carrier.owner;


        // >>> 单位状态
        public bool IsFall;
        public Dictionary<UnitState, bool> ActionState;

        public LinkedList<Element> ElementAttachment;

        public bool IsRemote
        {
            get => false;
        }

        // >>> 单位面板
        public int Hp
        {
            get => _hp;
            // 由自身受伤函数设置
            private set
            {
                WhenSetHp(value);
                // 血量上限
                _hp = System.Math.Min(value, BaseUnit.BaseHp);
                _hp = System.Math.Max(value, 0);
            }
        }
        private int _hp;

        public int Atk
        {
            get
            {
                int value = BaseUnit.BaseAtk;
                BuffManager.Instance.AttackBuff(this,ref value);
                return value;
            }
        }
        public ElementEnum AtkElement
        {
            get
            {
                // TODO: 附魔Buff
                ElementEnum element = BaseUnit.BaseAtkElement;
                BuffManager.Instance.AttackElementBuff(this,ref element);
                return BaseUnit.BaseAtkElement;
            }
        }

        public DamageStruct GetDamage()
        {
            return new DamageStruct(Atk, AtkElement);
        }

        public Element SelfElement
        {
            get
            {
                // 自身无元素 且 存在附着
                if (BaseUnit.BaseSelfElement == ElementEnum.None && ElementAttachment.Count > 0 && !ElementAttachment.Last.Value.ElementLock)
                {
                    return ElementAttachment.Last.Value;
                }
                else
                {
                    return new Element(BaseUnit.BaseSelfElement);
                }
            }
            set => ElementAttachment.AddLast(value);
        }

        public Unit() { }
        // 单位+位置创建（刷怪用
        public Unit(BaseUnit unit, Bucket carrier)
        {
            Init(unit, carrier);
        }
        // 卡牌+位置创建（召唤用
        public Unit(UnitCard unitCard, Bucket carrier, bool init = true)
        {
            Init(new BaseUnit(unitCard), carrier, init);
        }

        /// <summary>
        /// 通过BaseUnit和Bucket创建战场单位
        /// </summary>
        public void Init(BaseUnit unit, Bucket carrier, bool init = true)
        {
            BaseUnit = unit;
            Carrier = carrier;
            _hp = BaseUnit.BaseHp;

            Init(init);
        }

        public void Init(bool init = true)
        {
            ElementAttachment = new LinkedList<Element>();

            IsFall = false;
            ActionState = new Dictionary<UnitState, bool>
            {
                {UnitState.ActiveAttack,false },
                {UnitState.CounterattackAttack,true },
                {UnitState.SkillUsing, false },
                {UnitState.ChangeChara,false }
            };

            if (Carrier != null && init)
            {
                BattleFieldManager.Instance.SetBucketCarryFlag(Carrier.serial, this);
            }
            Subscribe();
        }

        public (int, bool) TakeDamage(int damageValue)
        {
            // 按依次经过减伤Buff
            // List<BaseBuff> reduceBuffList = BuffAttachment.FindAll(buff => buff.BuffType == BuffType.DamageReduceBuff);
            // damageValue = reduceBuffList.Aggregate(damageValue, (current, reduceBuff) => ((BaseDamageReduceBuff)reduceBuff).TakeDamage(current));

            BuffManager.Instance.ReduceDamage(this, ref damageValue);

            Hp -= damageValue;

            if (Hp <= 0)
            {
                IsFall = true;
            }

            // 还得return更多东西，但我忘了要什么
            return (damageValue, IsFall);
        }

        public void Cured(int value)
        {
            Hp += value;
        }


        public void SetFall()
        {
            if (IsFall)
            {
                WhenFall();
            }
        }


        /// <summary>
        /// 用于在回合开始前阶段把单位行动状态设置为“可进行攻击的”
        /// </summary>
        public void FreshActionState(BattleSite site)
        {
            if (Carrier != null && OwnerSite == site)
            {
                ActionState[UnitState.ActiveAttack] = true;
            }
        }

        /// <summary>
        /// 攻击后设置
        /// (如果后续固有属性支持多次攻击则调整实现)
        /// </summary>
        public void Acted()
        {
            ActionState[UnitState.ActiveAttack] = false;
        }

        /// <summary>
        /// 订阅状态刷新
        /// </summary>
        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
        }

        public virtual UnitView GetView()
        {
            return new UnitView(this);
        }

        protected virtual void WhenSetHp(int newHp) { }

        protected virtual void WhenFall()
        {
            BattleFieldManager.Instance.SetBucketCarryFlag(Carrier.serial);
        }

    }
}