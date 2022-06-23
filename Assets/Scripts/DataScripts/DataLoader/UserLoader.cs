using System.Collections.Generic;
using System.Linq;
using BattleSystem.Service.Player;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Utils;

namespace DataScripts.DataLoader
{
    /// <summary>
    /// 用户读取器，在内存中预存用户信息，用于主界面&登录
    /// 
    /// </summary>
    public class UserLoader : MonoSingleton<UserLoader>
    {
        private string path = "Data\\UserData";
        private string path1 = "Data\\UserData1";
        public List<int> ownCardIDList = new List<int>();  // 拥有的卡牌ID
        public int userLevel;//用int防止float精度问题
        public TextAsset userData; // 用户数据Json
        private string userPassword;
        public string userName;
        private int userId;
        public  Dictionary<int,int> cardInfo;//id+nums
      
        private void Awake()
        {
            cardInfo = new Dictionary<int, int>();
               userData = Resources.Load(path1) as TextAsset;
            //Debug.Log(Instance.userData.ToString());
            LoadUser();
            PlayerLoader.Instance.add(userId,PlayerType.InternetHuman,userName,userLevel,ownCardIDList);
        }

        /// <summary>
        /// 读取用户
        /// </summary>
        public void LoadUser()
        {
            
                JObject user = JObject.Parse(userData.text);

                // 读取基本角色信息
                this.userId = int.Parse(user["userID"].ToString());
                this.userName = user["userName"].ToString();
                this.userPassword = user["userPassword"].ToString();
                this.userLevel = int.Parse(user["userLevel"].ToString());

                JArray userIdArray = (JArray)user["userCardsId"];
            foreach(JObject jb in userIdArray)
            {
               
                int cardId = int.Parse(jb["id"].ToString());
                int carnNums = int.Parse(jb["nums"].ToString());
                this.cardInfo.Add(cardId, carnNums);

            }
            this.ownCardIDList = cardInfo.Keys.ToList();
            
        }

        

    }
}