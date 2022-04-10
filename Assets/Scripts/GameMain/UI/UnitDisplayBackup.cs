using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位UI展示
    /// 包括展示Buff列表
    /// </summary>
    public class UnitDisplayBackup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 待显示单位
        /// </summary>
        /// 
        public UnitEntity unitEntity;

        /// <summary>
        /// 动画控制器
        /// </summary>
        public Animator animator;

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
        private GameObject UnitModelAni;

        public GameObject UILayer;

        public Dictionary<BuffEnum, GameObject> BuffOverlayImage;

        private float DelayTime = 1f;
        private bool IsShow = false;

        /// <summary>
        /// IsAnimating在协程中使用，实现一个类似Sema的同步信号量功能。其实可能有很多更聪明的办法，待优化
        /// </summary>
        ///
        private bool IsAnimating = false;

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

        private IEnumerator ShowAnimatorState(Damage damage)
        {
            float cnt = 5f;

            while (cnt > 0)
            {
                cnt -= 0.05f;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    break;
                }
                yield return new WaitForSeconds(0.05f);
            }

            while (cnt > 0)
            {
                cnt -= 0.05f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    IsAnimating = false;
                    HittenNumManager.Instance.PlayDamage(damage);
                    break;
                }
                yield return new WaitForSeconds(0.05f);
            }

        }

        private IEnumerator DoAttack(Damage damage)
        {
            while (IsAnimating)
            {
                yield return new WaitForSeconds(0.05f);
            }
            IsAnimating = true;

            string trigger = "atk";
            AnimationHandle.Instance.AddAnimator(trigger, animator);
            animator.SetTrigger(trigger);
            StartCoroutine(ShowAnimatorState(damage));
        }

        /// <summary>
        /// 显示攻击动画仅仅是对外接口，实际功能在DoAttack()中实现
        /// </summary>
        /// 
        public void AttackAnimation(Damage damage)
        {
            if (animator != null)
            {
                StartCoroutine(DoAttack(damage));
            }
            else HittenNumManager.Instance.PlayDamage(damage);
        }

        private IEnumerator DoInjured()
        {
            float cnt = 5f;

            while (IsAnimating)
            {
                yield return new WaitForSeconds(0.05f);
            }
            IsAnimating = true;

            string trigger = "injured";
            AnimationHandle.Instance.AddAnimator(trigger, animator);
            animator.SetTrigger("injured");

            while (cnt > 0)
            {
                cnt -= 0.05f;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("injured"))
                {
                    break;
                }
                yield return new WaitForSeconds(0.05f);
            }

            while (cnt > 0)
            {
                cnt -= 0.05f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("injured"))
                {
                    IsAnimating = false;
                    break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        /// <summary>
        /// 显示受伤动画,仅仅是对外接口，实际功能在DoInjured()中实现
        /// </summary>
        /// 
        public void InjuredAnimation()
        {
            if (animator != null)
            {
                StartCoroutine(DoInjured());
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
                /*
                // CharaComponent charaComponent = GetComponent<CharaComponent>();
                if (charaComponent != null)
                {
                    EngText.text = charaComponent.MP.ToString();
                }
                */
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
                    animator = UnitModelAni.GetComponent<Animator>();

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsShow = true;
            Invoke("ShowInfo", DelayTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsShow = false;
            HideInfo();
        }

        public void ShowInfo()
        {
            if (!IsShow)
            {
                return;
            }
            UnitInfoDisplay t = PrefabsLoader.Instance.infoCard.GetComponent<UnitInfoDisplay>();
            //if (t && t.GetUnitEntity() != unitEntity)
            //{
            //    t.Init(unitEntity);
            //}
            t.Display(InfoCardType.MonsterOnBattleInfo);
        }

        public void HideInfo()
        {
            PrefabsLoader.Instance.infoCard.GetComponent<UnitInfoDisplay>().Hide();
        }
    }
}