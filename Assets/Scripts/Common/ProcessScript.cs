
using Messager;
using UnityEngine;

namespace Genpai
{
    public class ProcessScript : MonoBehaviour
    {
        public void EndRound()
        {
            // Debug.Log("click");

            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            NormalProcessManager.Instance.EndRound();
        }
    }
}
