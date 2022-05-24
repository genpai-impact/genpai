
using Messager;
using UnityEngine;

namespace Genpai
{
    public class ClickManager : Singleton<ClickManager>
    {
        public static void CancelAllClickAction()
        {
            SummonManager.Instance.SummonCancel();
            AttackManager.Instance.AttackCancel();
            MagicManager.Instance.MagicCancel();
            SpellManager.Instance.SpellCancel();
            SkillManager.Instance.SkillCancel();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }
    }
}
