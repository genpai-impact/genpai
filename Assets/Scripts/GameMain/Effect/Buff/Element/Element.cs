using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{

    /// <summary>
    /// Ԫ��Buff��
    /// Ԫ�ؽ��ڸ���ʱ��Buff��ʽ���н���
    /// </summary>
    public class Element : BaseBuff
    {

        /// <summary>
        /// Ԫ������
        /// </summary>
        public ElementEnum ElementType
        {
            set; get;
        }

        /// <summary>
        /// Ԫ����������֮����Ϊ������Ԫ��
        /// </summary>
        public bool ElementLock
        {
            set; get;
        }

        public Element(ElementEnum element)
        {
            this.ElementType = element;
        }

        /// <summary>
        /// ����Ԫ�ط�Ӧ
        /// </summary>
        /// <param name="element"></param>
        public void ElementReaction(Element element)
        {
            // todo ��ϸ�۲�Ԫ�ط�Ӧ�ĵ���������ô���Ԫ�ط�Ӧ
        }



    }
}

