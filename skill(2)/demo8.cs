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
            //���ж����������֮��ص�free ����
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
//���չ���ɼ��ܣ����в�����֮��free
//�������������������ܷ���
//�ͷ���Ҳ�ж����ˣ����������ڷ��й���
//Ѫ���ı䣬֪ͨ�ͻ���eMsgToGCFromGs NotifyHPChange
//���п�Ѫ����
//�ٴη���eMsgToGCFromGS NotifySkillMode IHitTarget
//֪ͨ�ͻ��ˣ��Ѿ����У�����������Ч����
