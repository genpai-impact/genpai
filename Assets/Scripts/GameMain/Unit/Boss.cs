using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Boss : Unit, IMessageSendHandler
    {

        public Boss(UnitCard unitCard, PlayerID _owner) : base(unitCard, _owner)
        {
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            // ����ʱ���͸��¼Ʒ�����Ϣ������ʱ������Ϸ������Ϣ
        }
    }
}