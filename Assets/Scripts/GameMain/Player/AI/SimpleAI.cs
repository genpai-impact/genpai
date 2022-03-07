using Genpai;
using UnityEngine;
using System;
using System.Collections.Generic;
using Messager;

namespace Genpai
{
    public class SimpleAI : BaseAI
    {
        public SimpleAI(AIType _Type, GenpaiPlayer _Player): base(_Type, _Player){ }
        public override void CharaStrategy()//上角色策略
        {
            //无角色在场且手中有角色则上场角色
            //BucketEntity Bucket = Player.CharaBucket;
            if (Player.HandCharaManager.Count() != 0 && Player.CharaBucket.unitCarry == null)
            {
                Player.HandCharaManager.Summon();
            }
        }

        public override void MonsterStrategy()//上怪物策略
        {
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
                        
                        Card card=Player.CardDeck.HandCardList.Last.Value;
                        if(card is SpellCard)
                        {
                            if(card is DamageSpellCard)
                            {
                                MagicManager.Instance.MagicAttack(null, GameContext.TheBoss, card as DamageSpellCard);
                            }
                            continue;
                        }

                        SummonManager.Instance.Summon((UnitCard)Player.CardDeck.HandCardList.Last.Value, Bucket, true);

                        Player.CardDeck.HandCardList.RemoveLast();
                        break;
                    }
                }
            }

        }

        public override void AttackStrategy()//攻击策略
        {
            //只攻击BOSS
            //遍历每个格子，找到格子上面的UnitEntity，判断其ActionState[ActiveAttack],攻击boss
            foreach (var grid in BattleFieldManager.Instance.bucketVertexs.Values)
            {
                if (grid.owner == Player)
                {
                    //有召唤物并且可以攻击
                    if (grid.unitCarry != null && grid.unitCarry.ActionState[UnitState.ActiveAttack] == true)
                    {
                        AttackManager.Instance.Attack(grid.unitCarry, GameContext.TheBoss);
                    }
                }
            }
        }

        //临时用这个凑合实现魔法攻击，之后的魔法应该都写在另一个函数

        public override void MagicStrategy()
        {
            throw new NotImplementedException();
        }

    }
}