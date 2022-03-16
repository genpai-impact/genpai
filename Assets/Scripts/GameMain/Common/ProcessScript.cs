
using Messager;
using UnityEngine;


namespace Genpai
{
    public class ProcessScript : MonoBehaviour
    {
        public void EndRound()
        {
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            GameContext.CurrentPlayer.GenpaiController.EndRound();
        }

        public void EndGame()
        {
            
            NormalProcessManager.Instance.End();
        }

        public void Restart()
        {
            
            UserLoader.Instance.Clean();
            ScoringBroad.Instance.Clean();
            PlayerLoader.Instance.Clean();
            BattleFieldManager.Instance.Clean();
            NormalProcessManager.Instance.Restart();
        }
    }
}
