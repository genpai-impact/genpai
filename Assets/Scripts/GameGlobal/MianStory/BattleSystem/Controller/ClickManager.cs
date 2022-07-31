using Utils;
using Utils.Messager;

namespace BattleSystem.Controller
{
    public class ClickManager : Singleton<ClickManager>
    {
        public static void CancelAllClickAction()
        {
            SummonManager.Instance.SummonCancel();
            AttackManager.Instance.AttackCancel();
            SpellManager.Instance.SpellCancel();
            SkillManager.Instance.SkillCancel();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }
    }
}
