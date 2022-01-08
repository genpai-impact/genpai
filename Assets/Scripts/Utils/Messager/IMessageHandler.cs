using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Messager
{
    public interface IMessageSendHandler
    {
        /// <summary>
        /// ʵ�֣�MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        /// </summary>
        public void Dispatch(MessageArea areaCode, int eventCode, object message);
    }

    public interface IMessageReceiveHandler
    {
        // ������Ϣ
        public void Execute(int eventCode, object message);

        // ����Ϣ���Ķ�����Ϣ��ȷ������������
        public void Subscribe();
    }
    public interface IMessageHandler : IMessageSendHandler, IMessageReceiveHandler
    {

    }

}

