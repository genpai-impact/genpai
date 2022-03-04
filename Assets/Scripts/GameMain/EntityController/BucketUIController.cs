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
        //* public GameObject summonHighLight;
        //* public GameObject attackHighLight;

        //public Material TileMaterial;
        
        [ColorUsage(false,true,2,5,0.125f,3)]
        public Color colorInside;

        private float colorInsideStrength = 8.0f;
        [ColorUsage(false, true,2,5,0.125f,3)] 
        public Color colorOutside;
        private float colorOutsideStrength = 32.0f;
        
        
        Dictionary<string, Material> HighLightMaterial = new Dictionary<string, Material>();


        // 格子属性（由Sence定义）
        public BattleSite ownerSite;    // 所属玩家
        public int serial;              // 格子序号（包含上俩信息）


        public BucketEntity bucket;



        public void Init()
        {
            gameObject.AddComponent<BucketEntity>();
            gameObject.AddComponent<BucketPlayerController>();
            bucket = GetComponent<BucketEntity>();

            //* HighLightMaterial.Add("Attack", attackHighLight.GetComponent<SpriteRenderer>().material);
            //* HighLightMaterial.Add("Summon", summonHighLight.GetComponent<SpriteRenderer>().material);
            
            HighLightMaterial.Add("Attack", GetComponent<SpriteRenderer>().material);
            HighLightMaterial.Add("Summon", GetComponent<SpriteRenderer>().material);

            HighLightMaterial.Add("Idle", GetComponent<SpriteRenderer>().material);

        }


        void Awake()
        {
            Subscribe();
        }

        /// <summary>
        /// 修改格子不同状态UI的展示
        /// Idle    InsideColor(85,125,195,75) 2.5  OutsideColor(0,65,195,255) 5.0
        /// Attack  InsideColor(195,125,125,75) 2.5 OutsideColor(195,0,25,255) 5.0
        /// Summon  InsideColor(155,195,135,75) 2.5 OutsideColor(15,255,0,255) 5.0
        /// </summary>
        public void SetSummon()
        {
            if (GetComponent<SpriteRenderer>().material)
            {
                colorInside = new Color(155, 195, 135) * colorInsideStrength/255.0f;
                colorOutside = new Color(15, 255, 0) * colorOutsideStrength/255.0f;
                GetComponent<SpriteRenderer>().material.SetColor("_InsideColor",colorInside);
                GetComponent<SpriteRenderer>().material.SetColor("_OutsideColor",colorOutside);

            }
            
            //* GetComponent<SpriteRenderer>().material = HighLightMaterial["Summon"];
            // summonHighLight.SetActive(true);
        }

        public void SetAttack()
        {
            if (GetComponent<SpriteRenderer>().material)
            {
                colorInside = new Color(195, 125, 125)* colorInsideStrength/255.0f;
                colorOutside = new Color(195, 0, 25)* colorOutsideStrength/255.0f;
                GetComponent<SpriteRenderer>().material.SetColor("_InsideColor",colorInside);
                GetComponent<SpriteRenderer>().material.SetColor("_OutsideColor",colorOutside);

            }
            //GetComponent<SpriteRenderer>().material = HighLightMaterial["Attack"];
            // attackHighLight.SetActive(true);
        }

        public void SetIdle()
        {
            if (GetComponent<SpriteRenderer>().material)
            {

                colorInside = new Color(85, 125, 195)* colorInsideStrength/255.0f;
                colorOutside = new Color(0, 65, 195)* colorOutsideStrength/255.0f;
                GetComponent<SpriteRenderer>().material.SetColor("_InsideColor",colorInside);
                //GetComponent<SpriteRenderer>().material.SetInt("_InsideColor.Intensity",2.5);
                GetComponent<SpriteRenderer>().material.SetColor("_OutsideColor",colorOutside);

            }
            //GetComponent<SpriteRenderer>().material = HighLightMaterial["Idle"];
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
                GetComponent<BucketPlayerController>().summoning = true;
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
            GetComponent<BucketPlayerController>().summoning = false;

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