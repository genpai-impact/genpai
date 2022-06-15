
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 上下文脚本
    /// </summary>
    public class GameContextScript : MonoBehaviour
    {
        public void Start()
        {
            LubanLoader.Init();
            
            Debug.Log("game context is " + GameContext.Instance);

            Fresh();

            BattleFieldManager.Instance.Init();

            CardLibrary.Instance.LoadFormFile();
            GameContext.Instance.Init();
            ScoringBroad.Instance.Init();
            SummonManager.Init();
            AttackManager.Instance.Init();
            HittenNumManager.Instance.Init();
            NormalProcessManager.Instance.Start();

        }
        
        // 在游戏开始之初删除了一些manager中存储的状态
        // 其中上半部分是有进行实质删除的，下半部分只是为了形式上的统一
        // 这里只有部分manager，实际上所有单例都存在这个隐患，应该要统一处理一遍（但当时并没有这个意识，现在想来着实痛苦
        // 所以其实是最初有考虑到统一管理单例所以加了这个script，但是后来就忘记管了吗
        public void Fresh()
        {
            // 删除了manager中存储的状态
            BattleFieldManager.Instance.Fresh();
            NormalProcessManager.Instance.Fresh();
            
            // 并未做任何操作，只是为了形式上的统一
            GameContext.Instance.Fresh();
            ScoringBroad.Instance.Fresh();
            SummonManager.Instance.Fresh();
            AttackManager.Instance.Fresh();
            HittenNumManager.Instance.Fresh();
        }
    }
}
