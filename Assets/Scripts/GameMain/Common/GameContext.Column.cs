namespace Genpai
{

    /// <summary>
    /// 存储整个游戏的上下文信息
    /// 本文件只存字段
    /// </summary>
    public partial class GameContext
    {
        /// <summary>
        /// 玩家1
        /// </summary>
        public static GenpaiPlayer Player1
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家2
        /// </summary>
        public static GenpaiPlayer Player2
        {
            get;
            set;
        }

        /// <summary>
        /// 当前是哪个玩家行动
        /// </summary>
        public static GenpaiPlayer CurrentPlayer
        {
            get;
            set;
        }

        /// <summary>
        /// BOSS信息
        /// </summary>
        public static Boss OneBoss
        {
            get;
            set
            {
                if(OneBoss.HP > 0.75 * OneBoss.HP)
                {
                    if(value.HP > 0.5 * OneBoss.HP)
                    {
                        for (int i = 0; i < 1; i++)
                        {//后续可能更改的话则将1改为相应变量,以下同理
                            this.Player1.CardDeck.DrawHero();
                            this.Player2.CardDeck.DrawHero();
                        }
                    }
                    else if(value.HP <= 0.5 * OneBoss.HP)
                    {
                        for(int i = 0; i < 2; i++)
                        {
                            this.Player1.CardDeck.DrawHero();
                            this.Player2.CardDeck.DrawHero();
                        }
                    }
                }
                else if(OneBoss.HP > 0.5 * OneBoss.HP)
                {
                    else if (value.HP <= 0.5 * OneBoss.HP)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            this.Player1.CardDeck.DrawHero();
                            this.Player2.CardDeck.DrawHero();
                        }
                    }
                }
                this.OneBoss = value;
            }
        }

        /// <summary>
        /// 战场信息
        /// </summary>
        public static BattleFieldManager BattleField = BattleFieldManager.Instance;

        /// <summary>
        /// 流程管理
        /// </summary>
        public static NormalProcessManager processManager = NormalProcessManager.Instance;
    }
}
