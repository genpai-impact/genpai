using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 可删除Buff接口
    /// </summary>
    public interface IBuffDeleteable
    {
        /// <summary>
        /// 删除Buff
        /// (注：如果完全删除，则自行从target对应buffAttachment中Remove)
        /// </summary>
        /// <param name="deleteStorey">删除层数</param>
        public void DeleteBuff(int deleteStorey = 0);
    }

    /// <summary>
    /// 可Buff的Buff接口
    /// </summary>
    public interface IBuffIncreasable
    {
        /// <summary>
        /// 通过输入增加层数方法
        /// </summary>
        /// <param name="storeys"></param>
        public void IncreaseBuff(int storeys = 0);

        /// <summary>
        /// 获取作为加法后位时的值
        /// </summary>
        /// <returns></returns>
        public int GetIncrease();

    }

}
