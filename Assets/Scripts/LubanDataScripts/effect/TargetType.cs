//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cfg.effect
{
    [System.Flags]
    public enum TargetType
    {
        /// <summary>
        /// 无目标
        /// </summary>
        None = 0,
        /// <summary>
        /// 对手
        /// </summary>
        Enemy = 1,
        /// <summary>
        /// 大怪
        /// </summary>
        Boss = 2,
        /// <summary>
        /// 自己
        /// </summary>
        Self = 4,
        /// <summary>
        /// 非对手
        /// </summary>
        NotEnemy = Self|Boss,
        /// <summary>
        /// 非自身
        /// </summary>
        NotSelf = Boss|Enemy,
        /// <summary>
        /// 全体
        /// </summary>
        All = Self|Boss|Enemy,
    }
}
