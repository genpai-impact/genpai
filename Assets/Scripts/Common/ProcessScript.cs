

using UnityEngine;

namespace Genpai
{
    public class ProcessScript : MonoBehaviour
    {
        public void EndRound()
        {
            Debug.Log("click");
            NormalProcessManager.Instance.EndRound();
        }
    }
}
