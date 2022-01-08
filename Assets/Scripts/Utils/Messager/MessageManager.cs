using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messager
{
    /// <summary>
    /// ��Ϣ���ģ����ڽ��ղ�����������Ϣ
    /// </summary>
    public class MessageManager : MonoSingleton<MessageManager>, IMessageSendHandler
    {
        private Dictionary<MessageArea, AreaMessageManager> managers = new Dictionary<MessageArea, AreaMessageManager>();

        private void Awake()
        {
            // �Զ�ѭ�����б���������
            foreach (MessageArea area in System.Enum.GetValues(typeof(MessageArea)))
            {
                CreateAreaManager(area).Subscribe();
            }

        }

        /// <summary>
        /// �������������������������ʱ���ã�
        /// </summary>
        /// <param name="areaCode">����</param>
        /// <param name="areaManager">������</param>
        public void AddAreaManager(MessageArea areaCode, AreaMessageManager areaManager)
        {
            managers.Add(areaCode, areaManager);

        }

        public AreaMessageManager CreateAreaManager(MessageArea areaCode)
        {
            return new AreaMessageManager(areaCode);
        }

        public AreaMessageManager GetManager(MessageArea areaCode)
        {
            if (managers.ContainsKey(areaCode))
            {
                return managers[areaCode];
            }
            else
            {
                // Debug.Log("�����ڹ�������" + areaCode.ToString());
                return null;
            }

        }

        /// <summary>
        /// ����Ϣ�ַ����������
        /// </summary>
        /// <param name="areaCode">����������</param>
        /// <param name="eventCode">�����¼����</param>
        /// <param name="message">��Ϣ</param>
        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            GetManager(areaCode).Execute(eventCode, message);
        }


    }

    /// <summary>
    /// ����Ϣ�����������ڷ���������Ϣ
    /// </summary>
    public class AreaMessageManager : IMessageReceiveHandler
    {
        // ������Ϣ��
        protected MessageArea areaCode;
        // ����������<��Ϣ��,�����б�>
        private Dictionary<int, List<IMessageHandler>> ListenerDict;

        public AreaMessageManager(MessageArea _areaCode)
        {
            this.areaCode = _areaCode;
        }



        /// <summary>
        /// ���Լ�������ն˷ַ���Ϣ
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
        public void Execute(int eventCode, object message)
        {
            if (ListenerDict.ContainsKey(eventCode))
            {
                foreach (var listener in ListenerDict[eventCode])
                {
                    listener.Execute(eventCode, message);
                }
            }
        }

        /// <summary>
        /// ����Ϣ���Ķ���
        /// </summary>
        public void Subscribe()
        {
            MessageManager.Instance.AddAreaManager(this.areaCode, this);
        }


    }

}

