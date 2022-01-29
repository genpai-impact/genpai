//发送至客户端技能特效释放

eMsgToGCFronGS Notifyskil IMode lEmitu
public IEnumerator OnNetMsg Notifyskillmode1EmitCoroutine(Stream stream)
    {
    //解析消息
    GSTOGC.EmitSkill pMsg;
    if (!ProtoDes(out pMsg, stream))
    {
        yield break;
    }
    UInt64 skillPlayerID = pMsg.guid;
    UInt64 skillTargetID = pMsg.targuid;
    Vector3 pos = this.ConvertPosToVector3(pMsg.tarpos);
    Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
    //创建特效。
    yield return 1;
}//此处是我加的}
FlyEffect effect =
B1Game.Effect.EffectManager.Instance.CreateFlyEffect(skillPlayerID, skillTargetID,
PMsg.effectid, (uint)pMsg.uniqueid, pos, dir, pMsg.ifAbsorbSkill);
//所有技能特效在EffetcManager里面管理（kk更改）
