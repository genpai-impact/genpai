using System;
using System.Collections.Generic;
using System.Linq;
using cfg.effect;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// 效果构造器
    /// 用于魔法与主动技能流程中的Effect构造
    /// </summary>
    public class EffectConstructor
    {
        private readonly EffectConstructProperties _props;
        private readonly BattleSite _battleSite;
        
        // ------ 属性快速获取器 ------
        private EffectType EffectType => _props.EffectType;
        private string EffectAppendix => _props.EffectAppendix;
        private int Numerical => _props.Numerical;
        private TargetType TargetType => _props.TargetType;
        private TargetArea TargetArea => _props.TargetArea;

        // fixme: 设立新的选项，如确定EffectTimeStep的类型（施法/技能）

        public EffectConstructor(EffectConstructProperties properties, BattleSite battleSite)
        {
            _props = properties;
            _battleSite = battleSite;
        }

        /// <summary>
        /// 根据目标属性获得目标格子序列
        /// </summary>
        /// <param name="battleSite"></param>
        /// <returns></returns>
        public List<bool> GetTargetsChoiceAble(BattleSite battleSite)
        {
            return BattleFieldManager.Instance.GetTargetListByTargetType(battleSite, TargetType);
        }

        /// <summary>
        /// 根据本地记生成序列
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public EffectTimeStep GenerateTimeStep(Unit target = null)
        {
            // 获取目标
            List<Unit> targetList = GetTargetByArea(target);

            switch (EffectType)
            {
                case EffectType.Damage:
                    return DamageEffect(targetList);
                case EffectType.AddBuff:
                    break;
                case EffectType.Cure:
                    return CureEffect(targetList);
                case EffectType.Draw:
                    DrawEffect();
                    break;
                default:
                    return null;
            }
            
            return null;
        }

        /// <summary>
        /// 获取目标序列
        /// 对于无目标、单目标、全目标无需Target参数
        /// 对于AOE取主目标及相邻目标
        /// </summary>
        /// <param name="target">选取目标</param>
        /// <returns>所有目标序列</returns>
        private List<Unit> GetTargetByArea(Unit target = null)
        {
            List<Bucket> targetBucketList = new List<Bucket>();
            
            switch (TargetArea)
            {
                case TargetArea.AOE:
                    if (target == null) break;
                    targetBucketList.Add(target.Carrier);
                    targetBucketList.AddRange(BattleFieldManager.Instance.GetNeighbors(target.Carrier)); 
                    break;
                case TargetArea.All:
                    targetBucketList = BattleFieldManager.Instance.GetAllTargets(TargetType, _battleSite);
                    break;
                case TargetArea.None:
                case TargetArea.Mono:
                default:
                    break;
            }

            return targetBucketList.Select(bucket => bucket.unitCarry).ToList();
        }
        
        /// <summary>
        /// 根据信息创建伤害TimeStep
        /// 注：在Damage类型的Props中，EffectAppendix为ElementEnum结构的字符串
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        private EffectTimeStep DamageEffect(IEnumerable<Unit> units)
        {
            // 初始化Effect来源
            Unit source = GameContext.GetPlayerBySite(_battleSite).Chara;
            // 初始化伤害元素
            var element = EnumUtil.ToEnum<ElementEnum>(EffectAppendix);
            
            var effects = units.Select(
                unit => new Damage(source, unit, new DamageStruct(Numerical, element))
                ).Cast<IEffect>().ToList();

            return new EffectTimeStep(effects,TimeEffectType.Spell);
        }

        private EffectTimeStep CureEffect(IEnumerable<Unit> units)
        {
            Unit source = GameContext.GetPlayerBySite(_battleSite).Chara;
            var effects = units.Select(
                unit => new Cure(source, unit, Numerical)
                ).Cast<IEffect>().ToList();
            
            return new EffectTimeStep(effects,TimeEffectType.Spell);
        }
        
        private void DrawEffect()
        {
            GameContext.GetPlayerBySite(_battleSite).HandOutCard(Numerical);
        }
    }
}