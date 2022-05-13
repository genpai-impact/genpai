using System;

namespace Genpai
{

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class UnitCard : Card
    {
        public int atk;
        public int hp;
        public ElementEnum atkElement;
        public ElementEnum selfElement;

        public UnitCard()
        {
        }

        public UnitCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, int _hp,
            ElementEnum _atkElement, ElementEnum _selfElement) : base(_id, _cardType, _cardName, _cardInfo)
        {
            this.atk = _atk;
            this.hp = _hp;
            this.atkElement = _atkElement;
            this.selfElement = _selfElement;
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
