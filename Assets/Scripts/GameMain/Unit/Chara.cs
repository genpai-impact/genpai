using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 存放角色信息，比Unit类新增了<能量值上限>和<当前能量值>两个属性
    /// </summary>
    public class Chara : Unit
    {
        /// <summary>
        /// 能量值上限
        /// </summary>
        public readonly int MPMax;

        /// <summary>
        /// 当前能量值
        /// </summary>
        public int MP;  // 需要把MP添加到UnitCard中作为角色卡的基础属性，但需要重写不少地方，暂时还没有做

        public Chara(UnitCard unitCard, int _MPMax) : base(unitCard)
        {
            this.MPMax = _MPMax;
            this.MP = 0;  // 策划说：游戏开始时，角色的MP应该是空的，设MP的默认值为0吧
        }

        public override void WhenFall()
        {
            base.WhenFall();
            Debug.Log("Chara Fall");
            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);
            NormalProcessManager.Instance.End();
        }
    }
}
