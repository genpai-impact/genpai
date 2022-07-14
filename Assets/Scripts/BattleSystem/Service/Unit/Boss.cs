using System.Collections.Generic;
using BattleSystem.Controller.Unit.UnitView;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Effect;
using BattleSystem.Service.Element;
using BattleSystem.Service.MessageDatas;
using BattleSystem.Service.Player;
using DataScripts.Card;
using Utils.Messager;

namespace BattleSystem.Service.Unit
{
    public class Boss : Unit
    {

        /// <summary>
        /// 技能1的MP上限
        /// </summary>
        public readonly int MPMax_1;

        /// <summary>
        /// 技能2的MP上限
        /// </summary>
        public readonly int MPMax_2;

        /// <summary>
        /// 技能1的MP
        /// </summary>
        public int MP_1;

        /// <summary>
        /// 技能2的MP
        /// </summary>
        public int MP_2;

        public Boss(UnitCard _unitCard, Bucket _carrier) : base(_unitCard, _carrier)
        {
            this.MPMax_1 = 1;
            this.MPMax_2 = 3;
            this.MP_1 = 0;
            this.MP_2 = 0;

            ActionState[UnitState.SkillUsing] = true;
        }

        public override UnitView GetView()
        {
            UnitView view = new UnitView(this);
            view.Mp = MP_2;
            return view;
        }

        protected override void WhenSetHp(int _newHP)
        {
            MessageManager.Instance.Dispatch(
                    MessageArea.Context,
                    MessageEvent.ContextEvent.BossScoring,
                    new BossScoringData(GameContext.CurrentPlayer.playerSite, Hp - _newHP));
            if (Hp > 0.75 * BaseUnit.BaseHp && _newHP <= 0.75 * BaseUnit.BaseHp)
            {
                MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach75, true);
                GameContext.Player1.HandOutChara(1);
                GameContext.Player2.HandOutChara(1);
            }
            if (Hp > 0.5 * BaseUnit.BaseHp && _newHP <= 0.5 * BaseUnit.BaseHp)
            {
                MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach50, true);
                GameContext.Player1.HandOutChara(1);
                GameContext.Player2.HandOutChara(1);
            }
        }

        protected override void WhenFall()
        {
            MessageManager.Instance.Dispatch(
                    MessageArea.Context,
                    MessageEvent.ContextEvent.BossScoring,
                    new BossScoringData(GameContext.CurrentPlayer.playerSite, 5));

            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);
        }

        // todo 技能改成类
        public void Skill()
        {
            // Debug.Log("Boss Skilling");

            if (MP_2 >= 3)
            {
                // 获取可攻击格子
                List<bool> bucketMask = BattleFieldManager.Instance.CheckAttackable(BattleSite.Boss, true);
                List<Bucket> bucketList = BattleFieldManager.Instance.GetBucketSet(bucketMask);
                DamageStruct damage = new DamageStruct(2, ElementEnum.None);

                List<IEffect> damageList = new List<IEffect>();
                // 对每个格子上单位造成伤害
                foreach (Bucket bucket in bucketList)
                {
                    damageList.Add(new Damage(GameContext.TheBoss, bucket.unitCarry, damage, DamageType.Magic));
                }

                EffectManager.Instance.TakeEffect(new EffectTimeStep(damageList, TimeEffectType.Skill));
                MP_2 = 0;
                return;
            }
            if (MP_1 >= 1)
            {
                Bucket bucket = BattleFieldManager.Instance.GetDangerousBucket(GameContext.PreviousPlayerSite);
                if (bucket != null)
                {
                    DamageStruct damage = new DamageStruct(4, ElementEnum.None);
                    List<IEffect> damageList = new List<IEffect>();
                    damageList.Add(new Damage(GameContext.TheBoss, bucket.unitCarry, damage));
                    EffectManager.Instance.TakeEffect(new EffectTimeStep(damageList, TimeEffectType.Attack));
                    // 找到上回合行动方顺序单位
                    MP_1 = 0;
                }
            }
        }

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
            // Debug.Log("Boss MP1:" + MP_1 + " MP2:" + MP_2);
        }

    }
}