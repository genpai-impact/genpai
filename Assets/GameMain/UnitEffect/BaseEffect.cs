namespace Genpai
{
    /// <summary>
    /// 抽象类基础作用,对单位造成任何作用都通过该类实现
    /// 主要形式为（伤害，恢复，挂buff）
    /// 主要被技能类、Buff类、以及普通攻击时调用
    /// </summary>
    public abstract class BaseEffect
    {
        public abstract void OnEffect(BaseUnit target);
    }
}
