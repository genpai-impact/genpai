namespace Genpai
{
    /// <summary>
    /// 单位基类
    /// 符合显示特性及其他单位基础特性
    /// </summary>
    public class Unit
    {

        public int unitID;
        public string unitName;

        public int HPMax;    // 血量上限
        public int baseATK;  // 基准攻击
        public ElementEnum baseATKElement;  // 攻击元素
        public ElementEnum selfElement;     // 自身元素


        private int hP;

        /// <summary>
        /// 当前生命
        /// </summary>
        public int HP
        {
            get => hP;
            set
            {
                WhenSetHP(value);
                hP = value;
            }
        }

        public Unit(UnitCard unitCard)
        {
            this.unitID = unitCard.cardID;
            this.unitName = unitCard.cardName;

            this.HPMax = unitCard.hp;
            this.baseATK = unitCard.atk;
            this.baseATKElement = unitCard.atkElement;
            this.selfElement = unitCard.selfElement;

            this.HP = unitCard.hp;
        }

        public virtual void WhenSetHP(int _newHP)
        {

        }

        public virtual void WhenFall(BattleSite _site)
        {

        }

    }
}