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
    public class BucketUIController : MonoBehaviour, IMessageReceiveHandler
    {

        // UI连接
        public GameObject summonHighLight;
        public GameObject attackHighLight;

        // 格子属性
        public BattleSite ownerSite;    // 所属玩家
        public int serial;              // 格子序号（包含上俩信息）

        public BucketEntity bucket;           // 对应格子

        public bool summoning = false;


        public void Init()
        {
            gameObject.AddComponent<BucketEntity>();
            gameObject.AddComponent<BucketReactionController>();
            bucket = GetComponent<BucketEntity>();
        }


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

        public void SetAttack()
        {
            attackHighLight.SetActive(true);
        }

        public void SetIdle()
        {
            summonHighLight.SetActive(false);
            attackHighLight.SetActive(false);
        }



        //高亮监听函数
        public void HeightLight(List<bool> _serial)
        {
            if (!_serial[serial])
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


        public void Subscribe()
        {

            MessageManager.Instance.GetManager(MessageArea.UI)
                .Subscribe<List<bool>>(MessageEvent.UIEvent.SummonHighLight, HeightLight);
            MessageManager.Instance.GetManager(MessageArea.UI)
                .Subscribe<bool>(MessageEvent.UIEvent.ShutUpHighLight, CancelHeightLight);

        }
    }
}