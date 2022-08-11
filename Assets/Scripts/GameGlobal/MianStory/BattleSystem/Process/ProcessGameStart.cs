using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.UI;
using BattleSystem.Controller.Unit;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Card;
using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using BattleSystem.Service.Unit;
using DataScripts.Card;
using DataScripts.DataLoader;
using UnityEngine;
using Utils.Messager;


/// <summary>
/// 游戏开始时需要做的事
/// </summary>
class ProcessGameStart : IProcess
{ 
        //private static readonly ProcessGameStart GameStartProcess = new ProcessGameStart();

/*private ProcessGameStart()
{
}*/

private static readonly string Name = "GameStart";
public static ProcessGameStart GetInstance()
{
    return GameStartProcess;
}

public string GetName()
{
    return Name;
}

public void Run()
{
    // 初始化牌组
    CardLibraryLoader.Instance.CardDeckLoad();

    // 创建玩家
    GameContext.Player1 = new GenpaiPlayer(200, BattleSite.P1);
    GameContext.Player1.Init();
    GameContext.Player2 = new GenpaiPlayer(201, BattleSite.P2);
    GameContext.Player2.Init();

    GameContext.CurrentPlayer = GameContext.Player1;
    GameContext.LocalPlayer = GameContext.Player1;

    // 为双方玩家牌库初始化配置（set抽卡数）

    // 发布游戏开始消息（牌库实现抽卡）
    GameContext.Player1.HandOutChara(GameContext.MissionConfig.StartHeroCount);
    GameContext.Player1.HandOutCard(GameContext.MissionConfig.StartCardCount);

    GameContext.Player2.HandOutChara(GameContext.MissionConfig.StartHeroCount);
    GameContext.Player2.HandOutCard(GameContext.MissionConfig.StartCardCount);

    GameContext.Player1.CharaManager.Summon(true);
    GameContext.Player2.CharaManager.Summon(true);
    GameContext.Player1.CharaManager.CurrentCharaBanner.gameObject.SetActive(true);
    GameContext.Player2.CharaManager.CurrentCharaBanner.gameObject.SetActive(true);
    GameContext.Player1.CharaCD = 0;
    GameContext.Player2.CharaCD = 0;


    // 创建Boss
    InitBoss();

    NormalProcessManager.Instance.Next();
}


public void InitBoss()
{
    // 获取Boss卡牌数据
    UnitCard bossCard = CardLoader.Instance.GetCardById(GameContext.MissionConfig.BossID) as UnitCard;

    // 创建Boss对象
    GameObject bucket = BucketEntityManager.Instance.GetBucketBySerial(0);
    Transform unitSeats = bucket.transform.Find("Unit");
    GameObject unit = Object.Instantiate(PrefabsLoader.Instance.unitPrefab, unitSeats.transform);
    unit.AddComponent<UnitEntity>();
    unit.AddComponent<UnitPlayerController>();
    unit.GetComponent<UnitEntity>().Init(BattleSite.Boss, bucket.GetComponent<BucketEntity>());

    BucketEntityManager.Instance.SetBucketCarryFlag(bucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());


    Bucket newBucket = BattleFieldManager.Instance.GetBucketBySerial(0);
    Unit.Unit newUnit = new Boss(bossCard, newBucket);
    unit.GetComponent<UnitDisplay>().Display(newUnit.GetView());
    GameContext.TheBoss = (Boss)newUnit;
}

public void Dispatch(MessageArea areaCode, string eventCode, object message)
{
    MessageManager.Instance.Dispatch(areaCode, eventCode, message);
}
}

