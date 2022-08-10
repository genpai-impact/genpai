using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{


    /// <summary>
    /// ά����Ҫѭ���Ĵ�������
    /// </summary>
    private readonly List<IProcess> _loopProcessList = new List<IProcess>();

    
    bool nextProcess = false;
    private Queue<GameObject> _clickedObjs = new Queue<GameObject>();//����������


    //�ṩ������Ҫ��ɵ�ί�У���ͨ��ί�иı�_clickedObj����_clickedObj,�������ÿ��������Ҫ�ã����԰�����scene��������
    public void ClickDelegate(GameObject gameObject)
    {
        //�˴����������Ͻ�һЩ������ʵ��һ���̣ܳ�shyx 2022/8/1
        
            _clickedObjs.Enqueue(gameObject);
       
        //Debug.Log(gameObject.name);
    }


    /// <summary>
    /// �ؿ���Ϣ
    /// </summary>
    public static MissionConfig MissionConfig
    {
        get;
        set;
    }

    /// <summary>
    /// ���1
    /// </summary>
    public static GenpaiPlayer Player1
    {
        get;
        set;
    }

    /// <summary>
    /// ���2
    /// </summary>
    public static GenpaiPlayer Player2
    {
        get;
        set;
    }

    /// <summary>
    /// �Ƿ�ʹ��AI
    /// </summary>
    public static bool UsingAI
    {
        get;
        set;
    }

    /// <summary>
    /// �л���ɫCD
    /// </summary>
    public static int CharaCd
    {
        get;
        set;
    }

    /// <summary>
    /// Boss
    /// �ݴ�������󷽱����
    /// </summary>
    public static Boss TheBoss
    {
        get;
        set;
    }



    /// <summary>
    /// ��ǰ���ĸ�����ж�
    /// </summary>
    public static GenpaiPlayer CurrentPlayer
    {
        get;
        set;
    }

    /// <summary>
    /// �������/����ٿ����
    /// </summary>
    public static GenpaiPlayer LocalPlayer
    {
        get;
        set;
    }
    /// <summary>
    /// ��һ�����Ӫ
    /// ��Ҫ����Boss��ȡ
    /// �����ʣ�
    /// </summary>
    public static BattleSite PreviousPlayerSite => CurrentPlayer.playerSite == BattleSite.P1 ? BattleSite.P2 : BattleSite.P1;

    /// <summary>
    /// ս����Ϣ
    /// </summary>
    public static BucketEntityManager BattleField = BucketEntityManager.Instance;



    /// <summary>
    /// ��ǰ������ʲô�������̣�ֵ��int���ͣ���ֵ��_loopProcessList�����±��λ��
    /// </summary>
    private int CurrentProcess
    {
        get;
        set;
    }

    

    public void Init(LevelBattleItem LevelInfo, int playerInfo)
    {
        // todo δ��ѡ��֮����ѡ��ѡ���Ǹ��ؿ���Ϣ
        MissionConfig = new MissionConfig(LevelInfo, playerInfo);
    }

    // ֻ��Ϊ����GameContextScript�н�������Ϸ��fresh��ʱ�򱣳���ʽͬһ��û����������
    public void Fresh()
    {

    }

    /// <summary>
    /// �����ǰ�ж������
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
    /// ���ڵ�ǰ���
    /// </summary>
    /// <returns></returns>
    public static GenpaiPlayer GetCurrentPlayer()
    {
        return CurrentPlayer;
    }

    /// <summary>
    /// �������1
    /// </summary>
    /// <returns></returns>
    public static GenpaiPlayer GetPlayer1()
    {
        return Player1;
    }

    /// <summary>
    /// �������2
    /// </summary>
    /// <returns></returns>
    public static GenpaiPlayer GetPlayer2()
    {
        return Player2;
    }

    /// <summary>
    /// ������Ӫ��ȡ���
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
    /// ��ȡ��ǰ������
    /// </summary>
    /// <returns>��ǰ����</returns>
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
    /// ��ʼ�����̹��������ȵ��ã�����ᱨ��
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
    /// ��ʼ������Ϸ,����������ʵ�������Σ�ܳ 2022/7/28
    /// </summary>
    public void StartProcess()
    {
        Init();
        ProcessGameStart.GetInstance().Run();
    }



    /// <summary>
    /// ִ����һ������
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
        CurrentProcess++; // ��Ҫ����ʡһ��д��_loopProcessList[++_currentProcess].Run(); ���ܻᱻ�쵼˵��
    }



    /// <summary>
    /// ����������Ϸ
    /// </summary>
    public void End()
    {
        ProcessGameEnd.GetInstance().Run();
    }


    /// <summary>
    /// �������غ�
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
    /// �ؿ���Ϸ
    /// </summary>
    public void Restart()
    {
        ProcessGameRestart.GetInstance().Run();
    }



}
