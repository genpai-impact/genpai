
using Messager;
using UnityEngine;


namespace Genpai
{
    public class ProcessScript : MonoBehaviour
    {
        int Winner;
        private const int _bossWin = 1;
        private const int _charaWin = 2;
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
            Winner = _charaWin;
        }

        public void UseFailPage(bool _none)
        {
            Winner = _bossWin;
        }
        public void EndRound()
        {
            // Debug.Log("click");

            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            GameContext.CurrentPlayer.GenpaiController.EndRound();
        }

        public void EndGame()
        {
            if (Winner == _bossWin)
            {
                GameObject failEndGame = GameObject.Find("FailEndGame");
                failEndGame.SetActive(false);
            }
            else if (Winner == _charaWin)
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

            if (Winner == _bossWin)
            {
                GameObject failEndGame = GameObject.Find("FailEndGame");
                failEndGame.SetActive(false);
            }
            else if (Winner == _charaWin)
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
