using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class NewChara : NewUnit
    {
        /// <summary>
        /// ����ֵ����
        /// </summary>
        public readonly int MPMax;
        public ISkill Warfare;
        public ISkill Erupt;

        public readonly static int DefaultMP = 4;

        /// <summary>
        /// ��ǰ����ֵ
        /// </summary>
        public int MP;  // ��Ҫ��MP��ӵ�UnitCard����Ϊ��ɫ���Ļ������ԣ�����Ҫ��д���ٵط�����ʱ��û����


        public NewChara(UnitCard _unitCard, NewBucket _carrier) : base(_unitCard, _carrier)
        {
            CharaCard charaCard = _unitCard as CharaCard;
            this.MPMax = 4;
            this.MP = 0;
            this.Warfare = charaCard.Warfare;
            this.Erupt = charaCard.Erupt;
        }

        public override void WhenFall()
        {
            HandCharaManager handCharaManager = ownerSite == BattleSite.P1 ? GameContext.Player1.HandCharaManager : GameContext.Player2.HandCharaManager;

            if (handCharaManager.Count() == 0)
            {
                // ���ʧ��
                if (ownerSite == BattleSite.P1)
                {
                    // ��Ϸ����
                    return;
                }
                return;
            }
            handCharaManager.Summon(true);
        }
    }
}