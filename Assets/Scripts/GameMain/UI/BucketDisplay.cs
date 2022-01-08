using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 格子UI行为
    /// </summary>
    public class BucketDisplay : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {

        // UI连接
        public GameObject summonHighLight;
        public GameObject attackHighLight;

        // 格子属性
        public PlayerID owner;      // 所属玩家
        public int serial;          // 格子序号（包含上俩信息）

        public Bucket bucket;       // 对应格子

        private void Awake()
        {
            bucket = new Bucket(owner, serial);
        }
        // 单位获取
        // public GameObject unitCarry;

        public void SetSummon()
        {
            summonHighLight.SetActive(true);
        }

        public void SetAttack()
        {
            attackHighLight.SetActive(true);
        }

        public void SetIdle()
        {
            summonHighLight.SetActive(false);
            attackHighLight.SetActive(false);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (SummonManager.Instance.summonWaiting)
            {
                if (owner == SummonManager.Instance.waitingPlayer && bucket.unitCarry == null && !bucket.charaBucket)
                {
                    // 向召唤管理器发布消息：格子被点击，执行召唤确认
                    Dispatch(MessageArea.Behavior, 0, this);
                }
            }
        }


        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }


        public void Execute(int eventCode, object message)
        {
            // 接收战场管理器UI消息：据不同消息对格子进行高亮
        }


        public void Subscribe()
        {
            // 订阅UI管理器
        }
    }
}