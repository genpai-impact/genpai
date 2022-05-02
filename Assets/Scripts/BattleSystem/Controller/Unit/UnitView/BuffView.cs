using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// Buff������ͼ��������Ϣ
    /// </summary>
    public class BuffView
    {
        public BuffEnum buffName;
        public BuffType buffType;
        public Image buffImage;
        public int lifeCycles;  // Buff��������
        public int storey;      // Buff����

        public BuffView(BaseBuff _baseBuff)
        {
            buffName = _baseBuff.buffName;
            buffType = _baseBuff.buffType;

        }

        public string ReturnDescription()
        {
            string ret = "�Ų�ľ��J���KBuff�ھŻغϺ���ʧ";
            return ret;
        }
    }
}