if (skillManagerConfig.isNormalAttack == 1)
{
    RealEntity.PlayeAttackAnimation();
}
else
{
    {
        RealEntity.PlayerAnimation(skillManagerConfig.rAnimation);
        if (RealEntity.animation[skill]ManagerConfig.rAnimation != null && RealEntity.animation[skillManagerConfig.rAnimation].wrapMode != WrapMode.Loop)
            {
            //所有动画播放完成之后回到free 动画
            RealEntity.CrossFadeSqu("free");
        }
        BlGame.Effect.EffectManagen.playSkillReleaseSound(this, EntitySkillID);
    }
}
float distance = GetDistanceToPos(EntityFSMPosition);
if (entityType! = EntityType.Building)
{
    obitransform.rotation =
    Quaternion.LookRotation(EntityFSMDirection);
}
//将普攻算成技能，所有播放完之后free
//动画，播放声音，技能方向
//释放着也有动作了，技能粒子在飞行过程
//血量改变，通知客户端eMsgToGCFromGs NotifyHPChange
//进行扣血表现
//再次发送eMsgToGCFromGS NotifySkillMode IHitTarget
//通知客户端，已经击中，创建技能特效出来
