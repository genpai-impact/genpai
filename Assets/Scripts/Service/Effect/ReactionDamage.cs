using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 反应伤害效果结构体,Damage类中的ApplyDamage和攻击动画绑定，对于元素反应伤害会出现要求之外的攻击。遂加入ReactionDamage。
    /// DamageCalculator.Reaction.cs中的Damage均有ReactionDamage代替，对EffectManager.cs的DealTimeStep加入新的case判断。
    /// </summary>
    public class ReactionDamage : Damage
    {
        private MonoBehaviour mbr;

        public ReactionDamage(Unit _source, Unit _target, DamageStruct _damage) : base(_source, _target, _damage)
        {
            effectType = "ReactionDamage";
        }

        ///// <summary>
        ///// 关于这个协程方法，在这个工程里我加了很多（讲道理其实应该集成一下的）
        ///// cnt表示等待的秒数，如果有时候效果显示不出来可能是秒数太短了（不过这里的秒可能和现实中的感受不大一样，反正长一点总没错，但使用无限循环可能导致崩溃
        ///// </summary>
        //private IEnumerator DoDamageAfterSource()
        //{
        //    float cnt = 10f;

        //    while (cnt > 0)
        //    {
        //        cnt -= 0.05f;
        //        if (source.GetComponent<UnitModelDisplay>().animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        //        {
        //            break;
        //        }
        //        yield return new WaitForSeconds(0.05f);
        //    }

        //    while (cnt > 0)
        //    {
        //        cnt -= 0.05f;
        //        if (!source.GetComponent<UnitModelDisplay>().animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        //        {
        //            HittenNumManager.Instance.PlayDamage(this);
        //            break;
        //        }
        //        yield return new WaitForSeconds(0.05f);
        //    }

        //}

        ///// <summary>
        ///// 为了达到攻击后再触发元素反应的效果使用了协程达到同步，讲道理这个形式的代码我已经写了好多段一样的了，是不是该重构一下实现代码复用:(
        ///// 协程见DoDamageAfterSource()
        ///// </summary>
        //public override bool ApplyDamage()
        //{
        //    if (damageStructure.DamageValue == 0)
        //    {
        //        return false;
        //    }
        //    if (source.GetComponent<UnitModelDisplay>().animator != null)
        //    {
        //        mbr = GameObject.FindObjectOfType<MonoBehaviour>();
        //        mbr.StartCoroutine(DoDamageAfterSource());
        //    }
        //    else HittenNumManager.Instance.PlayDamage(this);

        //    // 受击动画已整合至TakeDamage中
        //    (int damageValue, bool isFall) = GetTarget().TakeDamage(damageStructure.DamageValue);
        //    damageStructure.DamageValue = damageValue;

        //    return isFall;

        //}
    }

}