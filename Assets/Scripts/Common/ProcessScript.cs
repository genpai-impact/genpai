
using Messager;
using UnityEngine;


namespace Genpai
{
    public class ProcessScript : MonoBehaviour
    {
        string whoWin;
        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<bool>(MessageEvent.ContextEvent.BossFall, UseVicPage);
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<bool>(MessageEvent.ContextEvent.CharaFall, UseFailPage);
        }

        private void Awake()
        {
            Subscribe();

        }
        public void UseVicPage(bool _none)
        {
            whoWin = "CharaWin";
        }

        public void UseFailPage(bool _none)
        {
            whoWin = "BossWin";
        }
        public void EndRound()
        {
            Debug.Log("---------click----------------");

            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            NormalProcessManager.Instance.EndRound();
        }

        public void EndGame()
        {
            if (whoWin == "BossWin")
            {
                GameObject failEndGame = GameObject.Find("FailEndGame");
                failEndGame.SetActive(false);
            }
            else if (whoWin == "CharaWin")
            {
                GameObject vicEndGame = GameObject.Find("VicEndGame");
                vicEndGame.SetActive(false);
            }
            else
            {
                Debug.Log("玩家和boss都没有胜利！");
            }
            Debug.Log("GameEnd");
            NormalProcessManager.Instance.End();
        }

        public void Restart()
        {

            if (whoWin == "BossWin")
            {
                GameObject failEndGame = GameObject.Find("FailEndGame");
                failEndGame.SetActive(false);
            }
            else if (whoWin == "CharaWin")
            {
                GameObject vicEndGame = GameObject.Find("VicEndGame");
                vicEndGame.SetActive(false);
            }
            else
            {
                Debug.Log("玩家和boss都没有胜利！");
            }
            NormalProcessManager.Instance.Start();

        }
    }
}
