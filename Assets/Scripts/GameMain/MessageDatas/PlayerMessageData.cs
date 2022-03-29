using System.Collections;
using System.Collections.Generic;
using Messager;
using UnityEngine;

/// <summary>
/// 存储战斗中的回合数
/// </summary>
public class RoundData : IMessageData
{ 
    public int RoundNum;

    public void RoundNumberData(int _roundNum)
    {
        RoundNum = _roundNum;
    }
}
