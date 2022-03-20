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
        /// 
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
        public GameObject UnitModelAni;

        public GameObject UILayer;

        public Dictionary<BuffEnum, GameObject> BuffOverlayImage;

        public HashSet<string> UnitHaveModel = new HashSet<string> {
            "史莱姆·水",
            "史莱姆·冰",
            "史莱姆·火",
            "史莱姆·雷",
            "史莱姆·风",
            "史莱姆·岩",
            "打手丘丘人" };

        public HashSet<BuffEnum> BuffHaveOverlay = new HashSet<BuffEnum>
        {
            BuffEnum.Shield,
            BuffEnum.Freeze
        };

        public HashSet<ElementEnum> ElementHaveIcon = new HashSet<ElementEnum>
        {
            ElementEnum.Pyro,
            ElementEnum.Hydro,
            ElementEnum.Cryo,
            ElementEnum.Electro,
            ElementEnum.Anemo
        };


        public void Init()
        {
            UnitModel.SetActive(true);
            UILayer.SetActive(true);
            unitEntity = GetComponent<UnitEntity>();
            BuffOverlayImage = new Dictionary<BuffEnum, GameObject>();

            if (unitEntity.unit != null)
            {
                DisplayUnit();
            }

        }

        public void AttackAnimation()
        {

            if (unitEntity.animator != null)
            {
                unitEntity.animator.SetTrigger("atk");
            }
        }

        public void InjuredAnimation()
        {
            if (unitEntity.animator != null)
            {
                unitEntity.animator.SetTrigger("injured");
            }

        }

        public void FreshUnitUI()
        {
            atkText.text = unitEntity.ATK.ToString();
            hpText.text = unitEntity.HP.ToString();

            // 如果为角色，打开能量框
            if (unitEntity.unitType == UnitType.Chara)
            {
                EngCanvas.SetActive(true);
                CharaComponent charaComponent = GetComponent<CharaComponent>();
                if (charaComponent != null)
                {
                    EngText.text = charaComponent.MP.ToString();
                }
            }
            else if (unitEntity.unitType == UnitType.Boss)
            {
                transform.Find("UI").gameObject.SetActive(false);
            }

            FreshBuffOverlay();

            try
            {
                ElementEnum element = unitEntity.ElementAttachment.ElementType;

                if (ElementHaveIcon.Contains(element))
                {
                    string ElementIconPath = "ArtAssets/UI/战斗界面/人物 Buff/人物元素Buff-" + element;

                    Sprite sprite = Resources.Load(ElementIconPath, typeof(Sprite)) as Sprite;

                    CurrentEle.sprite = sprite;
                    CurrentEle.color = new Color(255, 255, 255, 255);
                }
                else
                {
                    CurrentEle.color = new Color(255, 255, 255, 0);
                }
            }
            catch
            {
                Debug.Log("报错一定要输出点东西才行");
            }


                }

        private void FreshBuffOverlay()
        {
            List<BaseBuff> BuffList = unitEntity.buffAttachment;
            HashSet<BuffEnum> BuffOverlay = new HashSet<BuffEnum>();

            // 获取可显示Buff
            foreach (var buff in BuffList)
            {
                if (BuffHaveOverlay.Contains(buff.buffName))
                {
                    BuffOverlay.Add(buff.buffName);
                }
            }

            // 新增Buff层
            foreach (BuffEnum buff in BuffOverlay)
            {
                if (BuffOverlayImage.ContainsKey(buff))
                {
                    continue;
                }
                else
                {
                    GameObject BuffOverlayPrefab = Resources.Load("Prefabs/BuffOverlay") as GameObject;
                    GameObject newImg = GameObject.Instantiate(BuffOverlayPrefab, gameObject.transform);

                    newImg.GetComponent<SpriteRenderer>().sprite = Resources.Load("ArtAssets/BuffOverlay/" + buff.ToString(), typeof(Sprite)) as Sprite;

                    BuffOverlayImage.Add(buff, newImg);
                }

            }

            // 刷新显示
            foreach (KeyValuePair<BuffEnum, GameObject> pair in BuffOverlayImage)
            {
                if (BuffOverlay.Contains(pair.Key))
                {
                    pair.Value.SetActive(true);
                }
                else
                {
                    pair.Value.SetActive(false);
                }
            }

        }

        private void DisplayUnit()
        {
            Unit unit = unitEntity.unit;

            FreshUnitUI();

            try
            {
                string imgPath = "UnitModel/ModelImage/" + unit.unitName;
                string modelPath = "UnitModel/UnitPrefabs/" + unit.unitName;


                if (UnitHaveModel.Contains(unit.unitName))
                {
                    GameObject prefab = Resources.Load(modelPath) as GameObject;
                    UnitModelAni = GameObject.Instantiate(prefab, UnitModel.transform);
                    Animator _animator = UnitModelAni.GetComponent<Animator>();
                    unitEntity.animator = _animator;

                }
                else
                {
                    Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                    UnitModel.GetComponent<SpriteRenderer>().sprite = sprite;
                }

            }
            catch
            {
                Debug.Log(unit.unitName + " 无模型");
            }


        }
    }
}