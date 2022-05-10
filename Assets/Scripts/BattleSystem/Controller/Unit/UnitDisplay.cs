﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        public UnitView unitView;

        public Text atkText;
        public Text hpText;
        public Text EngText;
        public Image atkElement;    // 攻击元素图标
        public Image CurrentEle;    // 附着元素图标

        public GameObject EngCanvas;

        public GameObject UILayer;

        public Dictionary<BuffEnum, GameObject> BuffOverlayImage;


        public HashSet<BuffEnum> BuffHaveOverlay = new HashSet<BuffEnum>
        {
            BuffEnum.Shield,
            BuffEnum.Freeze,
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
            if (unitView.unitType == CardType.Chara)
            {
                EngCanvas.GetComponentInChildren<Animator>().SetInteger("expectEng", unitView.EruptMp);
            }
        }

        public void ShutDisplay()
        {
            UILayer.SetActive(false);
            GetComponent<UnitModelDisplay>().Init();
            foreach (KeyValuePair<BuffEnum, GameObject> pair in BuffOverlayImage)
            {
                pair.Value.SetActive(false);
            }
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

            unitView = _unitView;

            atkText.text = unitView.ATK.ToString();
            hpText.text = unitView.HP.ToString();

            if (unitView.unitType == CardType.Chara)
            {
                EngText.text = unitView.MP.ToString();
                EngCanvas.GetComponentInChildren<Animator>().SetInteger("eng", unitView.MP);
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
                    GameObject BuffOverlayPrefab = Resources.Load("Prefabs/Buff/" + buff.ToString()) as GameObject;
                    GameObject newImg = GameObject.Instantiate(BuffOverlayPrefab, gameObject.transform);
                    newImg.transform.localScale = new Vector3(1, 1, 0);

                    // newImg.GetComponent<SpriteRenderer>().sprite = Resources.Load("ArtAssets/BuffOverlay/" + buff.ToString(), typeof(Sprite)) as Sprite;

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

            if (unitView.unitType == CardType.Chara)
            {
                EngCanvas.SetActive(true);
            }
            else if (unitView.unitType == CardType.Boss)
            {
                // transform.Find("UI").gameObject.SetActive(false);
                //Debug.Log(unit.transform.GetChild(1).gameObject.name);
                GameObject uiChild = UILayer.transform.GetChild(0).gameObject;
                uiChild.transform.GetChild(0).gameObject.SetActive(false);
                uiChild.transform.GetChild(1).gameObject.SetActive(false);
                uiChild.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                uiChild.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
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