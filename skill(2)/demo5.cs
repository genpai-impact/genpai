//����Greate֮���޷������ˣ�û��Update������������JxBIGame��������
//Update is called once per frame
void Update()
{
    //����buff
    B1Game.Skill.BuffManager.Instance.Update();
    //������Ч
    B1Game.Effect.EffectManager.Instance.Updateself(����
//������ʾ��ʧ
MsgInfoManager.Instance.Update();
    //  ͬ�ϣ�����Update �������� EffectManager. Updateself0 :
    //��Чϵͳ���¡�
    public void UpdateSelf()
    {
        deadEffectList.Clean();
        //�����Ч�Ƿ�������
        foreach (var effect in m EffectMap.Values)
    {
    effect.Update();
    //��Ч�������߽��� object �����ⲿ�Գ�
    if (effect.isdead == true || effect.obj == null)
    {
        deadEffectList.Add(effect);
    }
}
  }
}
//�����Effect.Update0;(�����Ӷ�������

//�������ڽ�ɫû���ͷż��ܶ����������߼����������ٷ�һ��
//eMsgToGCFromGS NotifyGameOb jectRe leaseskillState
//�ý�ɫ�����ͷ�״̬
