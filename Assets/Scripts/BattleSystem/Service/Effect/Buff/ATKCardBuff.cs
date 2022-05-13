//using UnityEngine;
//using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using System.Linq;

namespace Genpai
{
    /// <summary>
    /// 由Atk魔法卡施加的AtkBuff
    /// </summary>
    public class ATKCardBuff : BaseAtkEnhanceBuff, IBuffDeleteable, IMessageReceiveHandler
    {
        /// <summary>
        /// 生命周期
        /// </summary>
        public int LifeCycle;

        public ATKCardBuff(int _storey = 1, int _life = 1)
        {
            BuffName = BuffEnum.AtkBuff;
            BuffType = BuffType.ATKEnhanceBuff;
            Storey = _storey;
            LifeCycle = _life;
        }

        public override void AddBuff(Unit _target)
        {
            base.AddBuff(_target);
            Subscribe();
            //_target.GetComponent<UnitDisplay>().FreshUnitUI();
        }

        /// <summary>
        /// 用于玩家每次回合结束时，生命周期-1。若生命周期已归零，则移除buff
        /// </summary>
        /// <param name="site"></param>
        public void CheckRemoval(BattleSite site)
        {
            Debug.Log("ATKCardBuff.CheckRemoval");
            LifeCycle--;
            if (Target.OwnerSite == site && LifeCycle <= 0)
            {
                DeleteBuff();
            }
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            Trigger = false;
            Target.BuffAttachment.Remove(this);
            //target.GetComponent<UnitDisplay>().FreshUnitUI();
        }

        public void Subscribe()
        {
            // 订阅回合结束，检测销毁
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundEnd, CheckRemoval);
        }
    }
}
