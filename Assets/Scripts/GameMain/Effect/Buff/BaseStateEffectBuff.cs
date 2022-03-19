using Messager;

namespace Genpai
{
    /// <summary>
    /// 状态影响类Buff
    /// </summary>
    public abstract class BaseStateEffectBuff : BaseBuff, IMessageReceiveHandler
    {
        // 生命周期
        public int LifeCycles;

        /// <summary>
        /// 具体影响附着单位ActionState
        /// 参数方便快捷开关
        /// </summary>
        public virtual void EffectState(bool force = false)
        {
        }

        /// <summary>
        /// 己方回合开始时生效
        /// </summary>
        public void Effect(BattleSite site)
        {
            if (trigger && target.ownerSite == site)
            {
                EffectState();
                LifeCycles--;
            }
        }

        /// <summary>
        /// 己方回合结束后判断是否移除buff
        /// </summary>
        public virtual void CheckRemoval(BattleSite site)
        {
        }

        /// <summary>
        /// 订阅生命周期刷新时间or销毁时间
        /// 注：所有判定需要加Trigger，以防在buff注销后持续生效
        /// </summary>
        public virtual void Subscribe()
        {
            // 设置玩家行动前生效
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRound, Effect);
            // 设置回合结束时检测销毁
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundEnd, CheckRemoval);
        }
    }
}
