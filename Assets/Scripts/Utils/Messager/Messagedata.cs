using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Genpai
{
    /// <summary>
    /// ��ͬ����������Ϣ�ӿڷ�װ
    /// </summary>
    public interface IMessageData
    {

    }

    /// <summary>
    /// ���ͼ����¼����¼��Ĳ���Ϊ T <����>��
    /// </summary>
    public class MessageData<T> : IMessageData
    {
        public UnityAction<T> MessageEvents;
        public MessageData(UnityAction<T> action)
        {
            MessageEvents += action;
        }
    }
}
