using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Genpai
{
    public class UnitDisplay : MonoBehaviour
    {

        /// <summary>
        /// 待显示单位
        /// </summary> 
        public UnitView unitView;

        public Text atkText;
        public Text hpText;
        public Text EngText;
        public Image atkElement;    // 攻击元素图标
        public Image CurrentEle;    // 附着元素图标

        public GameObject EngCanvas;


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


        public void Init(UnitView _unitView)
        {
            unitView = _unitView;

            if (unitView == null)
            {
                ShutDisplay();
                return;
            }

            UILayer.SetActive(true);
            BuffOverlayImage = new Dictionary<BuffEnum, GameObject>();
            DisplayUnit();
        }

        public void ShutDisplay()
        {
            UILayer.SetActive(false);
            GetComponent<UnitModelDisplay>().Init();
        }

        /// <summary>
        /// 更新UI信息
        /// </summary>
        public void FreshUnitUI(UnitView _unitView)
        {
            if (unitView == null || unitView.unitName != _unitView.unitName)
            {
                Init(_unitView);
                return;
            }

            atkText.text = _unitView.ATK.ToString();
            hpText.text = _unitView.HP.ToString();

            if (unitView.unitType == UnitType.Chara)
            {
                FreshCharaUI();
            }

            FreshBuffOverlay();
            ShowSelfElement();
        }

        /// <summary>
        /// 更新单位UI
        /// </summary>
        private void FreshCharaUI()
        {
            CharaComponent charaComponent = GetComponent<CharaComponent>();
            if (charaComponent != null)
            {
                EngText.text = charaComponent.MP.ToString();
            }
        }

        /// <summary>
        /// Buff附着显示
        /// </summary>
        private void FreshBuffOverlay()
        {
            List<BuffView> buffViews = unitView.buffViews;

            HashSet<BuffEnum> BuffOverlay = new HashSet<BuffEnum>();

            // 获取可显示Buff
            foreach (var buff in buffViews)
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

        /// <summary>
        /// 初始化显示
        /// </summary>
        private void DisplayUnit()
        {
            SetUIbyUnitType();
            FreshUnitUI(unitView);
            GetComponent<UnitModelDisplay>().Init();
        }

        /// <summary>
        /// 初始化不同类型单位UI
        /// </summary>
        private void SetUIbyUnitType()
        {

            if (unitView.unitType == UnitType.Chara)
            {
                EngCanvas.SetActive(true);
            }
            else if (unitView.unitType == UnitType.Boss)
            {
                transform.Find("UI").gameObject.SetActive(false);
            }
        }

        // >>> 图标获取类
        /// <summary>
        /// 更新元素附着显示
        /// </summary>
        private void ShowSelfElement()
        {
            try
            {
                ElementEnum element = unitView.SelfElement;

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


    }
}