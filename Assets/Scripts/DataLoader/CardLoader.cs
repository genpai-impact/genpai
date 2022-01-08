using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    public class CardLoader : MonoBehaviour
    {
        public List<Card> CardList = new List<Card>();
        public TextAsset cardData; // ��������Json

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadCard()
        {
            JArray cardArray = JArray.Parse(cardData.text);
            foreach (var item in cardArray)
            {

                JObject card = (JObject)item;

                // ��ȡ����������Ϣ
                int id = int.Parse(card["cardID"].ToString());
                string cardName = card["cardName_ZH"].ToString();
                string cardType = card["cardType"].ToString();

                JArray infoArray = (JArray)card["cardInfo"];
                string[] cardInfo = infoArray.ToObject<List<string>>().ToArray();

                // ��ȡ���⿨����Ϣ
                if (card["cardType"].ToString() == "charaCard" | card["cardType"].ToString() == "monsterCard")
                {
                    JObject unitInfo = (JObject)card["unitInfo"];

                    // ���õ�λ����
                    int HP = int.Parse(unitInfo["HP"].ToString());
                    int ATK = int.Parse(unitInfo["ATK"].ToString());

                    ElementEnum ATKElement = ElementEnum.None;
                    ElementEnum selfElement = ElementEnum.None;
                    try
                    {
                        ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), unitInfo["ATKElement"].ToString());
                        selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), unitInfo["selfElement"].ToString());
                    }
                    catch
                    {
                        Debug.Log(cardName + "��������Ԫ����������");
                    }

                    if (card["cardType"].ToString() == "monsterCard")
                    {
                        // ��ɫ�����￨����δ����
                        CardList.Add(new UnitCard(id, cardType, cardName, cardInfo, ATK, HP, ATKElement, selfElement));
                    }
                    else
                    {
                        // ��ɫ���ܷ�֧δ����
                        CardList.Add(new UnitCard(id, cardType, cardName, cardInfo, ATK, HP, ATKElement, selfElement));

                    }

                    Debug.Log(cardName + " ����¼");
                }
            }
        }
    }
}