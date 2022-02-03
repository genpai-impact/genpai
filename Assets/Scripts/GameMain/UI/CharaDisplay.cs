using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

namespace Genpai
{
    /// <summary>
    /// 卡牌显示，通过UnityEngine.UI修改卡牌模板
    /// </summary>
    public class CharaDisplay : MonoBehaviour
    {
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public Card card;

        public BattleSite PlayerSite;

        /// <summary>
        /// 基础卡牌信息
        /// </summary>
        public Text charaName;
        public Text charaInfoText;
        public Image charaImage;

        /// <summary>
        /// 单位卡信息容器显示
        /// </summary>

        public Text atkText;
        public Text hpText;
        public Text engText;
        //public Image atkElement;

        /// <summary>
        /// 过于草率的角色召唤实现
        /// TODO：修复
        /// </summary>
        public void OnMouseDown()
        {
            // Debug.Log("Chara Mouse Down");
            // 获取角色位
            GameObject Bucket;
            if (PlayerSite == BattleSite.P1)
            {
                Bucket = BattleFieldManager.Instance.GetBucketBySerial(5);
            }
            else
            {
                Bucket = BattleFieldManager.Instance.GetBucketBySerial(12);
            }

            // 获取卡牌数据
            UnitCard summonCard = card as UnitCard;

            // 生成实际UnitEntity
            Transform UnitSeats = Bucket.transform.Find("Unit");
            GameObject unit = GameObject.Instantiate(processtest.Instance.unitPrefab, UnitSeats.transform);

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(summonCard, GameContext.Instance.GetPlayerBySite(PlayerSite), Bucket.GetComponent<BucketEntity>());
            unit.GetComponent<UnitDisplay>().Init();


            BattleFieldManager.Instance.SetBucketCarryFlag(Bucket.GetComponent<BucketUIController>().bucket.serial);
        }

        void Start()
        {
            if (card != null)
            {
                DisplayChara();
            }
        }

        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
        public void DisplayChara()
        {
            // 默认关闭数值表
            //UnitCanvas.gameObject.SetActive(false);

            // 加载卡名&描述
            charaName.text = card.cardName;
            //charaInfoText.text = card.cardInfo[0];

            if (card is UnitCard)
            {
                var unitcard = card as UnitCard;
                atkText.text = unitcard.atk.ToString();
                hpText.text = unitcard.hp.ToString();
                //UnitCanvas.gameObject.SetActive(true);

                //获取元素图片
                // atkElement.sprite

            }

            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + card.cardName;

                float imageSizeScale = 0.5f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                charaImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                charaImage.overrideSprite = sprite;
            }
            catch
            {
                //Debug.Log(card.cardName + " 无模型");
            }

        }
    }
}