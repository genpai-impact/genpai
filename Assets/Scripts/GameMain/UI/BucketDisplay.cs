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
        public PlayerSite ownerSite;      // 所属玩家
        public int serial;          // 格子序号（包含上俩信息）

        public Bucket bucket;       // 对应格子

        /// <summary>
        /// 根据GameObj确定的属性创建逻辑格子
        /// </summary>
        private void Awake()
        {
            bucket = new Bucket(ownerSite, serial);
        }

        /// <summary>
        /// 修改格子不同状态UI的展示
        /// </summary>
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

        /// <summary>
        /// 点击格子实现召唤确认
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (SummonManager.Instance.summonWaiting)
            {
                if (ownerSite == SummonManager.Instance.waitingPlayer.playerSite && bucket.unitCarry == null && !bucket.charaBucket)
                {
                    // 向召唤管理器发布消息：格子被点击，执行召唤确认
                    // Dispatch(MessageArea.Behavior, 0, this);
                }
            }
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }


        public void Execute(string eventCode, object message)
        {
            // 接收战场管理器UI消息：据不同消息对格子进行高亮
        }


        public void Subscribe()
        {
            // 订阅UI管理器
        }
    }
}