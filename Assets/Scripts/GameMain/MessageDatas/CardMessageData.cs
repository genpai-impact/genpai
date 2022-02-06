using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{

    // 该文件储存所有卡牌相关数据包

    /// <summary>
    /// MoveTo消息类型的数据包
    /// gameObject：指明哪个物体
    /// target：目标位置
    /// </summary>
    public class MoveToData : IMessageData
    {
        public GameObject gameObject;
        public Vector3 target;
        public MoveToData(GameObject obj, Vector3 vec)
        {
            gameObject = obj;
            target = vec;
        }
    }

}

