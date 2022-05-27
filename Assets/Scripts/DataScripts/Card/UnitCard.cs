using System;

namespace Genpai
{

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class UnitCard : Card
    {
        public readonly int Atk;
        public readonly int Hp;
        public readonly ElementEnum AtkElement;
        public readonly ElementEnum SelfElement;

        public UnitCard()
        {
        }

        public UnitCard(int id, cfg.card.CardType cardType, string cardName, string[] cardInfo, int atk, int hp,
            ElementEnum atkElement, ElementEnum selfElement) : base(id, cardType, cardName, cardInfo)
        {
            this.Atk = atk;
            this.Hp = hp;
            this.AtkElement = atkElement;
            this.SelfElement = selfElement;
        }

        public bool CanUse()
        {
            GenpaiPlayer genpaiPlayer = GameContext.GetPlayer1();
            if (genpaiPlayer.CurrentRoundMonsterCount >= GameContext.MissionConfig.RoundMonsterCount)
            {
                // 本回合已经召唤过了
                return false;
            }
            if (GameContext.CurrentPlayer != genpaiPlayer)
            {
                // 不是玩家的回合
                return false;
            }
            // todo 游戏刚开始还在加载，或者游戏结束弹出了结算菜单了
            // todo 地块满部署时，不能部署新的怪物了，怪物卡就也不能用了
            return true;
        }
    }
}
