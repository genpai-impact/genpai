#define AI

using Genpai;
using UnityEngine;
using System;
using System.Collections.Generic;
using Messager;

namespace Genpai
{
    public class SimpleAI : MonoBehaviour,IMessageReceiveHandler
    {
        public bool usingAI;
        public GenpaiPlayer Player;
        private int _currentRound = 0;


        private void Awake()
        {
            GameContext.usingAI = usingAI;
            if (usingAI == true)
            {
                Subscribe();
            }
        }
        public void AIAction(GenpaiPlayer Player)
        {
            if (Player == null)
            {
                this.Player = GameContext.Player2;
            }

            //判断回合，是否应该行动
            if (_currentRound == 0)
                _currentRound = Player.CurrentRound;
            else
            {
                if (_currentRound == Player.CurrentRound) { return; }
                else { _currentRound = Player.CurrentRound; }
            }

            Debug.Log("now is round" + _currentRound);

            //无角色在场且手中有角色则上场角色
            //BucketEntity Bucket = Player.CharaBucket;
            if (Player.CharaList.Count != 0 && Player.CharaBucket.unitCarry == null)
            {
                //召唤
                GameObject newCard;
                newCard = GameObject.Instantiate(PrefabsLoader.Instance.charaPrefab, PrefabsLoader.Instance.chara2Pool.transform);
                newCard.GetComponent<CharaDisplay>().PlayerSite = Player.playerSite;
                newCard.GetComponent<CharaDisplay>().chara = Player.CharaList[Player.CharaList.Count-1];
                newCard.GetComponent<CharaDisplay>().SummonChara();
                //删除角色牌
                Player.CharaList.RemoveAt(Player.CharaList.Count - 1);
            }



            bool summonable = false;
            List<bool> summonFree = BattleFieldManager.Instance.CheckSummonFree(Player.playerSite, ref summonable);
            //有手牌且可召唤
            if (Player.CardDeck.HandCardList.Count != 0 && summonable)
            {
                for (int i = 0; i < summonFree.Count; i++)
                {
                    //只召唤一张牌
                    if (summonFree[i])
                    {
                        GameObject Bucket = BattleFieldManager.Instance.GetBucketBySerial(i);

                        SummonManager.Instance.waitingPlayer = Player.playerSite;
                        SummonManager.Instance.Summon((UnitCard)Player.CardDeck.HandCardList.Last.Value, Bucket, true);

                        Player.CardDeck.HandCardList.RemoveLast();
                        break;
                    }
                }
            }

            //只攻击BOSS
            
            //遍历每个格子，找到格子上面的UnitEntity，判断其ActionState[ActiveAttack],攻击boss
            foreach(var grid in BattleFieldManager.Instance.bucketVertexs.Values)
            {
                //召唤物并且可以攻击
                if(grid.unitCarry != null && grid.unitCarry.ActionState[UnitState.ActiveAttack] == true)
                {
                    AttackManager.Instance.Attack(grid.unitCarry, GameContext.TheBoss);
                }
            }

            //结束回合
            ProcessRound.GetInstance().AIEndRound();
            //Debug.Log("2333");

        }


        public void Subscribe()
        {
            // 订阅AI操作请求
            MessageManager.Instance.GetManager(MessageArea.AI).Subscribe<GenpaiPlayer>(MessageEvent.AIEvent.AIAction, AIAction);

        }
    }
}