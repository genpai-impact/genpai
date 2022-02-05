using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// ��ɾ��Buff�ӿ�
    /// </summary>
    public interface IBuffDeleteable
    {
        /// <summary>
        /// ɾ��Buff
        /// (ע�������ȫɾ���������д�target��ӦbuffAttachment��Remove)
        /// </summary>
        /// <param name="deleteStorey">ɾ������</param>
        public void DeleteBuff(int deleteStorey = 0);
    }

    /// <summary>
    /// ��Buff��Buff�ӿ�
    /// </summary>
    public interface IBuffIncreasable
    {
        /// <summary>
        /// ͨ���������Ӳ�������
        /// </summary>
        /// <param name="storeys"></param>
        public void IncreaseBuff(int storeys = 0);

        /// <summary>
        /// ��ȡ��Ϊ�ӷ���λʱ��ֵ
        /// </summary>
        /// <returns></returns>
        public int GetIncrease();

    }

}
