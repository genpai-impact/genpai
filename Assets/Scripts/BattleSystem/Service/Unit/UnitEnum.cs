namespace BattleSystem.Service.Unit
{
    public enum CardType
    {
        Chara,      // 角色，特殊单位
        Monster,    // 怪物，基准单位
        Boss        // Boss，特殊单位
    }

    public enum UnitState
    {
        ActiveAttack,       // 主动攻击
        CounterattackAttack,// 反击
        SkillUsing,         // 使用技能
        ChangeChara,        // 更换角色
    }
}