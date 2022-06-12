using System;
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

        private Dictionary<string, GameObject> _buffOverlayImage = new Dictionary<string, GameObject>();


        private readonly HashSet<string> _buffHaveOverlay = new HashSet<string>
        {
            "护甲",
            "冻结",
        };

        private readonly HashSet<ElementEnum> _elementHaveIcon = new HashSet<ElementEnum>
        {
            ElementEnum.Pyro,
            ElementEnum.Hydro,
            ElementEnum.Cryo,
            ElementEnum.Electro,
            ElementEnum.Anemo
        };


        /// <summary>
        /// 通用更新接口
        /// </summary>
        /// <param name="unitView">输入Unit信息</param>
        public void Display(UnitView unitView)
        {
            // 如果空就送走
            if (unitView == null)
            {
                ShutDisplay();
                return;
            }
            
            // 是否变化模型
            bool changeFlag = UnitView == null || unitView.UnitName != UnitView.UnitName;
            UnitView = unitView;
            
            // 模型显示
            if(changeFlag) DisplayUnit();
            
            FreshUnitUI();
        }

        private void ShutDisplay()
        {
            // 删数据
            UnitView = null;
            // 删UI
            uiLayer.SetActive(false);
            // 删模型
            GetComponent<UnitModelDisplay>().Init();
            // 删Buff图
            foreach (KeyValuePair<string, GameObject> pair in _buffOverlayImage)
            {
                pair.Value.SetActive(false);
            }
            _buffOverlayImage = new Dictionary<string, GameObject>();
        }

        /// <summary>
        /// 更新UI信息
        /// </summary>
        private void FreshUnitUI()
        {
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
        

        /// <summary>
        /// 调整Unit状态（以静止格为主
        /// </summary>
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
            childTransform.gameObject.SetActive(!unitEntity.GetUnit().ActionState[UnitState.ActiveAttack]);
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
            GetComponent<UnitModelDisplay>().Init();
        }

        /// <summary>
        /// 初始化单位UI
        /// </summary>
        private void SetUIbyUnitType()
        {
            uiLayer.SetActive(true);
            
            switch (UnitView.UnitType)
            {
                case CardType.Chara:
                    engCanvas.SetActive(true);
                    engCanvas.GetComponentInChildren<Animator>().SetInteger("expectEng", UnitView.EruptMp);
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
                    string elementIconPath = "ArtAssets/UI/战斗界面/Buff/元素Buff-" + element;

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