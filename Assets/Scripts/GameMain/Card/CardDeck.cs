
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 卡组，实现单场对局中特定玩家的手牌管理
    /// </summary>
    public class CardDeck
    {
        public PlayerID owner;

        public static int S_HandCardLimit = 10; // 手牌上限

         
        /// <summary>
        /// 带上场的卡组
        /// </summary>
        public LinkedList<Card> CardLibrary = new LinkedList<Card>();

        /// <summary>
        /// 手牌
        /// </summary>
        public LinkedList<Card> HandCardList = new LinkedList<Card>();
        /// <summary>
        /// 出场角色
        /// </summary>
        public LinkedList<Card> HandCharaList = new LinkedList<Card>();
        /// <summary>
        /// 已用卡牌
        /// </summary>
        public LinkedList<Card> UsedList = new LinkedList<Card>();

        /// <summary>
        /// 由选出的卡中检查并剔除
        /// </summary>
        public void CheckAndCullCard() { 
            
        }
        /// <summary>
        /// 尽最大可能发/摸牌（！不是抽牌） charaN:发放角色数 cardN：发放卡牌数
        /// 返回值：1 牌库够；-1 牌库不够；0：手牌溢出，依然发牌，需执行CheckAndCullCard()剔除操作
        /// </summary>
        public int HandOutCard(int charaN,int cardN) {
            int ret = 1;
            if (charaN > CharaLibrary.Count) {
                charaN = CharaLibrary.Count;
                ret = -1;
            }
            if( cardN >CardLibrary.Count) {
                cardN = CardLibrary.Count;
                ret = -1;
            }
            if (HandCardList.Count + cardN > S_HandCardLimit) {
                ret = 0;
            }
            for (int i = 0; i < cardN; i++) {
                HandCardList.AddLast(CardLibrary.First.Value);
                CardLibrary.RemoveFirst();
            }
            for (int i = 0; i < charaN; i++) {
                HandCharaList.AddLast(CharaLibrary.First.Value);
                CharaLibrary.RemoveFirst();
            }
            return ret;
        }

        


        
        /// <summary>
        /// 带上场的角色
        /// </summary>
        public LinkedList<Card> CharaLibrary = new LinkedList<Card>();

        /// <summary>
        /// 通过玩家自定卡组构造，对上场玩家的牌库进行初始化
        /// </summary>
        public CardDeck (GenpaiPlayer genpaiPlayer)
        {
            //genpaiPlayer.selectedCardIDList:前几个为角色卡
            List<Card> selectedCard= CardLoader.Instance.GetCardByIds(genpaiPlayer.selectedCardIDList);
            List<Card> charaCard = new List<Card>();
            List<Card> monsterCard = new List<Card>();
            List<Card> spellCard = new List<Card>();

            //选出前面的角色卡
            for (int i = 0; i < genpaiPlayer.charanum; i++) {
                charaCard.Add(selectedCard[i]);
            }
            //选出后面的怪物卡
            for (int i = genpaiPlayer.charanum; i <genpaiPlayer.charanum+genpaiPlayer.monsternum ; i++) {
                monsterCard.Add(selectedCard[i]);
            }
            RadomSort(ref charaCard);
            RadomSort(ref monsterCard);
            /*string s = "";
            for (int i = 0; i < 4; i++)
                s += charaCard[i].cardID;
            Debug.Log(s);*/
            /*for (int i = 0; i < 30; i++)
                Debug.Log(monsterCard[i]);*/
            for (int i = 0; i < genpaiPlayer.charanum; i++)
            {
                CharaLibrary.AddLast(charaCard[i]);
            }
            for (int i = 0; i < genpaiPlayer.monsternum; i++)
            {
                CardLibrary.AddLast(monsterCard[i]);
            }
            
                
          
        }

        public static void RadomSort(ref List<Card> arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                var index = new System.Random().Next(i, arr.Count);
                var tmp = arr[i];
                var ran = arr[index];
                arr[i] = ran;
                arr[index] = tmp;
            }
            
        }

        /// <summary>
        /// 抽牌
        /// </summary>
        public void DrawCard()
        {
            // 无牌情况
            if (CardLibrary.Count == 0)
            {
                return;
            }
            Card DrawedCard = CardLibrary.First.Value;
            CardLibrary.Remove(DrawedCard);
            if (HandCardList.Count >= S_HandCardLimit)
            {
                return;
            }
            HandCardList.AddLast(DrawedCard);
        }
        /// <summary>
        /// 抽角色
        /// </summary>
        public void DrawHero()
        {
            foreach (Card chara in CharaLibrary)
            {

            }
        }

    }
}

