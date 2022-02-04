using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{

    public class Boss : Unit
    {
        public new int HP
        {
            get => HP;
            set
            {

                if (HP > 0.75 * HPMax && value <= 0.75 * HPMax)
                {
                    MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach75, true);
                }
                if (HP > 0.5 * HPMax && value <= 0.5 * HPMax)
                {
                    MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach50, true);
                }
                HP = value;
            }
        }

        public new int baseATK
        {
            get => 0;
        }

        /// <summary>
        /// 技能1的MP上限
        /// </summary>
        public readonly int MPMax_1;

        /// <summary>
        /// 技能2的MP上限
        /// </summary>
        public readonly int MPMax_2;

        /// <summary>
        /// 技能1的MP
        /// </summary>
        public int MP_1;

        /// <summary>
        /// 技能2的MP
        /// </summary>
        public int MP_2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitCard"></param>
        /// <param name="_MPMax_1">技能1的MP上限</param>
        /// <param name="_MPMax_2">技能2的MP上限</param>
        /// <param name="_MPInit_1">技能1的初始MP</param>
        /// <param name="_MPInit_2">技能2的初始MP</param>
        public Boss(UnitCard unitCard, int _MPMax_1, int _MPMax_2, int _MPInit_1, int _MPInit_2) : base(unitCard)
        {
            this.MPMax_1 = _MPMax_1;
            this.MPMax_2 = _MPMax_2;
            this.MP_1 = _MPInit_1;
            this.MP_2 = _MPInit_2;
        }

        public override void WhenFall()
        {
            base.WhenFall();
            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);
        }
    }
}
