using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// 单位UI展示
    /// 包括展示Buff列表
    /// </summary>
    public class UnitDisplay : MonoBehaviour
    {
        /// <summary>
        /// 待显示单位
        /// </summary>
        public UnitEntity unitEntity;

        // 待修改为模型展示（完全不了解）
        public Image unitModelImage;

        // 待展示UI内容
        // public Text unitName;
        public Text BuffInfoText;
        public Text atkText;
        public Text hpText;
        public Image atkElement;    // 攻击元素图标
        public Image CurrentEle;    // 附着元素图标



        public void Init()
        {
            unitEntity = GetComponent<UnitEntity>();
            if (unitEntity.unit != null)
            {
                DisplayUnit();
            }
        }



        public void DisplayUnit()
        {
            Unit unit = unitEntity.unit;

            // unitName.text = unit.unitName;

            atkText.text = unit.baseATK.ToString();
            hpText.text = unit.HP.ToString();

            try
            {
                string imgPath = "UnitModel/ModelImage/" + unit.unitName;

                float imageSizeScale = 0.5f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                unitModelImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                unitModelImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(unit.unitName + " 无模型");
            }

            // 设置元素
            // TODO
        }
    }
}