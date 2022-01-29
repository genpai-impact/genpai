//进入到iseIfplayer类中找
//再找到父类 lentity 类的该方法：
public virtual void OnEntityReleaseSkill()
{
    SkillManagerConfig skillManagerConfig = ConfigReader.GetSkillManagerCfg(EntitySkillID);
    if (skillManagerConfig == null)
    {
        return;
    }
    //吸附技能
    if (skillManagerConfig.isAbsorb == 1)
    {
        //删除己有的相同特效
        foreach (Transform child in RealEntity.objAttackPoint)
        {
            if (child.name.Contains(skillManagerConfig.absorbRes))
            {
                int offset = child.name.IndexOf("_");
                if (offset != 0)
                {
                    string name = child.name.Substring(offset + 1);
                    int id = Convert.ToInt32(name);
                    EffectManager.Instance.DestroyEffect(id);
                }
            }
        }
        string absortActPath = "effect/soul act/" + skillManagerConfig.absorbRes;
        NormalEffect absortSkillEffect =
        EffectManager.Instance.CreateNormalEffect(absortActPath, RealEntity.objAttackPoint.gameObject);
        if (absortSkillEffect != null)
        {
            Quaternion rt = Quaternion.LookRotation(EntityFSMDirection);
            //absortSkillEffect.obistransform.rotation=rt;
        }
        objTransform.rotation = Quaternion.LookRotation(EntityFSMDirection);
    }
    else
    if (skillManagerConfig.isNormalAttack == 1)
    {
        RealEntity.PlayeAttackAnimation();
    }
    else
    {
        {
            RealEntity.PlayerAnimation(skillManagerConfig.rAnimation);
            if (RealEntity.animation[skillManagerConfig.rAnimation] != nul1 && RealEntity.animation[skil]ManagerConfie.rAnimation].wrapMode! = WrapMode.Loop)
            //所有动画播放完之后回到free动画
            RealEntity.CrossFadeSqu("free");
        }
        B1Game.Effect.EffectManager.playSkillReleaseSound(this，EntitySkillID);
    }
}
float distance = GetDistanceToPos(EntityFSMPosition);
if (entityType != EntityType.Building)
{
    objTransform.rotation = Quaternion.LookRotation(EntityFSMDirection);
}
   }
}
  //首先通过技能id找到技能配置信息类：SkillManagerConfigs
public class SkillManagerConfig
{
    public int id;//id
    public string name;//名宇。
    public int isNormalAttack;//是否普通攻击
    public string yAnimation;//吟唱动画
    public string ySound;//吟唱声音
    public string yEffect;//吟唱效果
    public string rAnimation;//释放动画
    public string rsound;//释放声音
    public string
    rEffect;//吟唱效果
    public int
    useway;//使用方法
    public int targetType;//目标类型
    public string skillIcon;//
    public string info;//
    public int coolDown; //
    public float range;
    public string absorbRes;
    public int isAbsorb;
    public int n32UpgradeLevel;
    public float yTime;//时间
    public float mpUse;
    public float hpUse;
    public float cpUse;
}
//存储相关技能对应数据
