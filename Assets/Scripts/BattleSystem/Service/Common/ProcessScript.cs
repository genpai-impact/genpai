using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.UI;
using BattleSystem.Service.Process;
using DataScripts.DataLoader;
using Utils;
using Utils.Messager;
using BattleSystem.Controller;

namespace BattleSystem.Service.Common
{
    public class ProcessScript : BaseClickHandle
    {

        public void Update()
        {
            //AnimationHandle.Instance.AllAnimationOver();
        }

        public void EndRound()
        {
            SummonManager.Instance.SummonCancel();
            //ClickManager.CancelAllClickAction();//todo：留的小bug，上面一行删去换成这一行！！！
            GenpaiMouseDown();
            AudioManager.Instance.PlayerEffect("Play_RoundEnd");
        }

        protected override void DoGenpaiMouseDown()
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
            BucketEntityManager.Instance.Clean();
            NormalProcessManager.Instance.Restart();
        }
    }
}
