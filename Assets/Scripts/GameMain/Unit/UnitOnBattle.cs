using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位战场行为
    /// 选择需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d99e47517a81003fd02bdc
    /// </summary>
    public class UnitOnBattle : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {
        public Unit unit;
        public void OnPointerDown(PointerEventData eventData)
        {
            // 如果单位可行动
            if (true)
            {
                // 点击发起攻击请求()
            }

        }
        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}