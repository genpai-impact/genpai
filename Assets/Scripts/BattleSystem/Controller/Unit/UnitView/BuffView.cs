using BattleSystem.Service.Buff;
using UnityEngine.UI;

namespace BattleSystem.Controller.Unit.UnitView
{
    /// <summary>
    /// Buff快速视图，概括信息
    /// </summary>
    public class BuffView
    {
        public readonly string BuffName;
        public Image BuffImage;
        public readonly int LifeCycle;  // Buff生命周期
        public readonly int Storey;      // Buff层数

        public BuffView(Buff buff)
        {
            BuffName = buff.BuffName;

            LifeCycle = buff.LifeCycle;
            Storey = buff.Storey;

        }

        public string ReturnDescription()
        {
            const string ret = "九层的究↗极↘Buff在九回合后消失";
            return ret;
        }
    }
}