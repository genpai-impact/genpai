
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 管理所有点击事件
    /// </summary>
    public abstract class BaseClickHandle : MonoBehaviour
    {
        public void GenpaiMouseDown()
        {
          //  Debug.Log("按下了鼠标左键，进入了动画检查。");
            if (!AnimationHandle.Instance.AllAnimationOver())
            {
                Debug.LogWarning("请在动画结束后执行操作");
                return;
            }
            DoGenpaiMouseDown();
        }

        protected abstract void DoGenpaiMouseDown();
    }
}
