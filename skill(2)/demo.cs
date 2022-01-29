///< summary >
/// 按下技能按钮。
///</ summary >
///<param name = "ie" > 技能对应的 id< /param>
/// <pamam name="isDown" ></pamam></paman>
private void OnSkillBtnFunc(int ie, bool isDown)
{

    if (mIsShowDes)
    {
        mIsShowDes = false;
        return;
    }
    mIsShowDes = false;
    GamePlayCtrl.Instance.showaudiotimeold = System.DateTime.Now;

    if (PlayerManager.Instance.LocalPlayer.FSM == null ||
    PlayerManager.Instance.LocalPlayer.FSM.State ==
    B1Game.FSM.FsmState.FSM STATE DEAD )
            //此处把沉默的判断删除了，因为目前没有这个技能
    {
    return;
}
SendSkill(ie);
}
//判断是否不是死亡和沉默状态，如果是值计入return。
//然后SendSKill（ie）发送释放技能到服务器
//使用技能
private void SendSkill(int btn)
{

    if (PlayerManager.Instance.LocalPlayer.FSM.State ==
    B1Game.FSM.FsmState.FSM STATE DEAD)
return;
SkillType type = GetSkillType(btn);
if (type == SkillType.SKILL_NULL)
{
    return;
}
PlayerManager.Instance.LocalPlayer.SendPreparePlaySkill(type);
    }
//如果技能类型对应为null，直接return
