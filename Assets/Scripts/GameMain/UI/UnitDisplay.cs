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
    public class UnitDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 待显示单位
        /// </summary>
        public UnitEntity unitEntity;

        /// <summary>
        /// 动画控制器
        /// </summary>
        public Animator animator;

        public GameObject UnitModel;
        private GameObject UnitModelAni;


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

        public void Init()
        {
            unitEntity = GetComponent<UnitEntity>();

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

        private void DisplayUnit()
        {
            Unit unit = unitEntity.unit;

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
            if (t && t.GetUnitEntity() != unitEntity)
            {
                t.Init(unitEntity);
            }
            t.Display(InfoCardType.MonsterOnBattleInfo);
        }

        public void HideInfo()
        {
            PrefabsLoader.Instance.infoCard.GetComponent<UnitInfoDisplay>().Hide();
        }
    }
}