using System;
using System.Collections;
using System.Collections.Generic;
using BattleSystem.Controller;
using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.Unit;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Unit;
using DataScripts.Card;
using UnityEngine;

namespace BattleSystem.Service.Player.AI
{
    public class SimpleAI : BaseAI
    {
        private Queue<UnitEntity> queueForAct = new Queue<UnitEntity>();
        private MonoBehaviour _mb;

        public SimpleAI(AIType _Type, GenpaiPlayer _Player) : base(_Type, _Player) { }
        public override void CharaStrategy()//上角色策略
        {
            //无角色在场且手中有角色则上场角色
            //BucketEntity Bucket = Player.CharaBucket;
            if (Player.CharaManager.Count() != 0 && Player.CharaBucket.unitCarry == null)
            {
                Player.CharaManager.Summon(false);
            }
        }

        public override void MonsterStrategy()//上怪物策略
        {
            bool summonable = false;
            List<bool> summonFree = BattleFieldManager.Instance.CheckSummonFree(Player.playerSite, ref summonable);
            //有手牌且可召唤
            if (Player.CardDeck.HandCardList.Count == 0 || !summonable) return;
            
            for (int i = 0; i < summonFree.Count; i++)
            {
                //只召唤一张牌
                if (summonFree[i])
                {
                    GameObject Bucket = BucketEntityManager.Instance.GetBucketBySerial(i);

                    SummonManager.Instance.waitingPlayer = Player.playerSite;

                    DataScripts.Card.Card card = Player.CardDeck.HandCardList.Last.Value;
                    if (card is SpellCard)
                    {
                        //重构魔法卡管理器中，暂时注释
                        //if(card is DamageSpellCard)
                        //{
                        //    MagicManager.Instance.MagicAttack(null, GameContext.TheBoss, card as DamageSpellCard);
                        //}
                        continue;
                    }

                    SummonManager.Instance.Summon((UnitCard)Player.CardDeck.HandCardList.Last.Value, Bucket, true);

                    Player.CardDeck.HandCardList.RemoveLast();
                    break;
                }
            }

        }


        private IEnumerator ActInQueue()
        {
            while (queueForAct.Count != 0)
            {
                UnitEntity unitEntity = queueForAct.Dequeue();

                float cnt = 5f;

                Unit.Unit unit = BattleFieldManager.Instance.GetBucketBySerial(unitEntity.carrier.serial).unitCarry;

                AttackManager.Instance.Attack(unit, GameContext.TheBoss);

                if (unitEntity.GetComponent<UnitModelDisplay>().animator == null) { continue; }

                while (cnt > 0)
                {
                    cnt -= 0.05f;
                    if (unitEntity.GetComponent<UnitModelDisplay>().animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    {
                        break;
                    }
                    yield return new WaitForSeconds(0.05f);
                }

                while (cnt > 0)
                {
                    cnt -= 0.05f;
                    if (!unitEntity.GetComponent<UnitModelDisplay>().animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    {
                        break;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }

        private IEnumerator WaitForQueue()
        {
            while (queueForAct.Count != 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        public override void AttackStrategy()//攻击策略
        {
            //只攻击BOSS
            //遍历每个格子，找到格子上面的UnitEntity，判断其ActionState[ActiveAttack],攻击boss

            _mb = GameObject.FindObjectOfType<MonoBehaviour>();

            _mb.StartCoroutine(WaitForQueue());

            foreach (var grid in BattleFieldManager.Instance.Buckets.Values)
            {
                if (grid.owner != Player) continue;
                //有召唤物并且可以攻击
                if (grid.unitCarry != null && grid.unitCarry.ActionState[UnitState.ActiveAttack] == true)
                {
                    //queueForAct.Enqueue(grid.unitCarry);
                    //_mb.StartCoroutine(ActInQueue());
                    AttackManager.Instance.Attack(grid.unitCarry, GameContext.TheBoss);
                }
            }

            _mb.StartCoroutine(ActInQueue());

        }

        //临时用这个凑合实现魔法攻击，之后的魔法应该都写在另一个函数

        public override void MagicStrategy()
        {
            throw new NotImplementedException();
        }

    }
}