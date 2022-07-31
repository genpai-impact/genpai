namespace BattleSystem.Service.Player.AI
{
    /// </summary>
    /// AI类型
    /// </summary>
    public enum AIType
    {
        SimpleAI,//暂定名
        FoolAI
    };

    /// <summary>
    /// AI基类
    /// </summary>
    public abstract class BaseAI 
    {
        public AIType AItype;
        public GenpaiPlayer Player;
        public int _currentRound = 0;

        public BaseAI(AIType _Type, GenpaiPlayer _Player)
        {
            AItype = _Type;
            Player = _Player;
        }

        public abstract void CharaStrategy();//上角色策略

        public abstract void MonsterStrategy();//上怪物策略

        public abstract void AttackStrategy();//攻击策略

        public abstract void MagicStrategy();//魔法卡策略

        public void EndRound()//结束回合
        {
            Player.GenpaiController.EndRound();
        }


        //其他策略
        //getname

        public virtual void Subscribe()
        {
        }
    }
}