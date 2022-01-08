using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    public class UnitDisplay : MonoBehaviour
    {
        // 获取单位信息
        public Unit unit;

        // 待修改为模型展示（完全不了解）
        public Image unitModelImage;

        // 待展示UI内容
        public Text unitName;
        public Text BuffInfoText;
        public Text atkText;
        public Text hpText;
        public Image atkElement;    // 攻击元素图标
        public Image CurrentEle;    // 附着元素图标
                                    // Start is called before the first frame update
        void Start()
        {
            if (unit != null)
            {
                DisplayUnit();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayUnit()
        {
            unitName.text = unit.unitName;

            atkText.text = unit.ATK.ToString();
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