using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Genpai
{
    /// <summary>
    /// ԭ�ƿ�����
    /// </summary>
    public class GenpaiController
    {
        /// <summary>
        /// �������
        /// </summary>
        public PlayerType PlayerType
        {
            get;
            set;
        }

        /// <summary>
        /// ��ǰ����Ƿ�ɽ��в���
        /// </summary>
        public static bool IsOperable
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʼ�����ߵĻغ�
        /// </summary>
        public void StartRound()
        {
            IsOperable = true;
        }

        /// <summary>
        /// ���������ߵĻغ�
        /// </summary>
        public void EndRound()
        {
            if (!IsOperable)
            {
                return;
            }
            IsOperable = false;
            // GameContext.processManager.EndRound(this);
        }
    }
}

