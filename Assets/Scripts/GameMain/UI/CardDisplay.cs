using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Genpai
{
    /// <summary>
    /// 卡牌显示，通过UnityEngine.UI修改卡牌模板
    /// </summary>
    public class CardDisplay : MonoBehaviour
    {
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public Card card;

        /// <summary>
        /// 基础卡牌信息
        /// </summary>
        public Text cardName;
        public Text cardInfoText;
        public Image cardImage;

        /// <summary>
        /// 单位卡信息容器显示
        /// </summary>
        public GameObject UnitCanvas;
        public Text atkText;
        public Text hpText;
        public Image atkElement;

        void Start()
        {
            if (card != null)
            {
                DisplayCard();
            }
        }

        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
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