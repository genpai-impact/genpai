//�������ͻ��˼�����Ч�ͷ�

eMsgToGCFronGS Notifyskil IMode lEmitu
public IEnumerator OnNetMsg Notifyskillmode1EmitCoroutine(Stream stream)
    {
    //������Ϣ
    GSTOGC.EmitSkill pMsg;
    if (!ProtoDes(out pMsg, stream))
    {
        yield break;
    }
    UInt64 skillPlayerID = pMsg.guid;
    UInt64 skillTargetID = pMsg.targuid;
    Vector3 pos = this.ConvertPosToVector3(pMsg.tarpos);
    Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
    //������Ч��
    yield return 1;
}//�˴����Ҽӵ�}
FlyEffect effect =
B1Game.Effect.EffectManager.Instance.CreateFlyEffect(skillPlayerID, skillTargetID,
PMsg.effectid, (uint)pMsg.uniqueid, pos, dir, pMsg.ifAbsorbSkill);
//���м�����Ч��EffetcManager�������kk���ģ�
