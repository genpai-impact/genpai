using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Genpai
{
    /// <summary>
    /// 卡牌显示，通过UnityEngine.UI修改卡牌模板
    /// </summary>
    public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public Card Card;
        [FormerlySerializedAs("UID")] public UnitInfoDisplay uid;
        /// <summary>
        /// 基础卡牌信息
        /// </summary>
        public Text cardName;
        public Image cardImage;

        /// <summary>
        /// 单位卡信息容器显示
        /// </summary>
        [FormerlySerializedAs("UnitCanvas")] public GameObject unitCanvas;
        public Text atkText;
        public Text hpText;
        public Image atkElement;
        private bool _canShow = false;
        private bool _isGary;

        /// <summary>
        /// 悬浮显示相关
        /// </summary>
        private Vector3 _objectScale;

        void Start()
        {
            uid = GameObject.Find("UnitInfo").GetComponent<UnitInfoDisplay>();
            _objectScale = gameObject.transform.localScale;
            if (Card != null)
            {
                DisplayCard();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _canShow = true;

            Zoom();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _canShow = false;
            gameObject.transform.localScale = _objectScale;
        }

        public void Zoom()
        {
            gameObject.transform.localScale = new Vector3(1.5f * _objectScale.x, 1.5f * _objectScale.y, 1);
            //            Debug.Log("放大");
        }

        public void Revert()
        {
            gameObject.transform.localScale = _objectScale;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(1) && _canShow)
            {
                uid.ReDraw_Card(this);
            }
            CardColorChange();
        }

        private void CardColorChange()
        {
            if (!(Card is UnitCard))
            {
                return;
            }
            Color color = Color.white;
            
            if (Card is UnitCard unitCard && !unitCard.CanUse())
            {
                if (!unitCard.CanUse() && _isGary)
                {
                    return;
                }
                color = Color.gray;
            }
            else
            {
                if (!_isGary)
                {
                    return;
                }
            }
            _isGary = color == Color.gray;
            Image[] images = GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                if (image.gameObject.name == "AtkEleImage")
                {
                    continue;
                }
                image.color = color;
            }
        }

        public void DisplayUnitCard(UnitCard unitCard)
        {
            atkText.text = unitCard.Atk.ToString();
            hpText.text = unitCard.Hp.ToString();
            unitCanvas.gameObject.SetActive(true);
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + Card.CardName;
                const float imageSizeScale = 1f;
                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(Card.CardName + " 无模型");
            }
        }

        public void DisplaySpellCard()
        {
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "ArtAssets/Card/魔法牌/" + Card.CardName;

                const float imageSizeScale = 1f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(Card.CardName + " 无模型");
            }
        }

        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
        public void DisplayCard()
        {
            // 默认关闭数值表
            unitCanvas.gameObject.SetActive(false);

            // 加载卡名&描述
            cardName.text = Card.CardName;

            switch (Card)
            {
                case UnitCard unitCard:
                    DisplayUnitCard(unitCard);
                    break;
                case OldSpellCard _:
                case SpellCard _:
                    DisplaySpellCard();
                    break;
            }
        }
    }
}