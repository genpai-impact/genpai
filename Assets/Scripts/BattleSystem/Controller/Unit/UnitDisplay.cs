using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Genpai
{
    /// <summary>
    /// 单位显示模块，管理单位UI显示
    /// </summary>
    public class UnitDisplay : MonoBehaviour
    {

        /// <summary>
        /// 待显示单位
        /// </summary> 
        public UnitView UnitView;

        public Text atkText;
        public Text hpText;
        [FormerlySerializedAs("EngText")] public Text engText;
        public Image atkElement;    // 攻击元素图标
        [FormerlySerializedAs("CurrentEle")] public Image currentEle;    // 附着元素图标

        [FormerlySerializedAs("EngCanvas")] public GameObject engCanvas;

        [FormerlySerializedAs("UILayer")] public GameObject uiLayer;

        private Dictionary<string, GameObject> _buffOverlayImage;


        private readonly HashSet<string> _buffHaveOverlay = new HashSet<string>
        {
            "Shield",
            "Freeze",
        };

        private readonly HashSet<ElementEnum> _elementHaveIcon = new HashSet<ElementEnum>
        {
            ElementEnum.Pyro,
            ElementEnum.Hydro,
            ElementEnum.Cryo,
            ElementEnum.Electro,
            ElementEnum.Anemo
        };


        public void Init(UnitView unitView)
        {
            UnitView = unitView;

            if (UnitView == null)
            {
                ShutDisplay();
                return;
            }

            uiLayer.SetActive(true);
            _buffOverlayImage = new Dictionary<string, GameObject>();
            DisplayUnit();
            if (UnitView.UnitType == CardType.Chara)
            {
                engCanvas.GetComponentInChildren<Animator>().SetInteger("expectEng", UnitView.EruptMp);
            }
        }

        public void ShutDisplay()
        {
            uiLayer.SetActive(false);
            GetComponent<UnitModelDisplay>().Init();
            foreach (KeyValuePair<string, GameObject> pair in _buffOverlayImage)
            {
                pair.Value.SetActive(false);
            }
        }

        /// <summary>
        /// 更新UI信息
        /// </summary>
        public void FreshUnitUI(UnitView unitView)
        {
            if (UnitView == null || UnitView.UnitName != unitView.UnitName)
            {
                Init(unitView);
                return;
            }

            UnitView = unitView;

            atkText.text = UnitView.Atk.ToString();
            hpText.text = UnitView.Hp.ToString();

            if (UnitView.UnitType == CardType.Chara)
            {
                engText.text = UnitView.Mp.ToString();
                engCanvas.GetComponentInChildren<Animator>().SetInteger("eng", UnitView.Mp);
            }
            UnitColorChange();
            FreshBuffOverlay();
            ShowSelfElement();
        }

        public void Update()
        {
            // 可作为性能优化点
            UnitColorChange();
        }

        public void UnitColorChange()
        {
            UnitEntity unitEntity = GetComponent<UnitEntity>();
            if (unitEntity == null || unitEntity.GetUnit() == null)
            {
                return;
            }
            if (unitEntity.ownerSite != BattleSite.P1)
            {
                return;
            }
            Transform childTransform = transform.parent.parent.Find("Attacked");
            if (childTransform == null || childTransform.gameObject == null)
            {
                return;
            }
            if (unitEntity.GetUnit().ActionState[UnitState.ActiveAttack])
            {
                childTransform.gameObject.SetActive(false);
            }
            if (!unitEntity.GetUnit().ActionState[UnitState.ActiveAttack])
            {
                childTransform.gameObject.SetActive(true);
            }

        }

        /// <summary>
        /// Buff附着显示
        /// </summary>
        private void FreshBuffOverlay()
        {
            List<BuffView> buffViews = UnitView.BuffViews;

            HashSet<string> buffOverlay = new HashSet<string>();

            // 获取可显示Buff
            foreach (var buff in buffViews)
            {
                if (_buffHaveOverlay.Contains(buff.BuffName))
                {
                    buffOverlay.Add(buff.BuffName);
                }
            }

            // 新增Buff层
            foreach (var buff in buffOverlay)
            {
                if (_buffOverlayImage.ContainsKey(buff))
                {
                    continue;
                }
                else
                {
                    GameObject buffOverlayPrefab = Resources.Load("Prefabs/Buff/" + buff.ToString()) as GameObject;
                    GameObject newImg = GameObject.Instantiate(buffOverlayPrefab, gameObject.transform);
                    newImg.transform.localScale = new Vector3(1, 1, 0);

                    // newImg.GetComponent<SpriteRenderer>().sprite = Resources.Load("ArtAssets/BuffOverlay/" + buff.ToString(), typeof(Sprite)) as Sprite;

                    _buffOverlayImage.Add(buff, newImg);

                }

            }

            // 刷新显示
            foreach (KeyValuePair<string, GameObject> pair in _buffOverlayImage)
            {
                pair.Value.SetActive(buffOverlay.Contains(pair.Key));
            }

        }

        /// <summary>
        /// 初始化显示
        /// </summary>
        private void DisplayUnit()
        {
            SetUIbyUnitType();
            FreshUnitUI(UnitView);
            GetComponent<UnitModelDisplay>().Init();
        }

        /// <summary>
        /// 初始化不同类型单位UI
        /// </summary>
        private void SetUIbyUnitType()
        {
            switch (UnitView.UnitType)
            {
                case CardType.Chara:
                    engCanvas.SetActive(true);
                    break;
                case CardType.Boss:
                {
                    // transform.Find("UI").gameObject.SetActive(false);
                    //Debug.Log(unit.transform.GetChild(1).gameObject.name);
                    var uiChild = uiLayer.transform.GetChild(0).gameObject;
                    uiChild.transform.GetChild(0).gameObject.SetActive(false);
                    uiChild.transform.GetChild(1).gameObject.SetActive(false);
                    uiChild.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                    uiChild.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    break;
                }
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
                ElementEnum element = UnitView.SelfElement;

                if (_elementHaveIcon.Contains(element))
                {
                    string elementIconPath = "ArtAssets/UI/战斗界面/人物 Buff/人物元素Buff-" + element;

                    Sprite sprite = Resources.Load(elementIconPath, typeof(Sprite)) as Sprite;

                    currentEle.sprite = sprite;
                    currentEle.color = new Color(255, 255, 255, 255);
                }
                else
                {
                    currentEle.color = new Color(255, 255, 255, 0);
                }
            }
            catch
            {
                Debug.Log("报错一定要输出点东西才行");
            }
        }


    }
}