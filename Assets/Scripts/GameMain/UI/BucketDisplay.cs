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
        private bool summoning = false;


        public void Init(PlayerSite _owner, int _serial)
        {
            bucket = new Bucket(_owner, _serial);
        }

        //高亮监听函数
        public void HeightLight(List<bool> _serial)
        {

            //Debug.LogWarning(bucket.serial);
            if (!_serial[bucket.serial])
            {
                return;
            }
            else
            {
                SetSummon();
                summoning = true;
            }
        }

        public void CancelHeightLight(bool none)
        {
            //Debug.LogWarning("Cancelheightlight");
            if (summoning)
            {
                SetIdle();
                summoning = false;
            }
        }



        void OnMouseEnter()
        {
            if (summoning)
            {
                transform.localScale = new Vector3(0.11f, 0.11f, 0.1f);

                SummonManager.Instance.waitingBucket = gameObject;
            }

        }


        void OnMouseExit()
        {

            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            SummonManager.Instance.waitingBucket = null;
        }


        /// <summary>
        /// 根据GameObj确定的属性创建逻辑格子
        /// </summary>
        void Awake()
        {
            Subscribe();


        }

        /// <summary>
        /// 修改格子不同状态UI的展示
        /// </summary>
        public void SetSummon()
        {
            summonHighLight.SetActive(true);
        }

        public void AfterSummon()
        {
            summonHighLight.SetActive(false);
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
        /// TODO：同CardControler实现
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (SummonManager.Instance.summonWaiting)
            {
                if (ownerSite == SummonManager.Instance.waitingPlayer.playerSite && bucket.unitCarry == null && !bucket.charaBucket)
                {
                    Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, SummonManager.Instance.waitingBucket);
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
            MessageManager.Instance.GetManager(MessageArea.UI)
                .Subscribe<List<bool>>(MessageEvent.UIEvent.SummonHighLight, HeightLight);
            //CancelHeightLight
            MessageManager.Instance.GetManager(MessageArea.Summon)
                .Subscribe<bool>("SummonEnd", CancelHeightLight);
        }
    }
}