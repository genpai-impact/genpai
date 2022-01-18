using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// TODO
    /// 需要拿到牌库剩余卡牌，目前测试默认为10
    /// </summary>
    public class CardDeckInfo : MonoBehaviour
    {
        public int RemainCardNum;
        CardDeck cardDeck;
        Text cardInfo;
        // Start is called before the first frame update
        void Start()
        {
            cardInfo = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            //RemainCardNum = cardDeck.GetRemainCard();
            cardInfo.text = "牌库：\n剩余卡牌"+RemainCardNum+"张";
        }
    }

}
