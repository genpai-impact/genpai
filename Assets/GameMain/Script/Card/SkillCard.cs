namespace Genpai
{
    /// <summary>
    /// 效果卡，可以是武器，也可以是魔法
    /// </summary>
    public class SkillCard : BaseCard, ISkill
    {
        public int SkillID{
            get;set;
        }

        public override CardInfo GetDesc()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUse(ITargetable target)
        {
            if (target is ISkillTargetable)
            {
                ((ISkillTargetable)target).TakeSkill();
            }
        }

        /// <summary>
        /// 通过自身技能ID创建Skill类，并通过接口释放
        /// </summary>
        /// <param name="target">释放目标</param>
        public void ReleaseSkill(ISkillTargetable target){
            new Skill(SkillID,this,target);
        }

    }
}