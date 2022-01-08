using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// ��Ϸ�е������Ϣ
    /// </summary>
    public class GenpaiPlayer
    {
        /// <summary>
        /// ������
        /// </summary>
        public GenpaiController GenpaiController
        {
            get;
            set;
        }

        /// <summary>
        /// ��ҵĿ���
        /// </summary>
        public CardDeck CardDeck
        {
            get;
            set;
        }

        /// <summary>
        /// ��ǰ�ǵڼ��غ�
        /// </summary>
        public int CurrentRound
        {
            get;
            set;
        }
    }
}
