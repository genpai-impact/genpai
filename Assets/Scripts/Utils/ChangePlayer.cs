using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class  ChangePlayer : MonoBehaviour
    {

        public BattleSite site = BattleSite.P1;
        public bool state = true;

        public GameObject player1;
        public GameObject player2;

        private void Awake()
        {

            player1.transform.position = new Vector3(0, 0, 0);
            player2.transform.position = new Vector3(1920, 0, 0);

        }



        /// <summary>
        /// 鼠标点击事件触发方法
        /// 改变本地玩家布局
        /// </summary>
        /// <param name="data"></param>
        public void OnClick() 
        { 
            ChangeLocalPlayer();
        }

        /// <summary>
        /// 改变本地玩家布局
        /// </summary>
        public void ChangeLocalPlayer()
        {
            if (site == BattleSite.P1)
            {

                player1.transform.position = new Vector3(1920, 0, 0);
                player2.transform.position = new Vector3(0, 0, 0);
                state = true;
                site = BattleSite.P2;
                Debug.Log("当前界面为" + site);
                return;
            }
            if (site == BattleSite.P2)
            {

                player1.transform.position = new Vector3(0, 0, 0);
                player2.transform.position = new Vector3(1920, 0, 0);
                state = false;
                site = BattleSite.P1;
                Debug.Log("当前界面为" + site);
                return;
            }

        }

    }
}