using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    /// <summary>
    /// 玩家读取器，在内存中预存所有玩家（本用户和NPC）用于对抗
    /// （数据转换由项目根目录DataScripts/JsonConvert.ipynb实现，修改卡牌类记得匹配修改转换脚本）
    /// </summary>
    public class PlayerLoader :MonoSingleton<PlayerLoader> 
    {
        
        private string path = "Data\\PlayerData";
        public Hashtable playerList = new Hashtable();    // 玩家&NPC数据哈希表
        public List<int> playerIDList = new List<int>();  // 测试用（id不连续
        public List<int> cardIDList = new List<int>();
        public TextAsset playerData; // 卡牌数据Json

        

        public void Awake()
        {
            
            PlayerLoader.Instance.playerData = Resources.Load(path) as TextAsset;
            //Debug.Log(Instance.playerData.ToString());
            PlayerLoader.Instance.LoadPlayer();
        }

        private void LoadPlayer() {
            
            
            JArray playerArray = JArray.Parse(playerData.text);
            foreach (var item in playerArray)
            {
                JObject player = (JObject)item;

                // 读取基本卡牌信息
                int id = int.Parse(player["playerID"].ToString());
                int playerLevel = int.Parse(player["playerLevel"].ToString());
                string playerName = player["playerName"].ToString();
                PlayerType playerType = (PlayerType)System.Enum.Parse(typeof(PlayerType), player["playerType"].ToString()); 
                

                JArray ownCardArray = (JArray)player["playerCardsId"];
                cardIDList = ownCardArray.ToObject<List<int>>();
                playerList.Add(id, new Player(id, playerType, playerName, playerLevel, cardIDList));
                playerIDList.Add(id); // 测试用
            }
        }

        /// <summary>
        /// 读取玩家
        /// </summary>
        

        public void add(int _id, PlayerType _playerType, string _playerName, int _playerLevel, List<int> _cardsId)
        {
            playerList.Add(_id, new Player(_id,_playerType,_playerName,_playerLevel,_cardsId));
        }

        public void remove(int _id)
        {
            playerList.Remove(_id);
        }
        public Player GetPlayById(int _id)
        {
            return (Player)playerList[_id];
        }

    }
}