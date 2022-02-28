using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class SpellPlayerController : MonoBehaviour, IMessageSendHandler
    {
        public SpellCard spellCard;
        public BattleSite playerSite;
        public UnitEntity chara;
        private void Awake()
        {

            InitTrigger();
        }
        /// <summary>
        /// 初始化鼠标事件触发器
        /// </summary>
        public void InitTrigger()
        {

            UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyOnMouseDown);
            EventTrigger.Entry myClick = new EventTrigger.Entry();
            myClick.eventID = EventTriggerType.PointerDown;
            myClick.callback.AddListener(click);

            UnityAction<BaseEventData> enter = new UnityAction<BaseEventData>(MyOnMouseEnter);
            EventTrigger.Entry myEnter = new EventTrigger.Entry();
            myEnter.eventID = EventTriggerType.PointerEnter;
            myEnter.callback.AddListener(enter);

            UnityAction<BaseEventData> exit = new UnityAction<BaseEventData>(MyOnMouseExit);
            EventTrigger.Entry myExit = new EventTrigger.Entry();
            myExit.eventID = EventTriggerType.PointerExit;
            myExit.callback.AddListener(exit);


            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            trigger.triggers.Add(myClick);
            trigger.triggers.Add(myEnter);
            trigger.triggers.Add(myExit);
        }

        /// <summary>
        /// 获取角色和魔法卡
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseEnter(BaseEventData data)
        {
            //Debug.Log("PointerEnter");
            spellCard = GetComponent<CardDisplay>().card as SpellCard;
            playerSite = GameContext.CurrentPlayer.playerSite;
            int index = (playerSite == BattleSite.P1) ? 5 : 12;
            chara = BattleFieldManager.Instance.bucketVertexs[index].unitCarry;
        }

        /// <summary>
        /// 鼠标移出,
        /// </summary>
        void MyOnMouseExit(BaseEventData data)
        {
        }
        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求
        /// </summary>
        /// <param name="data"></param>
        private void MyOnMouseDown(BaseEventData data)
        {
            Debug.Log("SpellCard Mouse Down");
            if (spellCard is DamageSpellCard)
            {
                // 发布攻击请求消息
                (UnitEntity, GameObject) magic = (chara, gameObject);
                MessageManager.Instance.Dispatch(MessageArea.Magic, MessageEvent.MagicEvent.AttackRequest, magic);
            }
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
        }
    }

}
