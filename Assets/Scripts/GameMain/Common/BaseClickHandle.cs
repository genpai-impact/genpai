
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
            Debug.Log("按下了鼠标左键，进入了动画检查。");
            if (!AnimationHandle.Instance.AllAnimationOver())
            {
                Debug.LogError("因为有还没完成的动画，所以鼠标单击命令未执行。");
                return;
            }
            DoGenpaiMouseDown();
        }

        abstract public void DoGenpaiMouseDown();
    }
}
