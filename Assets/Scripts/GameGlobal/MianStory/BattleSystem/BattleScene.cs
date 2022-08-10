using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{


    /// <summary>
    /// 维护需要循环的处理流程
    /// </summary>
    private readonly List<IProcess> _loopProcessList = new List<IProcess>();

    
    bool nextProcess = false;
    private Queue<GameObject> _clickedObjs = new Queue<GameObject>();//点击组件队列


    //提供点击组件要完成的委托，即通过委托改变_clickedObj队列_clickedObj,这个函数每个场景都要用，可以把所有scene整个父类
    public void ClickDelegate(GameObject gameObject)
    {
        //此处加锁更加严谨一些，可以实现一个管程？shyx 2022/8/1
        
            _clickedObjs.Enqueue(gameObject);
       
        //Debug.Log(gameObject.name);
    }


    /// <summary>
    /// 关卡信息
    /// </summary>
    public static MissionConfig MissionConfig
    {
        get;
        set;
    }

    /// <summary>
    /// 玩家1
    /// </summary>
    public static GenpaiPlayer Player1
    {
        get;
        set;
    }

    /// <summary>
    /// 玩家2
    /// </summary>
    public static GenpaiPlayer Player2
    {
        get;
        set;
    }

    /// <summary>
    /// 是否使用AI
    /// </summary>
    public static bool UsingAI
    {
        get;
        set;
    }

    /// <summary>
    /// 切换角色CD
    /// </summary>
    public static int CharaCd
    {
        get;
        set;
    }

    /// <summary>
    /// Boss
    /// 暂存组件对象方便访问
    /// </summary>
    public static Boss TheBoss
    {
        get;
        set;
    }



    /// <summary>
    /// 当前是哪个玩家行动
    /// </summary>
    public static GenpaiPlayer CurrentPlayer
    {
        get;
        set;
    }

    /// <summary>
    /// 本地玩家/界面操控玩家
    /// </summary>
    public static GenpaiPlayer LocalPlayer
    {
        get;
        set;
    }
    /// <summary>
    /// 上一玩家阵营
    /// 主要用于Boss获取
    /// （草率）
    /// </summary>
    public static BattleSite PreviousPlayerSite => CurrentPlayer.playerSite == BattleSite.P1 ? BattleSite.P2 : BattleSite.P1;

    /// <summary>
    /// 战场信息
    /// </summary>
    public static BucketEntityManager BattleField = BucketEntityManager.Instance;



    /// <summary>
    /// 当前正处于什么处理流程，值是int类型，数值是_loopProcessList数组下标的位置
    /// </summary>
    private int CurrentProcess
    {
        get;
        set;
    }

    

    public void Init(LevelBattleItem LevelInfo, int playerInfo)
    {
        // todo 未来选关之后，用选关选的那个关卡信息
        MissionConfig = new MissionConfig(LevelInfo, playerInfo);
    }

    // 只是为了在GameContextScript中进行新游戏的fresh的时候保持形式同一，没有特殊作用
    public void Fresh()
    {

    }

    /// <summary>
    /// 变更当前行动的玩家
    /// </summary>
    public static void ChangeCurrentPlayer()
    {
        CurrentPlayer = CurrentPlayer.Equals(Player1) ? Player2 : Player1;
    }

    public static void ChangeLocalPlayer()
    {
        LocalPlayer = LocalPlayer.Equals(Player1) ? Player2 : Player1;
        GameObject.Find("GameManager").GetComponent<ChangePlayer>().ChangeLocalPlayer();
        // Debug.Log("Local Player is: " + LocalPlayer.playerSite);
    }

    /// <summary>
    /// 用于当前玩家
    /// </summary>
    /// <returns></returns>
    public static GenpaiPlayer GetCurrentPlayer()
    {
        return CurrentPlayer;
    }

    /// <summary>
    /// 返回玩家1
    /// </summary>
    /// <returns></returns>
    public static GenpaiPlayer GetPlayer1()
    {
        return Player1;
    }

    /// <summary>
    /// 返回玩家2
    /// </summary>
    /// <returns></returns>
    public static GenpaiPlayer GetPlayer2()
    {
        return Player2;
    }

    /// <summary>
    /// 根据阵营获取玩家
    /// </summary>
    /// <param name="site"></param>
    /// <returns></returns>
    public static GenpaiPlayer GetPlayerBySite(BattleSite site)
    {
        return site switch
        {
            BattleSite.P1 => Player1,
            BattleSite.P2 => Player2,
            _ => null
        };
    }
    public ProcessGame processGame = ProcessGame.Start;

    public override string ToString()
    {
        return $"{{{nameof(Player1)}={Player1}, {nameof(Player2)}={Player2}, {nameof(TheBoss)}={TheBoss}, {nameof(CurrentPlayer)}={CurrentPlayer}}}";
    }

    PlayerRound playerRound = PlayerRound.Boss;

    private bool _nextTrigger = true;
    /// <summary>
    /// 获取当前的流程
    /// </summary>
    /// <returns>当前流程</returns>
    public IProcess GetCurrentProcess()
    {
        return _loopProcessList[CurrentProcess];
    }

    private void Update()
    {
        switch (processGame) { 
            case ProcessGame.Start:

                break;
            case ProcessGame.Gaming:
                switch (playerRound)
                {
                    case PlayerRound.Boss:


                }
                break;
            case ProcessGame.End:
                break;
        }
        
    }

    /// <summary>
    /// 初始化流程管理，必须先调用，否则会报错
    /// </summary>
    public void Init()
    {
        _loopProcessList.Add(ProcessBoss.GetInstance());
        _loopProcessList.Add(ProcessRoundStart.GetInstance());
        _loopProcessList.Add(ProcessRound.GetInstance());
        _loopProcessList.Add(ProcessRoundEnd.GetInstance());
        CurrentProcess = -1;
    }

    public void Fresh()
    {
        _loopProcessList.Clear();
    }

    /// <summary>
    /// 开始本局游戏,忘改名导致实例化两次，艹 2022/7/28
    /// </summary>
    public void StartProcess()
    {
        Init();
        ProcessGameStart.GetInstance().Run();
    }



    /// <summary>
    /// 执行下一个流程
    /// </summary>
    public void Next()
    {
        if (_loopProcessList.Count - 1 == CurrentProcess)
        {
            _nextTrigger = true;

            CurrentProcess = 0;
            //_loopProcessList[0].Run();
            return;
        }



        _nextTrigger = true;
        CurrentProcess++; // 不要想着省一行写成_loopProcessList[++_currentProcess].Run(); 可能会被领导说。
    }



    /// <summary>
    /// 结束本局游戏
    /// </summary>
    public void End()
    {
        ProcessGameEnd.GetInstance().Run();
    }


    /// <summary>
    /// 结束本回合
    /// </summary>
    public void EndRound()
    {
        if (GetCurrentProcess().GetName() != ProcessRound.Name)
        {
            return;
        }
        Next();
    }



    /// <summary>
    /// 重开游戏
    /// </summary>
    public void Restart()
    {
        ProcessGameRestart.GetInstance().Run();
    }



}
