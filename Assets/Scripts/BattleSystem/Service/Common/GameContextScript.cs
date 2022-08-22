using BattleSystem.Controller;
using BattleSystem.Controller.EntityManager;
using BattleSystem.Controller.UI;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Process;
using DataScripts;
using UnityEngine;
using GameSystem.LevelSystem;
using cfg.level;
using DataScripts.DataLoader;

namespace BattleSystem.Service.Common
{
    /// <summary>
    /// 用于激活游戏流程的脚本
    /// </summary>
    public class GameContextScript : MonoBehaviour
    {
        public void Start()
        {
            if(!LubanLoader.IsInit) LubanLoader.Init();
            
            LevelBattleItem levelInfo = LevelInfoDontDestroy.Instance.GetLevelInfo();
            // fixme: 此处后续需要传更多参数
            int playerInfo = LevelInfoDontDestroy.Instance.playerCardDeckId;
            
            Debug.Log("game context is " + GameContext.Instance);

            // 清空单例数据
            Fresh();
            
            // to do： 从LevelInfo读取场景背景
            // to do： 设置AI模式
            
            
            CardLoader.Instance.Init();
            // 初始化上下文
            GameContext.Instance.Init(levelInfo, playerInfo);
            // 初始化战场
            BattleFieldManager.Instance.Init();
            // 初始化分数统计
            ScoringBroad.Instance.Init();
            // 初始化结束面板
            EndGameBannerDisplay.Instance.Init();
            
            // 启动游戏流程
            NormalProcessManager.Instance.StartProcess();

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

            // 本不在这个部分出现的Manager
            AnimatorManager.Instance.Fresh();
            
            // 并未做任何操作，只是为了形式上的统一
            GameContext.Instance.Fresh();
            ScoringBroad.Instance.Fresh();
            SummonManager.Instance.Fresh();
            AttackManager.Instance.Fresh();
            HittenNumManager.Instance.Fresh();
        }
    }
}
