using BattleSystem.Service.MessageDatas;
using UnityEngine;

namespace BattleSystem.Controller.EntityController
{

    /// <summary>
    /// 卡牌动画管理器
    /// 主要管理卡牌的移动动画
    /// </summary>
    public class CardAniController : MonoBehaviour
    {
        /// <summary>
        /// TODO：由HandCardManager动态分配位置
        /// </summary>
        public Vector3 targetPosition;      //移动目标位置
        private int _times;

        // Use this for initialization
        private void Awake()
        {
            _times = 0;
        }

        // Update is called once per frame
        // fixme 当一张手牌还没有完全移动好的时候，就再次点击，回手操作会让牌变得鬼畜。
        void FixedUpdate()
        {
            Vector3 origin = transform.localPosition;
            float x = 0.93f;
            if (_times++ >= 80) return;
            
            Vector3 temp = Vector3.Lerp(origin, targetPosition, -x+1);
            var localPosition = temp;
            localPosition += new Vector3(1,0,0) ;
            transform.localPosition = localPosition;
            x *= 0.93f;
        }

        /// <summary>
        /// 监听事件响应方法
        /// </summary>
        /// <param name="data">监听事件传入消息</param>
        public void MoveTo(MoveToData data)
        {
            
            if (gameObject == data.gameObject)
            {
                _times = 0;
                targetPosition = data.target;
            };
        }
    }
}