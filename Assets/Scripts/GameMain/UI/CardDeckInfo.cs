using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// TODO
    /// ��Ҫ�õ��ƿ�ʣ�࿨�ƣ�Ŀǰ����Ĭ��Ϊ10
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
            cardInfo.text = "�ƿ⣺\nʣ�࿨��"+RemainCardNum+"��";
        }
    }

}
