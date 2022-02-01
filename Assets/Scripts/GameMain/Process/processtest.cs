using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Genpai
{
    class processtest : MonoSingleton<processtest>
    {
        public GameObject cardPrefab;
        public GameObject charaPrefab;

        public GameObject cardPool;
        public GameObject cardHeap;
        public GameObject charaPool;

       
        public GameObject card2Pool;
        public GameObject card2Heap;
        public GameObject chara2Pool;



        private void Awake()
        {

        }
        private void Start()
        {
            //ProcessGameStart.GetInstance().SHYXtest();

            //2s 后抽卡
            //StartCoroutine("抽卡");


        }
        private void Update()
        {

        }
        private IEnumerator 抽卡()
        {
            yield return new WaitForSeconds(2);
            GameContext.Player1.HandOutCard(2);
            {//测试,建议折叠
                CardDeck cardDeck = GameContext.Player1.CardDeck;
                Debug.Log("----摸牌------");



                string cardBrief = "手上的牌：\n";
                var temp1 = cardDeck.HandCardList.First;
                do
                {
                    cardBrief += temp1.Value.cardID + "  " + temp1.Value.cardName + "\n";
                    temp1 = temp1.Next;
                } while (temp1 != null);
                Debug.Log(cardBrief);
            }
        }
    }
}
