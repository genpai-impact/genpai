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

        public HashSet<string> UnitHaveModel = new HashSet<string> {
            "史莱姆·风",
            "史莱姆·雷",
            "打手丘丘人" };


        public void Init()
        {
            UnitModel.SetActive(true);
            UILayer.SetActive(true);
            unitEntity = GetComponent<UnitEntity>();

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
                EngText.text = GetComponent<CharaComponent>().MP.ToString();
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

            // TODO：设置元素
        }
    }
}