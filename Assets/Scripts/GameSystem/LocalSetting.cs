using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameSystem
{
    public class LocalSetting : MonoBehaviour
    {

        //是否是暂停状态
        public bool isPaused = true;
        //设置界面名称
        public GameObject menuGO;

        public Text messageText;

        //如果有加速倍数需求可以加，目前不实装


        public void ShowMessage(string str)
        {
            messageText.text = str;
        }

        /// <summary>
        /// 返回选关或者说是退出游戏
        /// </summary>
        public void BackButton()
        {
            //输入场景名字，可以修改
            SceneManager.LoadScene("选关界面");
        }

        private void Update()
        {
            //判断是否按下ESC键，按下的话，调出Menu菜单，并将游戏状态更改为暂停状态
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

        //暂停状态
        private void Pause()
        {
            isPaused = true;
            menuGO.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        //非暂停状态
        private void UnPause()
        {
            isPaused = false;
            menuGO.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = true;
        }

        //从暂停状态恢复到非暂停状态
        public void ContinueGame()
        {
            UnPause();
            ShowMessage("");
        }


    }
}
