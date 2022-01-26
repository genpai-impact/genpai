using System.Collections;
using UnityEngine;

namespace Genpai
{

    /// <summary>
    /// 单个卡牌管理器
    /// </summary>
    public class CardControler : MonoBehaviour
    {

        public float smooth = 2;//平滑移动系数
        bool isMoveTo = false;//移动控制器
        private Vector3 target;//移动目标

        // Use this for initialization
        private void Awake()
        {
            // 注册监听事件（订阅MoveTo类型消息）
            Messager.MessageManager.Instance.GetManager(Messager.MessageArea.Card).Subscribe<MoveToData>(Messager.MessageEvent.CardEvent.MoveTo, MoveTo);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (isMoveTo)
            {
                Vector3 temp = Vector3.Lerp(this.transform.localPosition, target, Time.deltaTime * smooth);
                this.transform.localPosition = temp;


                if (this.transform.localPosition.x <= target.x)
                    isMoveTo = false;
            }
        }

        /// <summary>
        /// 触发监听事件
        /// </summary>
        /// <param name="data">监听事件传入消息</param>
        public void MoveTo(MoveToData data)
        {
            //Debug.Log(".....................moveto"+data.target);
            if (this.gameObject == data.gameObject)
            {

                isMoveTo = true;
                this.target = data.target;
            };
        }

    }
}