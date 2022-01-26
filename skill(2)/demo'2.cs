///如果技能类型对应的类型为null，直接 return
///< summary >
/// 准备释放技能。///
/// </ summary >
///<param name="skType"›Sk type„</ param>
///技能类型。
public void SendPreparePlaySkill(SkillType skType)
{
    int skillID = GetSkillIdBySkillType(skType);
    //沉默
    //此处删除了沉默的判定
    if (skillID == 0)
        MsgInfoManager.Instance.ShowMse((int)ERROR_TYPE.eT_AbsentSkillNULL);
    return;
    //发送技能 id到服务器
    CGLCtrl.GameLogic.Instance.EmsBToss_ AskUsesk ill((uint)skillID);

}

//调用实体的释放技能方法，发送给服务器，收到后开始模拟技能走向路线