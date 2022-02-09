using Messager;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// boss行动流程
    /// </summary>
    class ProcessBoss : IProcess
    {
        private int round = 0;

        private static ProcessBoss bossProcess = new ProcessBoss();
        private ProcessBoss()
        {
        }
        public static ProcessBoss GetInstance()
        {
            return bossProcess;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            return "RoundBoss";
        }
        public void Run()
        {
            round++;

            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundStart, BattleSite.Boss);


            // boss第一回合不行动，产品需求如此
            if (round > 3 && GameContext.TheBoss.ActionState[UnitState.SkillUsing])
            {
                // 释放技能(草率草率草率)

                // 更新MP
                MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnBossStart, BattleSite.Boss);

                // 来个2技能
                if ((GameContext.TheBoss.unit as Boss).MP_2 == 3)
                {
                    Debug.Log("来个2技能");

                    // 获取可攻击格子
                    List<bool> bucketMask = BattleFieldManager.Instance.CheckAttackable(BattleSite.Boss, true);
                    List<GameObject> bucketList = BattleFieldManager.Instance.GetBucketSet(bucketMask);

                    DamageStruct damage = new DamageStruct(2, ElementEnum.None);
                    List<IEffect> damageList = new List<IEffect>();

                    // 对每个格子上单位造成伤害
                    foreach (GameObject bucket in bucketList)
                    {
                        damageList.Add(new Damage(GameContext.TheBoss, bucket.GetComponent<BucketEntity>().unitCarry, damage));
                    }


                    EffectManager.Instance.TakeEffect(damageList);

                    (GameContext.TheBoss.unit as Boss).MP_2 = 0;
                }

                // 来个1技能
                else if ((GameContext.TheBoss.unit as Boss).MP_1 == 1)
                {
                    Debug.Log("来个1技能");

                    GameObject bucket = BattleFieldManager.Instance.GetDangerousBucket(GameContext.PreviousPlayerSite);

                    if (bucket != null)
                    {
                        DamageStruct damage = new DamageStruct(4, ElementEnum.None);
                        List<IEffect> damageList = new List<IEffect>();
                        damageList.Add(new Damage(GameContext.TheBoss, bucket.GetComponent<BucketEntity>().unitCarry, damage));

                        EffectManager.Instance.TakeEffect(damageList);

                        // 找到上回合行动方顺序单位
                        (GameContext.TheBoss.unit as Boss).MP_1 = 0;
                    }


                }

            }
            GameContext.processManager.Next();
        }
    }
}
