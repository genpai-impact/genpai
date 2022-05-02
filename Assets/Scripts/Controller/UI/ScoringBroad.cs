using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messager;

namespace Genpai
{
    public class ScoringBroad : MonoSingleton<ScoringBroad>, IMessageReceiveHandler
    {
        public Text P1ScoreText;
        public Text P2ScoreText;

        public int P1Score = 0;
        public int P2Score = 0;

        public Image healthBar;
        //血条UI
        public Image startingBar;
        //初始血量UI
        public Image bufferBar;
        //缓冲UI
        public void Init()
        {
            Subscribe();
            UpdateDisplay();
        }

        public void UpdateScore(BossScoringData data)
        {
            if (data.site == BattleSite.P1)
            {
                P1Score += data.score;
            }
            else
            {
                P2Score += data.score;
            }
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            P1ScoreText.text = P1Score.ToString();
            P2ScoreText.text = P2Score.ToString();
        }

        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Context)
                .Subscribe<BossScoringData>(MessageEvent.ContextEvent.BossScoring, UpdateScore);
        }

        /// <summary>
        /// 需要一个初始的血量，和当前的血量
        /// 设置血量的方法的返回值是void，
        /// 我没能拿到这个值，
        /// 有没有其他办法呢？
        /// </summary>
        public void SetBarUI()
        {
            float newHp = 0.5f;
            //当前血量,需要从外界实时获得血量值，我暂时没找到这个值
            //这里需要修改
            float startingHP = 1.0f;
            //初始血量，且先设置为1吧
            healthBar.fillAmount = newHp / startingHP;
            if (bufferBar.fillAmount > healthBar.fillAmount)
            {
                bufferBar.fillAmount -= 0.03f;
            }
            else
            {
                bufferBar.fillAmount = healthBar.fillAmount;
            }
        }
    }
}