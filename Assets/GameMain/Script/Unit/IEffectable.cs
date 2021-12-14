namespace Genpai
{
    /// <summary>
    /// 可以作为作战单位生成地
    /// </summary>
    public interface IEffectable
    {
        public void TakeEffect(BaseEffect effect);
    }
}