Int32 OnNetMse NotifySkillModelHitTarget(Stream stream)
    {
    StartCoroutine(OnNetMsg.NotifySkillModelHitTargetCoroutine(stream));
    return (Int32)FErrorCode.eNormal;
}
//在一个协程内的,创建特效是协程来创建
public Enumerator OnNetMsg NotifySkillModelHitTargetCoroutine(Stream stream)
{
    //解析消息
    GSTOGC.HitTar PMsg;
    if (!ProtoDes(out pMsg, stream))
    {
        yield break;
    }
    //创建特效
    UInt64 ownerID;
    ownerID = pMsg.guid;
    UInt64 targetID;
    targetID = pMsg.targuid;
    EventCenter.Broadcast‹UInt64, uint, UInt64> (EGamsExent..@GameEvent_BroadcastBeAtk, ownerID, RMsg, effectid, targetID);
    yield return 1;
    BlGame.Effect.EffectManager.Instance.CreateBeAttackEffect(ownerID, targetID, pMsg.effectid);
}
//在目标身上创建特效
//一切以服务器为准，客户端为表现
