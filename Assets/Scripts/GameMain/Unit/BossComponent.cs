using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Boss组件
    /// 追加在UnitEntity上以实现Boss功能
    /// 主要体现为释放技能
    /// </summary>
    public class BossComponent : MonoBehaviour
    {
        public Boss unit;

        public int MPMax_1
        {
            get { return unit.MPMax_1; }
        }
        public int MPMax_2
        {
            get { return unit.MPMax_2; }
        }
        public int MP_1
        {
            get => unit.MP_1;
            set { unit.MP_1 = value; }
        }
        public int MP_2
        {
            get => unit.MP_2;
            set { unit.MP_2 = value; }
        }

        public void Awake()
        {
        }

        public void Init(Boss _unit)
        {
            this.unit = _unit;
        }
        /*
        // todo 技能改成类
        public void Skill()
        {
            NewUnit TheBoss = NewBattleFieldManager.Instance.GetBucketBySerial(0).unitCarry;
            if ((GameContext.TheBoss.unit as Boss).MP_2 >= 3)
            {
                // 获取可攻击格子
                List<bool> bucketMask = NewBattleFieldManager.Instance.CheckAttackable(BattleSite.Boss, true);
                List<NewBucket> bucketList = NewBattleFieldManager.Instance.GetBucketSet(bucketMask);
                DamageStruct damage = new DamageStruct(2, ElementEnum.None);
                List<IEffect> damageList = new List<IEffect>();
                // 对每个格子上单位造成伤害
                foreach (NewBucket bucket in bucketList)
                {
                    damageList.Add(new Damage(TheBoss, bucket.unitCarry, damage));
                }
                EffectManager.Instance.TakeEffect(damageList);
                (GameContext.TheBoss.unit as Boss).MP_2 = 0;
            }
            if ((GameContext.TheBoss.unit as Boss).MP_1 >= 1)
            {
                NewBucket bucket = NewBattleFieldManager.Instance.GetDangerousBucket(GameContext.PreviousPlayerSite);
                if (bucket != null)
                {
                    DamageStruct damage = new DamageStruct(4, ElementEnum.None);
                    List<IEffect> damageList = new List<IEffect>();
                    damageList.Add(new Damage(TheBoss, bucket.unitCarry, damage));
                    EffectManager.Instance.TakeEffect(damageList);
                    // 找到上回合行动方顺序单位
                    (GameContext.TheBoss.unit as Boss).MP_1 = 0;
                }
            }
        }
        */
        public void AddMP()
        {
            if (0 <= MP_1 && MP_1 < MPMax_1)
            {
                MP_1++;
            }
            if (0 <= MP_2 && MP_2 < MPMax_2)
            {
                MP_2++;
            }
            Debug.Log("Boss MP1:" + MP_1 + " MP2:" + MP_2);
        }
    }
}