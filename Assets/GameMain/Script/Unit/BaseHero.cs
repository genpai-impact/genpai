namespace Genpai
{
    public abstract class BaseHero : BaseUnit
    {
        public abstract HeroStatusEnum GetHeroStatus();
        public abstract void SetHeroStatus(HeroStatusEnum Status);
    }
}
