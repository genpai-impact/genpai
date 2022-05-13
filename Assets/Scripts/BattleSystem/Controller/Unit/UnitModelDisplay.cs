﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位模型显示模块，主要实现动画控制
    /// </summary>
    public class UnitModelDisplay : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler
    {

        public UnitView unitView;

        /// <summary>
        /// 动画控制器
        /// </summary>
        public Animator animator;

        public GameObject UnitModel;
        public GameObject UnitModelAni;
        
        
        private float DelayTime = 0;
       // private bool IsShow = false;
       
        /// <summary>
        /// IsAnimating在协程中使用，实现一个类似Sema的同步信号量功能。其实可能有很多更聪明的办法，待优化
        /// </summary>
        ///
        private bool IsAnimating = false;
        UnitInfoDisplay t = null;
        private bool canClick = false;
        public HashSet<string> UnitHaveModel = new HashSet<string> {
            "霜铠丘丘王",
            "刻晴",
            "芭芭拉",
            "史莱姆·水",
            "史莱姆·冰",
            "史莱姆·火",
            "史莱姆·雷",
            "史莱姆·风",
            "史莱姆·岩",
            "打手丘丘人" };

        public void Init()
        {
            if (UnitModelAni != null)
            {
                UnitModelAni.SetActive(false);
                UnitModelAni = null;
            }

            unitView = GetComponent<UnitDisplay>().UnitView;
            if (unitView != null)
            {
                DisplayUnit();
            }
            else
            {
                UnitModel.SetActive(false);

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
            if (animator != null && damage.DamageType == DamageType.NormalAttack)
            {
                // StartCoroutine(DoAttack(damage));
                // AnimatorManager.Instance.InsertAnimator(damage, animator, "atk");
            }
            else HittenNumManager.Instance.PlayDamage(damage);
        }

        /// <summary>
        /// 显示攻击动画仅仅是对外接口，实际功能在DoAttack()中实现
        /// </summary>
        /// 
        public void ReactionAnimation(Damage damage)
        {
            if (animator != null && damage.DamageType == DamageType.Reaction)
            {
                // StartCoroutine(DoAttack(damage));
                // AnimatorManager.Instance.InsertAnimator(damage, "reaction");
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
        public void InjuredAnimation(Damage damage)
        {
            if (animator != null)
            {
                //StartCoroutine(DoInjured());
                // AnimatorManager.Instance.InsertAnimator(damage, animator, "injured");
            }
        }

        private void DisplayUnit()
        {
            UnitModel.SetActive(true);


            try
            {
                string imgPath = "UnitModel/ModelImage/" + unitView.UnitName;
                string modelPath = "UnitModel/UnitPrefabs/" + unitView.UnitName;


                if (UnitHaveModel.Contains(unitView.UnitName))
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
                Debug.Log(unitView.UnitName + " 无模型");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            canClick = true;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(1)&&canClick)
            {
                //IsShow = true;
                Invoke("ShowInfo", DelayTime);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            canClick = false;
            CancelInvoke("ShowInfo");
        }


        public void ShowInfo()
        {
            t = PrefabsLoader.Instance.infoCard.GetComponent<UnitInfoDisplay>();
          //t

            t.Init(GetComponent<UnitDisplay>().UnitView);
            t.Display();//InfoCardType.MonsterOnBattleInfo 原来有这个类型的传参
        }

    }
}