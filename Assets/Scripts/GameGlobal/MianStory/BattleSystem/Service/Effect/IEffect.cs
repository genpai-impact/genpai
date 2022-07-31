namespace BattleSystem.Service.Effect
{
    /// <summary>
    /// 基础效果接口，通过继承其表示自身作为效果
    /// </summary>
    public interface IEffect
    {
        public Unit.Unit GetSource();
        public Unit.Unit GetTarget();
    }
}