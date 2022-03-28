using Messager;

namespace Genpai
{
    public class ProcessScript : BaseClickHandle
    {

        public void Update()
        {
            AnimationHandle.Instance.AllAnimationOver();
        }

        public void EndRound()
        {
            GenpaiMouseDown();
        }

        public override void DoGenpaiMouseDown()
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
