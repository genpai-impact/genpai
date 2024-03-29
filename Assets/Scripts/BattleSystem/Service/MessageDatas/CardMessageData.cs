﻿using BattleSystem.Service.Player;
using UnityEngine;
using Utils.Messager;

namespace BattleSystem.Service.MessageDatas
{

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

    public class BossScoringData : IMessageData
    {
        public BattleSite site;
        public int score;

        public BossScoringData(BattleSite _site, int _score)
        {
            site = _site;
            score = _score;
        }
    }

}

