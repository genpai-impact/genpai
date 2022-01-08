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

        // 仅单位卡显示容器
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
            // 默认关闭数值表
            UnitCanvas.gameObject.SetActive(false);

            // 加载卡名&描述
            cardName.text = card.cardName;
            cardInfoText.text = card.cardInfo[0];

            if (card is UnitCard)
            {
                var unitcard = card as UnitCard;
                atkText.text = unitcard.atk.ToString();
                hpText.text = unitcard.hp.ToString();
                UnitCanvas.gameObject.SetActive(true);

                //获取元素图片
                // atkElement.sprite

            }
            else if (card is SpellCard)
            {

            }

            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + card.cardName;

                float imageSizeScale = 0.5f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(card.cardName + " 无模型");
            }

        }
    }
}