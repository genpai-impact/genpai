using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 读取卡牌
/// </summary>
namespace Genpai
{
    public class Player
    {
        public int playerId;
        public PlayerType playerType;
        public string playerName;
        public int playerLevel;

        public List<int> ownCardIDList = new List<int>();  // 拥有的卡牌ID


        public Player(int _id, PlayerType _playerType, string _playerName, int _playerLevel, List<int> _cardsId)
        {
            this.playerId = _id;
            this.playerType = _playerType;
            this.playerName = _playerName;
            this.playerLevel = _playerLevel;
            this.ownCardIDList = _cardsId;
        }
    }

}
