//���뵽iseIfplayer������
//���ҵ����� lentity ��ĸ÷�����
public virtual void OnEntityReleaseSkill()
{
    SkillManagerConfig skillManagerConfig = ConfigReader.GetSkillManagerCfg(EntitySkillID);
    if (skillManagerConfig == null)
    {
        return;
    }
    //��������
    if (skillManagerConfig.isAbsorb == 1)
    {
        //ɾ�����е���ͬ��Ч
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
            //���ж���������֮��ص�free����
            RealEntity.CrossFadeSqu("free");
        }
        B1Game.Effect.EffectManager.playSkillReleaseSound(this��EntitySkillID);
    }
}
float distance = GetDistanceToPos(EntityFSMPosition);
if (entityType != EntityType.Building)
{
    objTransform.rotation = Quaternion.LookRotation(EntityFSMDirection);
}
   }
}
  //����ͨ������id�ҵ�����������Ϣ�ࣺSkillManagerConfigs
public class SkillManagerConfig
{
    public int id;//id
    public string name;//���
    public int isNormalAttack;//�Ƿ���ͨ����
    public string yAnimation;//��������
    public string ySound;//��������
    public string yEffect;//����Ч��
    public string rAnimation;//�ͷŶ���
    public string rsound;//�ͷ�����
    public string
    rEffect;//����Ч��
    public int
    useway;//ʹ�÷���
    public int targetType;//Ŀ������
    public string skillIcon;//
    public string info;//
    public int coolDown; //
    public float range;
    public string absorbRes;
    public int isAbsorb;
    public int n32UpgradeLevel;
    public float yTime;//ʱ��
    public float mpUse;
    public float hpUse;
    public float cpUse;
}
//�洢��ؼ��ܶ�Ӧ����
