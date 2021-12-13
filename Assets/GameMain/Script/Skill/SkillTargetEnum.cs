namespace Genpai
{
    /// <summary>
    /// 技能目标类型
    /// </summary>
    public enum SkillTargetEnum
    {   
        TARGET, //对目标释放
        SOURCE, // 对自身释放
        NULL,   // 无目标
    }
    public enum  SkillTargetRandomEnum
    {
        APPOINT,// 目标指定
        RANDOM  // 目标随机
    }
}