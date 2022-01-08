using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Genpai
{
    public class CardDisplay : MonoBehaviour
    {
        public Card card;
        public Text cardName;
        public Text cardInfoText;
        public Image cardImage;

        // ����λ����ʾ����
        public GameObject UnitCanvas;
        public Text atkText;
        public Text hpText;
        public Image atkElement;

        // Start is called before the first frame update
        void Start()
        {
            if (card != null)
            {
                DisplayCard();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void DisplayCard()
        {
            // Ĭ�Ϲر���ֵ��
            UnitCanvas.gameObject.SetActive(false);

            // ���ؿ���&����
            cardName.text = card.cardName;
            cardInfoText.text = card.cardInfo[0];

            if (card is UnitCard)
            {
                var unitcard = card as UnitCard;
                atkText.text = unitcard.atk.ToString();
                hpText.text = unitcard.hp.ToString();
                UnitCanvas.gameObject.SetActive(true);

                //��ȡԪ��ͼƬ
                // atkElement.sprite

            }
            else if (card is SpellCard)
            {

            }

            try
            {
                // ʹ��Resources.Load��������ȡResources�ļ�����ģ��
                // Ŀǰʹ�ÿ���ֱ�Ӷ�ȡ����������Դ��ʽ
                // TODO
                string imgPath = "UnitModel/ModelImage/" + card.cardName;

                float imageSizeScale = 0.5f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(card.cardName + " ��ģ��");
            }

        }
    }
}