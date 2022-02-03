using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 管理格子UI高亮行为
    /// </summary>
    public class BucketUIController : MonoBehaviour, IMessageReceiveHandler
    {

        // UI连接
        public GameObject summonHighLight;
        public GameObject attackHighLight;
        Dictionary<string, Material> HighLightMaterial = new Dictionary<string, Material>();


        // 格子属性（由Sence定义）
        public BattleSite ownerSite;    // 所属玩家
        public int serial;              // 格子序号（包含上俩信息）


        public BucketEntity bucket;



        public void Init()
        {
            gameObject.AddComponent<BucketEntity>();
            gameObject.AddComponent<BucketReactionController>();
            bucket = GetComponent<BucketEntity>();

            HighLightMaterial.Add("Attack", attackHighLight.GetComponent<SpriteRenderer>().material);
            HighLightMaterial.Add("Summon", summonHighLight.GetComponent<SpriteRenderer>().material);
            HighLightMaterial.Add("Idle", GetComponent<SpriteRenderer>().material);

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
            GetComponent<SpriteRenderer>().material = HighLightMaterial["Summon"];
            // summonHighLight.SetActive(true);
        }

        public void SetAttack()
        {
            GetComponent<SpriteRenderer>().material = HighLightMaterial["Attack"];
            // attackHighLight.SetActive(true);
        }

        public void SetIdle()
        {
            GetComponent<SpriteRenderer>().material = HighLightMaterial["Idle"];
            // summonHighLight.SetActive(false);
            //attackHighLight.SetActive(false);
        }



        /// <summary>
        /// 设置高亮
        /// </summary>
        /// <param name="_serial">可高亮格子序号列表</param>
        public void SummonHighLight(List<bool> _serial)
        {
            if (!_serial[serial])
            {
                return;
            }
            else
            {
                SetSummon();
                GetComponent<BucketReactionController>().summoning = true;
            }
        }

        /// <summary>
        /// 设置高亮
        /// </summary>
        /// <param name="_serial">可高亮格子序号列表</param>
        public void AttackHighLight(List<bool> _serial)
        {
            if (!_serial[serial])
            {
                return;
            }
            else
            {
                SetAttack();
            }
        }

        /// <summary>
        /// 关闭高亮
        /// </summary>
        /// <param name="none">空参数</param>
        public void CancelHighLight(bool none)
        {
            //Debug.LogWarning("Cancelheightlight");

            SetIdle();
            GetComponent<BucketReactionController>().summoning = false;

        }


        public void Subscribe()
        {

            MessageManager.Instance.GetManager(MessageArea.UI)
                .Subscribe<List<bool>>(MessageEvent.UIEvent.SummonHighLight, SummonHighLight);

            MessageManager.Instance.GetManager(MessageArea.UI)
                .Subscribe<List<bool>>(MessageEvent.UIEvent.AttackHighLight, AttackHighLight);

            MessageManager.Instance.GetManager(MessageArea.UI)
                .Subscribe<bool>(MessageEvent.UIEvent.ShutUpHighLight, CancelHighLight);

        }
    }
}