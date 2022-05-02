using System;
using System.Collections;
using System.Collections.Generic;
using GalForUnity.System;
using Genpai;
using UnityEngine;
using UnityEngine.UI;
using Messager;


/// <summary>
/// 写一下血条UI
/// </summary>
public class Bar : MonoBehaviour 
{
    public Image healthBar;
    //当前血量的血条
    public Image startingBar;
    //初始血条，背景
    public Image bufferBar;
    //缓冲血条

    //private Boss theBoss;
    

    public void Update()
    {
        SetBarUI(GameContext.TheBoss.HP);
        //Debug.Log(GameContext.TheBoss.HP); 
    }
    
    /// <summary>
    /// 需要一个初始的血量，和当前的血量
    /// 
    /// </summary>
    public void SetBarUI(float _newHP)
    {
        float newHp = _newHP;
        //
        //这里需要修改
        float startingHP = 100.0f;
        //初始血量，且先设置为1吧
        healthBar.fillAmount = newHp / startingHP;
        if (bufferBar.fillAmount > healthBar.fillAmount)
        {
            bufferBar.fillAmount -= 0.01f;
        }
        else
        {
            bufferBar.fillAmount = healthBar.fillAmount;
        }
        //血条UI，背景，缓冲，红
    }
}
