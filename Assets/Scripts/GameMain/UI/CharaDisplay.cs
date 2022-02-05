using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messager;
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
        public Chara chara;

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

            GameObject unit = GameContext.Instance.GetPlayerBySite(PlayerSite).Chara;
            BucketEntity Bucket = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket;

            // 暂存场上单位
            Chara tempChara = unit.GetComponent<UnitEntity>().unit as Chara;

            // 根据己方单位更新
            unit.GetComponent<UnitEntity>().Init(chara, PlayerSite, Bucket);
            unit.GetComponent<UnitDisplay>().Init();

            chara = tempChara;

            if (tempChara == null)
            {
                unit.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }

            DisplayChara();

            BattleFieldManager.Instance.SetBucketCarryFlag(Bucket.serial, unit.GetComponent<UnitEntity>());
        }

        void Start()
        {
            if (chara != null)
            {
                DisplayChara();
            }
        }

        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
        public void DisplayChara()
        {

            if (chara != null)
            {
                charaName.text = chara.unitName;
                atkText.text = chara.baseATK.ToString();
                hpText.text = chara.HP.ToString();
                engText.text = chara.MP.ToString();
            }


            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + chara.unitName;

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