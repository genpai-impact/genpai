using System.Collections.Generic;
using BattleSystem.Service.Player;


    /// <summary>
    /// 玩家数据
    /// </summary>
    public class User
    {
        public int playerId;
        
        public string playerName;
        public int playerLevel;

        public List<int> ownCardIDList = new List<int>();  // 拥有的卡牌ID列表


        public User(int _id, string _playerName, int _playerLevel, List<int> _cardsId)
        {
            this.playerId = _id;
            
            this.playerName = _playerName;
            this.playerLevel = _playerLevel;
            this.ownCardIDList = _cardsId;
        }
    }


