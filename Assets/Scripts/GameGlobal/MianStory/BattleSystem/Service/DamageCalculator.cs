using BattleSystem.Service.Effect;
using BattleSystem.Service.Element;
using Utils;

namespace BattleSystem.Service
{
    /// <summary>
    /// 伤害计算器
    /// </summary>
    public partial class DamageCalculator : Singleton<DamageCalculator>
    {
        // 计算器线程锁
        public static object calculatorLock = new object();

        /// <summary>
        /// 计算当前伤害
        /// </summary>
        /// <param name="damage">待计算伤害</param>
        /// <returns>元组（伤害，目标）</returns>
        public void Calculate(ref Damage damage)
        {
            lock (calculatorLock)
            {
                Unit.Unit source = damage.GetSource();
                Unit.Unit target = damage.GetTarget();
                ElementReactionEnum reaction;
                // 进行元素攻击流程
                reaction = TakeReaction(damage);
                damage.DamageReaction = reaction;
                // 实现元素反应加伤&事件
                CalculateReaction(reaction, ref damage);
                // TODO：获取Buff相关过程加伤
            }
        }

        /// <summary>
        /// 进行元素反应
        /// </summary>
        /// <param name="damage">伤害事件</param>
        /// <returns>元素反应类型</returns>
        public ElementReactionEnum TakeReaction(Damage damage)
        {
            Unit.Unit target = damage.GetTarget();
            Unit.Unit source = damage.GetSource();
            ElementReactionEnum reaction = ElementReactionEnum.None;
            // 单位已经死亡
            if (target == null || target.IsFall)
            {
                return reaction;
            }

            Element.Element targetAttachment = target.SelfElement;
            ElementEnum damageElement = damage.DamageStructure.Element;

            // 判断是否产生元素反应
            if (damageElement != ElementEnum.None && damage.DamageStructure.AttendReaction)
            {
                // 不存在附着则追加附着
                if (targetAttachment.ElementType == ElementEnum.None)
                {
                    target.SelfElement = new Element.Element(damage.DamageStructure.Element);
                }
                // 存在附着那就元素反应
                else
                {
                    reaction = targetAttachment.ElementReaction(damage.DamageStructure.Element);
                }
            }
            // >>> 受元素反应影响Buff检测 
            // 待重构为Element追加Buff，随元素销毁模式
            /*
            BaseBuff indexFreeze = target.BuffAttachment.FirstOrDefault(buff => buff.BuffName == BuffEnum.Freeze);
            if (indexFreeze != null && damageElement == ElementEnum.Pyro)
            {
                //目标处于冻结状态且攻击为火伤
                if (targetAttachment.ElementType == ElementEnum.None)
                {
                    //无元素附着则追加冰附着
                    target.SelfElement = new Element(ElementEnum.Cryo);
                }
                // 去除冻结状态
                EffectManager.Instance.InsertTimeStep(
                    new EffectTimeStep(new List<IEffect> { new DelBuff(source, target, BuffEnum.Freeze) },
                    TimeEffectType.Appendix));
            }
        
            BaseBuff indexBurn = target.BuffAttachment.FirstOrDefault(buff => buff.BuffName == BuffEnum.Burning);
            //水元素攻击移除燃烧Buff
            if (indexBurn != null && damageElement == ElementEnum.Hydro)
            {
                EffectManager.Instance.InsertTimeStep(
                    new EffectTimeStep(new List<IEffect> { new DelBuff(source, target, BuffEnum.Burning, int.MaxValue) },
                    TimeEffectType.Appendix));
            }
            */
            // >>>

            return reaction;
        }

        /// <summary>
        /// 执行元素反应
        /// </summary>
        /// <param name="reaction">待执行元素反应</param>
        /// <param name="damage">对应元素反应伤害</param>
        public void CalculateReaction(ElementReactionEnum reaction, ref Damage damage)
        {

            Unit.Unit source = damage.GetSource();
            Unit.Unit target = damage.GetTarget();

            switch (reaction)
            {
                case ElementReactionEnum.None:
                    break;
                case ElementReactionEnum.Overload:
                    Overload(source, target);
                    break;
                case ElementReactionEnum.Superconduct:
                    Superconduct(source, target);
                    break;
                case ElementReactionEnum.ElectroCharge:
                    ElectroCharge(source, target);
                    break;
                case ElementReactionEnum.Freeze:
                    Freeze(source, target);
                    break;
                case ElementReactionEnum.Melt:
                    Melt(ref damage);
                    break;
                case ElementReactionEnum.Vaporise:
                    Vaporise(ref damage);
                    break;
                case ElementReactionEnum.Swirl:
                    Swirl(source, target);
                    break;
                case ElementReactionEnum.Crystallise:
                    Crystallise(source, target);
                    break;
            }
            target.SelfElement.FreshLock();
        }
    }
}