﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class Chara : Unit
    {
        /// <summary>
        /// 能量值上限
        /// </summary>
        public readonly int MPMax;
        public ISkill Warfare;
        public ISkill Erupt;

        public readonly static int DefaultMP = 4;

        /// <summary>
        /// 当前能量值
        /// </summary>
        public int MP;  // 需要把MP添加到UnitCard中作为角色卡的基础属性，但需要重写不少地方，暂时还没有做


        public Chara(UnitCard _unitCard, Bucket _carrier) : base(_unitCard, _carrier)
        {
            CharaCard charaCard = _unitCard as CharaCard;
            this.MPMax = 4;
            this.MP = 0;
            this.Warfare = charaCard.Warfare;
            this.Erupt = charaCard.Erupt;
        }

        public override UnitView GetView()
        {
            UnitView view = new UnitView(this);
            view.MP = MP;
            return view;
        }

        public override void WhenFall()
        {
            CharaManager handCharaManager = ownerSite == BattleSite.P1 ? GameContext.Player1.CharaManager : GameContext.Player2.CharaManager;

            if (handCharaManager.Count() == 0)
            {
                // 玩家失败
                if (ownerSite == BattleSite.P1)
                {
                    // 游戏结束
                    return;
                }
                return;
            }
            handCharaManager.Summon(true);
        }

        public void AddMP()
        {
            if (0 <= MP && MP < MPMax)
            {
                MP++;
            }
        }
    }
}