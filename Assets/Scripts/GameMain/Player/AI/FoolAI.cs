namespace Genpai
{
    public class FoolAI : BaseAI//do nothing，试验使用
    {
        public FoolAI(AIType _Type, GenpaiPlayer _Player) : base(_Type, _Player) { }

        public override void CharaStrategy() { }//上角色策略

        public override void MonsterStrategy() { }//上怪物策略

        public override void AttackStrategy() { }//攻击策略
    }
}