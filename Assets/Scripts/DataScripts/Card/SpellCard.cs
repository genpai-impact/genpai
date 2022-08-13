using System.Collections.Generic;
using BattleSystem.Service.Common;
using BattleSystem.Service.Element;
using BattleSystem.Service.Player;
using cfg.effect;

namespace DataScripts.Card
{
    public class SpellCard : Card
    {
        public ElementEnum BuffElement;
        public EffectConstructProperties baseEffect;
        public EffectConstructProperties buffedEffect;

        public SpellCard(
            int id, cfg.card.CardType cardType, string cardName, string[] cardInfo, 
            ElementEnum buffElement, List<EffectConstructProperties> effectConstructs
            ): base(id, cardType, cardName, cardInfo)
        {
            BuffElement = buffElement;
            baseEffect = effectConstructs[0];
            buffedEffect = effectConstructs[1];
        }

        public bool CanUse()
        {
            GenpaiPlayer genpaiPlayer = GameContext.GetPlayer1();
            /*if (genpaiPlayer.CurrentRoundMonsterCount >= GameContext.MissionConfig.RoundMonsterCount)
            {
                // 本回合已经召唤过了
                return false;
            }*/
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
