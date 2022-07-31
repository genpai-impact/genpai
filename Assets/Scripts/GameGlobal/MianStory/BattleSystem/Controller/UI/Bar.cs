using BattleSystem.Service.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem.Controller.UI
{
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
    
        //public RectTransform flowBar;
        //滑块装饰
        //public RectTransform barPos;
        //血条的位置信息
    

        public void Update()
        {
            // 使用HpDisplay，即用于展示的血量（其实算历史遗留问题，
            SetBarUI(GameContext.TheBoss.HpDisplay);
            
            //Debug.Log(GameContext.TheBoss.HP); 
        }
    
        /// <summary>
        /// 需要一个初始的血量，和当前的血量
        /// 
        /// </summary>
        public void SetBarUI(float newHp)
        {
            //这里需要修改
            const float startingHp = 100.0f;
            //初始血量
            healthBar.fillAmount = newHp / startingHp;
            if (bufferBar.fillAmount > healthBar.fillAmount)
            {
                bufferBar.fillAmount -= 0.0004f;
            }
            else
            {
                bufferBar.fillAmount = healthBar.fillAmount;
            }
            //血条UI，背景，缓冲，红
        }
    }
}
