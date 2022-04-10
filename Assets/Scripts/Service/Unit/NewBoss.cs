using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class NewBoss : NewUnit
    {

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

        public NewBoss(UnitCard _unitCard, NewBucket _carrier) : base(_unitCard, _carrier)
        {
            this.MPMax_1 = 1;
            this.MPMax_2 = 3;
            this.MP_1 = 0;
            this.MP_2 = 0;
            Debug.Log("Boss Created");
        }

        public override void WhenSetHP(int _newHP)
        {
            MessageManager.Instance.Dispatch(
                    MessageArea.Context,
                    MessageEvent.ContextEvent.BossScoring,
                    new BossScoringData(GameContext.CurrentPlayer.playerSite, HP - _newHP));
            if (HP > 0.75 * unit.baseHP && _newHP <= 0.75 * unit.baseHP)
            {
                MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach75, true);
                GameContext.Player1.HandOutChara(1);
                GameContext.Player2.HandOutChara(1);
            }
            if (HP > 0.5 * unit.baseHP && _newHP <= 0.5 * unit.baseHP)
            {
                MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach50, true);
                GameContext.Player1.HandOutChara(1);
                GameContext.Player2.HandOutChara(1);
            }
        }

        public override void WhenFall()
        {
            MessageManager.Instance.Dispatch(
                    MessageArea.Context,
                    MessageEvent.ContextEvent.BossScoring,
                    new BossScoringData(GameContext.CurrentPlayer.playerSite, 5));

            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);

        }
    }
}