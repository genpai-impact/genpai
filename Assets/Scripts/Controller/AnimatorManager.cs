using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Genpai
{
    /// <summary>
    /// 凹凸曼  2022/4/16
    /// 动画管理器，攻击动画，受击动画，ui更新，元素特效，死亡等动画的调用都放在这里，实现攻击动画的按序播放
    /// 仍待改进：
    /// 实际效果一般，只能说如果能耐心地等待每次动画运行结束再操作可能能勉强达到按序的要求
    /// 同一个目标多个动画请求到达时会有延时，似乎是动画本身的问题。比如刻晴芭芭拉攻击有个起手时间（意义不明的idle状态），导致播放时会有问题
    /// 多个对象同时发出攻击指令时可能会导致一些动画效果结算顺序出问题
    /// 动画播放时玩家依然可以操作，导致最后效果看起来更乱
    /// 写的丑，以后一定自学设计模式
    /// 个人想法:
    /// 这部分代码其实只是相当于一个转接口，很多东西依然是其他类的功能，使得这部分代码写起来很难受
    /// 动画管理器的实现还是想再讨论一下，战斗过程涉及到的图案更新还挺多挺乱的，希望讨论下实现的大概框架
    /// </summary>
    public class AnimatorManager : MonoSingleton<AnimatorManager>
    {
        private Queue<Animator> animatorQueue = new Queue<Animator>();

        private Queue<IEffect> damageQueue = new Queue<IEffect>();

        private Queue<string> triggerQueue = new Queue<string>();

        private Queue<UnitDisplay> unitDisplayQueue = new Queue<UnitDisplay>();

        private List<Animator> injuredOnDisplay = new List<Animator>();

        private List<IEffect> buffOnDisplay = new List<IEffect>();

        private List<Damage> reactionOnDisplay = new List<Damage>();

        private List<UnitDisplay> fallOnDisplay = new List<UnitDisplay>();

        private Animator animatorOnDisplay;

        private Damage damageOnDisplay;

        private bool isAtkDisplay = false;

        private bool isInjuredDisplay = false;

        private Vector3 sourceVector3;
        private Vector3 targetVector3;

        private GameObject atkGameObject;

        private BattleSite atkBattleSite;

        private float atkTime;

        /// <summary>
        /// Do sth. But temporarily it needs to do nothing.
        /// </summary>
        void Awake()
        {

        }

        /// <summary>
        /// 将要播的动画输入queue
        /// 主要是需要animator的动画，例如攻击、受击等
        /// </summary>
        public void InsertAnimator(Damage damage, Animator animator, string trigger)
        {
            damageQueue.Enqueue(damage);
            animatorQueue.Enqueue(animator);
            triggerQueue.Enqueue(trigger);
        }

        /// <summary>
        /// 将要播的动画输入queue
        /// 不需要animator的动画，例如buff等
        /// </summary>
        public void InsertAnimator(UnitDisplay unitDisplay, string trigger){
            unitDisplayQueue.Enqueue(unitDisplay);
            triggerQueue.Enqueue(trigger);
        }

        /// <summary>
        /// 将要播的动画输入queue
        /// 不需要animator的动画，例如buff等
        /// </summary>
        public void InsertAnimator(IEffect effect, string trigger){
            damageQueue.Enqueue(effect);
            triggerQueue.Enqueue(trigger);
        }

        /// <summary>
        /// 按顺序播放动画，以一次攻击以及其结算结束过程中触发的动画为一个流程
        /// 工作流程：
        /// 1.检测是否有攻击或受击在播放，若无，查看是否有攻击请求，有则继续进行，无则结束
        /// 2.设置攻击动画
        /// 3.按照triiger取出动画请求，直到遇到下一个攻击请求。执行受击动画，并将buff、reaction、fall的显示请求放入队列管理
        /// 4.等待受击动画结束，显示伤害、元素反应特效、角色死亡
        /// 5.一次流程结束
        /// 待优化：
        /// 具体实现效果（希望讨论一下
        /// 代码结构（等我看一下设计模式
        /// </summary>
        void Update()
        {
            if (!isAtkDisplay && !isInjuredDisplay && animatorQueue.Count != 0)
            {
                isAtkDisplay = true;
                animatorOnDisplay = animatorQueue.Dequeue();
                damageOnDisplay = (Damage)damageQueue.Dequeue();
                triggerQueue.Dequeue();
                // Debug.Log(Time.time+" attack " + animatorOnDisplay.name);
                AnimationHandle.Instance.AddAnimator("atk", animatorOnDisplay);
                // or setbool, add a callback function in each animator clip
                if (isTriggerExist(animatorOnDisplay, "atk")) {
                    sourceVector3 = BucketEntityManager.Instance.GetBucketBySerial(damageOnDisplay.GetSource().carrier.serial).transform.position;
                    targetVector3 = BucketEntityManager.Instance.GetBucketBySerial(damageOnDisplay.GetTarget().carrier.serial).transform.position;
                    Debug.Log(sourceVector3 + " " + damageOnDisplay.GetSource().unitName);
                    atkGameObject = BucketEntityManager.Instance.GetBucketBySerial(damageOnDisplay.GetSource().carrier.serial);
                    atkBattleSite = damageOnDisplay.GetSource().carrier.ownerSite;
                    if(atkBattleSite == BattleSite.P1) {
                        atkGameObject.transform.position = targetVector3;
                        atkGameObject.transform.Translate(-4,0,0);
                    }
                    else if(atkBattleSite == BattleSite.P2) {
                        atkGameObject.transform.position = targetVector3;
                        atkGameObject.transform.Translate(4,0,0);
                    }
                    animatorOnDisplay.SetTrigger("atk");
                    atkTime = Time.time;
                }
            }
            if (isAtkDisplay || isInjuredDisplay)
            {
                if (isInjuredDisplay == false)
                {
                    // Debug.Log(Time.time+" injured begin");
                    isInjuredDisplay = true;
                    injuredOnDisplay.Clear();
                    buffOnDisplay.Clear();
                    reactionOnDisplay.Clear();
                    fallOnDisplay.Clear();

                    while (triggerQueue.Count != 0 && triggerQueue.Peek() != "atk")
                    {
                        if (triggerQueue.Count != 0 && triggerQueue.Peek() == "injured")
                        {
                            // Debug.Log(Time.time+" injured " + animatorQueue.Peek());
                            injuredOnDisplay.Add(animatorQueue.Peek());
                            AnimationHandle.Instance.AddAnimator("injured", animatorQueue.Peek());
                            if(isTriggerExist(animatorQueue.Peek(), "injured") && !((Damage)damageQueue.Peek()).target.isFall)
                                animatorQueue.Peek().SetTrigger("injured");
                            animatorQueue.Dequeue();
                            damageQueue.Dequeue();
                            triggerQueue.Dequeue();
                        }
                        if (triggerQueue.Count != 0 && triggerQueue.Peek() == "addbuff")
                        {
                            // Debug.Log(Time.time+" addbuff " + animatorQueue.Peek());
                            if (!((AddBuff)damageQueue.Peek()).target.isFall)
                                buffOnDisplay.Add(damageQueue.Peek());
                            damageQueue.Dequeue();
                            triggerQueue.Dequeue();
                        }
                        if (triggerQueue.Count != 0 && triggerQueue.Peek() == "delbuff")
                        {
                            if (!((DelBuff)damageQueue.Peek()).target.isFall)
                                buffOnDisplay.Add(damageQueue.Peek());
                            damageQueue.Dequeue();
                            triggerQueue.Dequeue();
                        }
                        if (triggerQueue.Count != 0 && triggerQueue.Peek() == "reaction")
                        {
                            // Debug.Log(Time.time+" reaction " + animatorQueue.Peek());
                            if (!((Damage)damageQueue.Peek()).target.isFall)
                                reactionOnDisplay.Add((Damage)damageQueue.Peek());
                            damageQueue.Dequeue();
                            triggerQueue.Dequeue();
                        }
                        if (triggerQueue.Count != 0 && triggerQueue.Peek() == "fall")
                        {
                            // Debug.Log(Time.time+" reaction " + animatorQueue.Peek());
                            fallOnDisplay.Add(unitDisplayQueue.Peek());
                            unitDisplayQueue.Dequeue();
                            triggerQueue.Dequeue();
                        }
                    }
                }
                if (!isTriggerExist(animatorOnDisplay, "atk") || (animatorOnDisplay.GetBool("atk") == false && Time.time-atkTime > 1.0f))
                {
                    if (isAtkDisplay == true)
                    {
                        isAtkDisplay = false;
                        if(atkBattleSite == BattleSite.P1) {
                            atkGameObject.transform.position = sourceVector3;
                        }
                        else if(atkBattleSite == BattleSite.P2) {
                            atkGameObject.transform.position = sourceVector3;
                        }
                        // Debug.Log(Time.time+" attack finished");
                        HittenNumManager.Instance.PlayDamage(damageOnDisplay);
                        if (!damageOnDisplay.target.isFall)
                            BucketEntityManager.Instance.GetUnitEntityByUnit(damageOnDisplay.GetTarget()).UnitDisplay.FreshUnitUI(damageOnDisplay.GetTarget().GetView());
                        foreach (IEffect effect in buffOnDisplay)
                        {
                            BucketEntityManager.Instance.GetUnitEntityByUnit(effect.GetTarget()).UnitDisplay.FreshUnitUI(effect.GetTarget().GetView());
                        }
                        foreach (Damage damage in reactionOnDisplay)
                        {
                            HittenNumManager.Instance.PlayDamage(damage);
                            BucketEntityManager.Instance.GetUnitEntityByUnit(damage.GetTarget()).UnitDisplay.FreshUnitUI(damage.GetTarget().GetView());
                        }
                        //BucketEntityManager.Instance.GetUnitEntityByUnit(damageOnDisplay.GetTarget()).UnitDisplay.FreshUnitUI(effect.GetTarget().GetView());
                    }

                    bool injuredFinished = true;
                    foreach (Animator ani in injuredOnDisplay)
                    {
                        if (isTriggerExist(ani, "injured") && ani.GetBool("injured")) injuredFinished = false;
                        else if(Time.time-atkTime<3.0f) injuredFinished = false;
                    }
                    if (injuredFinished)
                    {
                        foreach(UnitDisplay unitDisplay in fallOnDisplay)
                        {
                            if(unitDisplay.unitView.unitType != UnitType.Chara) 
                            {
                                unitDisplay.Init(null); 

                            } 
                            // Debug.Log(Time.time + damage.target.unitName + " fall");
                        }
                        BucketEntityManager.Instance.GetUnitEntityByUnit(GameContext.Instance.GetPlayer1().Chara).UnitDisplay.FreshUnitUI(GameContext.Instance.GetPlayer1().Chara.GetView());
                        BucketEntityManager.Instance.GetUnitEntityByUnit(GameContext.Instance.GetPlayer2().Chara).UnitDisplay.FreshUnitUI(GameContext.Instance.GetPlayer2().Chara.GetView());
                        isInjuredDisplay = false;
                    }

                }
            }
        }

        private bool isTriggerExist(Animator animator, string str)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.name == str) return true;
            }
            return false;
        }

    }
}