using System;
using System.Collections.Generic;
using System.Linq;
using cfg.effect;

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
        private readonly Unit _sourceUnit;
        
        // ------ 属性快速获取器 ------
        public EffectType EffectType => _props.EffectType;
        public string EffectAppendix => _props.EffectAppendix;
        public int Numerical => _props.Numerical;
        public TargetType TargetType => _props.TargetType;
        public TargetArea TargetArea => _props.TargetArea;

        // fixme: 设立新的选项，如确定EffectTimeStep的类型（施法/技能）

        public EffectConstructor(EffectConstructProperties properties, BattleSite battleSite)
        {
            _props = properties;
            _battleSite = battleSite;
            // Effect来源视为当前场上角色
            _sourceUnit = GameContext.GetPlayerBySite(_battleSite).Chara;
        }

        /// <summary>
        /// 根据目标属性获得目标格子序列
        /// </summary>
        public List<bool> GetTargetsChoiceAble()
        {
            return BattleFieldManager.Instance.GetTargetListByTargetType(_battleSite, TargetType);
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
                    return AddBuffEffect(targetList);
                case EffectType.Cure:
                    return CureEffect(targetList);
                case EffectType.Draw:
                    DrawEffect();
                    return null;
                default:
                    return null;
            }
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
                // AOE目标选取
                case TargetArea.AOE:
                    if (target == null) break;
                    targetBucketList.Add(target.Carrier);
                    targetBucketList.AddRange(BattleFieldManager.Instance.GetNeighbors(target.Carrier)); 
                    break;
                // 全体目标选取
                case TargetArea.All:
                    targetBucketList = BattleFieldManager.Instance.GetAllTargets(TargetType, _battleSite);
                    break;
                // 单目标选取
                case TargetArea.Mono:
                    if (target == null) break;
                    targetBucketList.Add(target.Carrier);
                    break;
                case TargetArea.None:    
                default:
                    break;
            }

            return targetBucketList.Select(bucket => bucket.unitCarry).ToList();
        }

        /// <summary>
        /// 根据信息创建叠BuffTimeStep
        /// 注：在Buff相关类型Props中，Appendix为Buff序号
        /// </summary>
        private EffectTimeStep AddBuffEffect(IEnumerable<Unit> units)
        {
            // AddBuff类型Appendix参数为BuffId
            if (!int.TryParse(EffectAppendix, out var buffId)) return null;

            var effects = units.Select(
                unit => new AddBuff(_sourceUnit, unit, buffId,Numerical)
            ).Cast<IEffect>().ToList();
            
            return new EffectTimeStep(effects, TimeEffectType.Spell);
        }
        
        /// <summary>
        /// 根据信息创建伤害TimeStep
        /// 注：在Damage类型的Props中，EffectAppendix为ElementEnum结构的字符串
        /// </summary>
        private EffectTimeStep DamageEffect(IEnumerable<Unit> units)
        {
            
            // 初始化伤害元素
            var element = EnumUtil.ToEnum<ElementEnum>(EffectAppendix);
            
            var effects = units.Select(
                unit => new Damage(_sourceUnit, unit, new DamageStruct(Numerical, element))
                ).Cast<IEffect>().ToList();

            return new EffectTimeStep(effects,TimeEffectType.Spell);
        }

        private EffectTimeStep CureEffect(IEnumerable<Unit> units)
        {
            
            var effects = units.Select(
                unit => new Cure(_sourceUnit, unit, Numerical)
                ).Cast<IEffect>().ToList();
            
            return new EffectTimeStep(effects,TimeEffectType.Spell);
        }
        
        private void DrawEffect()
        {
            GameContext.GetPlayerBySite(_battleSite).HandOutCard(Numerical);
        }
    }
}