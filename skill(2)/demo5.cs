//技能Greate之后无法动不了，没有Update里面驱动，在JxBIGame里面驱动
//Update is called once per frame
void Update()
{
    //更新buff
    B1Game.Skill.BuffManager.Instance.Update();
    //更新特效
    B1Game.Effect.EffectManager.Instance.Updateself(）；
//更新提示消失
MsgInfoManager.Instance.Update();
    //  同上，他在Update 里面驱动 EffectManager. Updateself0 :
    //特效系统更新。
    public void UpdateSelf()
    {
        deadEffectList.Clean();
        //检查特效是否死亡。
        foreach (var effect in m EffectMap.Values)
    {
    effect.Update();
    //特效死亡或者綁定 object 对象被外部冊除
    if (effect.isdead == true || effect.obj == null)
    {
        deadEffectList.Add(effect);
    }
}
  }
}
//最后让Effect.Update0;(让粒子动起来）

//以上由于角色没有释放技能动作，不和逻辑，服务器再发一条
//eMsgToGCFromGS NotifyGameOb jectRe leaseskillState
//让角色进入释放状态
