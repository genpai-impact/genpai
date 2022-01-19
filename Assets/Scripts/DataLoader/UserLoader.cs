using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    /// <summary>
    /// 用户读取器，在内存中预存用户信息，用于主界面&登录
    /// 
    /// </summary>
    public class UserLoader : MonoSingleton<UserLoader>
    {
        private string path = "Data\\UserData";
        public List<int> ownCardIDList = new List<int>();  // 拥有的卡牌ID
        public int userLevel;//用int防止float精度问题
        public TextAsset userData; // 用户数据Json
        private string userPassword;
        public string userName;
        private int userId;
        private void Awake()
        {
            userData = Resources.Load(path) as TextAsset;
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
                this.ownCardIDList = userIdArray.ToObject<List<int>>();
            
        }

        

    }
}