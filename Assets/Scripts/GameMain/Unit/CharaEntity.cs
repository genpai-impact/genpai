using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 角色实体
    /// 需要实现出场技及主动技能，以及充能特性
    /// </summary>
    public class CharaEntity : UnitEntity, IMessageSendHandler
    {
        public new Chara unit;

        /// <summary>
        /// 当前能量值
        /// </summary>
        public int MP  // MP有随时被修改的需求
        {
            get => unit.MP;
            set => unit.MP = value;
        }

        /// <summary>
        /// 覆盖UnitEntity中的Awake()
        /// </summary>
        public override void Awake()
        {
            // 待实现：从数据库获取技能、充能等信息

            Subscribe();
        }

        public override void Init(UnitCard _unitCard, GenpaiPlayer _owner, BucketEntity _carrier)
        {
            this.unit = new Chara(_unitCard, 4);
            this.owner = _owner;
            this.carrier = _carrier;

            actionState = false;
        }

        /// <summary>
        /// 实现角色死亡消息
        /// 覆盖父类SetFall
        /// </summary>
        public new void SetFall()
        {
            base.SetFall();
            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);
        }

        /// <summary>
        /// 与信息传递有关
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {

        }

        /// <summary>
        /// 覆盖父类的Subscribe方法,
        /// 比父类增加了充能的订阅
        /// </summary>
        public new void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, AddMP);  // 把充一点MP这件事情添加到新回合开始时要做的事情中
        }
        /// <summary>
        /// 充一点能量，如果满了就不充
        /// </summary>
        public void AddMP(BattleSite site)
        {
            if (site == owner.playerSite)
            {
                if (0 <= MP && MP < unit.MPMax)
                {
                    MP++;
                }
            }

        }
    }
}