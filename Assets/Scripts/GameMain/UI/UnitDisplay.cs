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

        // 待展示UI内容
        // public Text unitName;
        public Text BuffInfoText;
        public Text atkText;
        public Text hpText;
        public Text EngText;
        public Image atkElement;    // 攻击元素图标
        public Image CurrentEle;    // 附着元素图标

        public GameObject EngCanvas;

        public GameObject UnitModel;



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

            // 如果为角色，打开能量框
            if (unitEntity.unitType == UnitType.Chara)
            {
                EngCanvas.SetActive(true);
                EngText.text = GetComponent<CharaComponent>().MP.ToString();
            }

            try
            {
                string imgPath = "UnitModel/ModelImage/" + unit.unitName;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                // unitModelImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                // unitModelImage.overrideSprite = sprite;

                UnitModel.GetComponent<SpriteRenderer>().sprite = sprite;

                // TODO：实现sprite获取失败时默认贴图
                if (sprite == null)
                {
                    sprite = Resources.Load("UnitModel/ModelImage/丘丘人", typeof(Sprite)) as Sprite;
                }

            }
            catch
            {
                Debug.Log(unit.unitName + " 无模型");
            }

            // TODO：设置元素
        }
    }
}