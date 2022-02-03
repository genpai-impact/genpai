using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class ChangePlayer : MonoBehaviour
    {

        public BattleSite site = BattleSite.P1;
        public bool state = true;

        public GameObject player1;
        public GameObject player2;

        private void Awake()
        {

            player1.transform.position = new Vector3(0, 9.5f, -9.13397503f);
            player2.transform.position = new Vector3(1, 9.5f, -9.13397503f);

        }



        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求和目标选中
        /// </summary>
        /// <param name="data"></param>
        public void OnClick()
        {
            if (site == BattleSite.P1)
            {

                player1.transform.position = new Vector3(1, 9.5f, -9.13397503f);
                player2.transform.position = new Vector3(0, 9.5f, -9.13397503f);
                state = true;
                site = BattleSite.P2;
                Debug.Log("当前界面为" + site);
                return;
            }
            if (site == BattleSite.P2)
            {

                player1.transform.position = new Vector3(0, 9.5f, -9.13397503f);
                player2.transform.position = new Vector3(1, 9.5f, -9.13397503f);
                state = false;
                site = BattleSite.P1;
                Debug.Log("当前界面为" + site);
                return;
            }

        }

    }
}