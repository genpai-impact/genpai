Int32 OnNetMsg_NotifyReleaseSkill(Stream stream)
{
    GSTOGC.ReleasingSkillState PMsg;
    if (!ProtoDes(out pMsg, stream))
    {
        return PROTO_DESERIALIZE_ERROR;
    }
    Vector3 pos = this.ConvertPosToVector3(pMSg.pos);
    Vector3 dir = this.ConvertDirToVester3(pMsg.dir);
    dir.y = 0.0f;
    UInt64 targetID;
    targetID = pMsg.targuid;
    UInt64 sGUID;
    sGUID = pMsg.objguid;
    Ientity target;
    EntityManager.AllEntitys.TryGetValue(targetID, out target);
    Ientity entity;
    if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
    {
        pos.y = entity.realObject.transform.position.y;
        entity.EntityFSMChangeDataOnPrepareSkill(pos, dir, pMsg skillid, target);
        entity.OnFSMStateChange(EntityReleaseSkillFSM.Instance);
    }
    return (Int32)EEnnarCade.eNormal;
}
//1.技能释放人物坐标2.技能释放人物方向3.技能名称4.目标名称
