//������Ч��
yield return 1;
FlyEffect effect =
B1Game.Effect.EffectManager.Instance.CreateFlyEffect(skillPlayerID, skillTargetID,
PMsg.effectid, (uint)pMsg.uniqueid, pos, dir, pMsg.ifAbsorbSkill);
//���еļ�����Ч���Ƕ�����EffectManager�������(kk�����޸ģ�
//4.��ͨ׷����Ч
public FlyEffect CreateFIyEffect(UInt64 owner, UInt64 target, uint skillID, uint
projectID, Vector3 position, Vector3 direction, bool isAbsorb)
{
    SkillEmitConfig skillConfig = ConfigReader.GetSkillEmitcfg(skillID);
    //�ж���Դ·���Ƿ���Ч
    if (skillConfig == null || skillConfig.effect == "O")
        return null;
    string resourcePath = GameConstDefine.LoadGameSkillEffectPath + "release/"
skillConfig.effect;
    Ientity entityOwner, entityTarget;
    EntityManager.AllEntitys.TryGetValue(owner, out entityOwner);
    EntityManager.AllEntitys.TryGetValue(target, out entityTarget);
    //�ͷ��߲���Ϊ�ա�
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
        //������Ч��Ϣ
        effect.cehuaTime = skillConfig.lifeTime;
        effect.resPath = resourcePath;
        //����
        effect.Create();
        AddEffect(effect.projectID, effect);
        return effect;
    }
}
