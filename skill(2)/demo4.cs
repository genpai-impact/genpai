//创建特效。
yield return 1;
FlyEffect effect =
B1Game.Effect.EffectManager.Instance.CreateFlyEffect(skillPlayerID, skillTargetID,
PMsg.effectid, (uint)pMsg.uniqueid, pos, dir, pMsg.ifAbsorbSkill);
//所有的技能特效我们都放在EffectManager里面管理(kk后期修改）
//4.普通追踪特效
public FlyEffect CreateFIyEffect(UInt64 owner, UInt64 target, uint skillID, uint
projectID, Vector3 position, Vector3 direction, bool isAbsorb)
{
    SkillEmitConfig skillConfig = ConfigReader.GetSkillEmitcfg(skillID);
    //判断资源路径是否有效
    if (skillConfig == null || skillConfig.effect == "O")
        return null;
    string resourcePath = GameConstDefine.LoadGameSkillEffectPath + "release/"
skillConfig.effect;
    Ientity entityOwner, entityTarget;
    EntityManager.AllEntitys.TryGetValue(owner, out entityOwner);
    EntityManager.AllEntitys.TryGetValue(target, out entityTarget);
    //释放者不能为空。
    if (entityOwner == null)
    {
        return null;
    }
    FlyEffect effect = new FlyEffect();
    effect.enOwnerKey = entityOwner.GameObjGUID;
    if (entitytarget! = null)
    {
        effect.enTargetKey = entityTarget.GameObiGUID;
        effect.skillID = skillID;
        effect.fixPosition = position;
        effect.dir = direction;
        effect.isAbsorb = isAbsorb;
        effect.projectID = projectID;
        effect.emitType = skillConfig.emitType;
        //加载特效信息
        effect.cehuaTime = skillConfig.lifeTime;
        effect.resPath = resourcePath;
        //创建
        effect.Create();
        AddEffect(effect.projectID, effect);
        return effect;
    }
}
