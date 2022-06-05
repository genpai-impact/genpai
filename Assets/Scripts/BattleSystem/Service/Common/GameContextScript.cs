
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
            BattleFieldManager.Instance.Init();

            CardLibrary.Instance.LoadFormFile();
            GameContext.Instance.Init();
            ScoringBroad.Instance.Init();
            SummonManager.Init();
            AttackManager.Instance.Init();
            HittenNumManager.Instance.Init();
            NormalProcessManager.Instance.Start();

        }
        
    }
}
