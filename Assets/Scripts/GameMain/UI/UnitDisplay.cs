using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    public class UnitDisplay : MonoBehaviour
    {
        // ��ȡ��λ��Ϣ
        public Unit unit;

        // ���޸�Ϊģ��չʾ����ȫ���˽⣩
        public Image unitModelImage;

        // ��չʾUI����
        public Text unitName;
        public Text BuffInfoText;
        public Text atkText;
        public Text hpText;
        public Image atkElement;    // ����Ԫ��ͼ��
        public Image CurrentEle;    // ����Ԫ��ͼ��
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
                Debug.Log(unit.unitName + " ��ģ��");
            }

            // ����Ԫ��
            // TODO
        }
    }
}