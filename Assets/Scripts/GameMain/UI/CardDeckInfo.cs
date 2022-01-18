using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
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
            cardInfo.text = "≈∆ø‚£∫\n £”‡ø®≈∆"+RemainCardNum+"’≈";
        }
    }

}
