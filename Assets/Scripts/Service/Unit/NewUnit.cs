using Messager;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{
    public class NewUnit : IDamageable, IMessageReceiveHandler
    {

        public string unitName { get => unit.unitName; }
        public UnitType unitType { get => unit.unitType; }

        private NewBaseUnit unit;
        public NewBucket carrier;

        // >>> 战场性质
        public BattleSite ownerSite { get => carrier.ownerSite; }
        public GenpaiPlayer owner { get => carrier.owner; }


        // >>> 单位状态
        public bool available = false;
        public bool isFall;
        public Dictionary<UnitState, bool> ActionState;

        public List<BaseBuff> buffAttachment;
        private LinkedList<Element> elementAttachment;

        // >>> 单位面板
        public int HP
        {
            get => HP;
            // 由自身受伤函数设置
            private set
            {
                // 血量上限
                HP = System.Math.Min(value, unit.baseHP);
            }
        }

        public int ATK
        {
            get
            {
                int value = unit.baseATK;
                List<BaseBuff> AtkBuffList = buffAttachment.FindAll(buff => buff.buffType == BuffType.ATKEnhanceBuff);

                if (AtkBuffList == null)
                {
                    return value;
                }

                foreach (var buff in AtkBuffList)
                {
                    BaseATKEnhanceBuff atkBuff = buff as BaseATKEnhanceBuff;
                    if (atkBuff.trigger == true)
                        value += atkBuff.Storey;
                }

                return value;
            }
        }
        public ElementEnum ATKElement
        {
            get
            {
                // TODO: 附魔Buff
                return unit.baseATKElement;
            }
        }

        public DamageStruct GetDamage()
        {
            return new DamageStruct(ATK, ATKElement);
        }

        public Element SelfElement
        {
            get
            {
                // 自身无元素 且 存在附着
                if (unit.baseSelfElement == ElementEnum.None && elementAttachment.Count > 0 && !elementAttachment.Last.Value.ElementLock)
                {
                    return elementAttachment.Last.Value;
                }
                else
                {
                    return new Element(unit.baseSelfElement);
                }
            }
            set
            {
                elementAttachment.AddLast(value);
            }
        }

        public NewUnit() { }
        // 单位+位置创建（刷怪用
        public NewUnit(NewBaseUnit _unit, NewBucket _carrier)
        {
            Init(_unit, _carrier);
        }
        // 卡牌+位置创建（召唤用
        public NewUnit(UnitCard _unitCard, NewBucket _carrier)
        {
            Init(new NewBaseUnit(_unitCard), _carrier);
        }

        /// <summary>
        /// 通过BaseUnit和Bucket创建战场单位
        /// </summary>
        public virtual void Init(NewBaseUnit _unit, NewBucket _carrier)
        {
            unit = _unit;
            carrier = _carrier;

            buffAttachment = new List<BaseBuff>();
            elementAttachment = new LinkedList<Element>();

            available = true;
            isFall = false;
            ActionState = new Dictionary<UnitState, bool>
            {
                {UnitState.ActiveAttack,false },
                {UnitState.CounterattackAttack,true },
                {UnitState.SkillUsing, false },
                {UnitState.ChangeChara,false }
            };

            NewBattleFieldManager.Instance.SetBucketCarryFlag(carrier.serial, this);

            Subscribe();
        }

        /// <summary>
        /// 用于在回合开始前阶段把单位行动状态设置为“可进行攻击的”
        /// </summary>
        public void FreshActionState(BattleSite site)
        {
            if (ownerSite == site)
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
    }
}