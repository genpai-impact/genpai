using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 卡牌显示，通过UnityEngine.UI修改卡牌模板
    /// </summary>
    public class CardDisplay : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public Card card;
        public UnitInfoDisplay UID;
        /// <summary>
        /// 基础卡牌信息
        /// </summary>
        public Text cardName;
        public Image cardImage;

        /// <summary>
        /// 单位卡信息容器显示
        /// </summary>
        public GameObject UnitCanvas;
        public Text atkText;
        public Text hpText;
        public Image atkElement;

        private bool isGary;

        /// <summary>
        /// 悬浮显示相关
        /// </summary>
        private Vector3 _ObjectScale;

        void Start()
        {
            UID = GameObject.Find("UnitInfo").GetComponent<UnitInfoDisplay>();
            _ObjectScale = gameObject.transform.localScale;
            if (card != null)
            {
                DisplayCard();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           
            UID.ReDraw_Card(this);
            Zoom();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.transform.localScale = _ObjectScale;
        }

        public void Zoom()
        {
            gameObject.transform.localScale = new Vector3(1.5f * _ObjectScale.x, 1.5f * _ObjectScale.y, 1);
            Debug.Log("放大");
        }

        public void Revert()
        {
            gameObject.transform.localScale = _ObjectScale;
        }

        public void Update()
        {
            CardColorChange();
        }

        private void CardColorChange()
        {
            if (!(card is UnitCard))
            {
                return;
            }
            var unitcard = card as UnitCard;
            Color color = Color.white;
            if (!unitcard.CanUse())
            {
                if (!unitcard.CanUse() && isGary)
                {
                    return;
                }
                color = Color.gray;
            }
            else
            {
                if (!isGary)
                {
                    return;
                }
            }
            isGary = color == Color.gray;
            Image[] images = GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                Image image = images[i];
                if (image.gameObject.name == "AtkEleImage") {
                    continue;
                }
                image.color = color;
            }
        }

        public void DisplayUnitCard(UnitCard unitcard)
        {
            atkText.text = unitcard.atk.ToString();
            hpText.text = unitcard.hp.ToString();
            UnitCanvas.gameObject.SetActive(true);
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + card.cardName;
                float imageSizeScale = 1f;
                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(card.cardName + " 无模型");
            }
        }

        public void DisplaySpellCard()
        {
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "ArtAssets/Card/魔法牌/" + card.cardName;

                float imageSizeScale = 1f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(card.cardName + " 无模型");
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

            if (card is UnitCard)
            {
                var unitcard = card as UnitCard;
                DisplayUnitCard(unitcard);
            }
            else if (card is SpellCard)
            {
                DisplaySpellCard();
            }
        }
    }
}