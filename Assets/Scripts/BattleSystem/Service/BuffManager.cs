using System;
using cfg.effect;
using cfg.buff;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cfg.common;
using Spine.Unity.Editor;

namespace Genpai
{

    /// <summary>
    /// BuffManager储存的关系数据结构
    /// Unit为对挂载单位的引用
    /// Buff为描述性数据结构
    /// Work标识决定Buff是否作用
    /// </summary>
    public class BuffPair
    {
        public Unit Unit => _pair.Key;
        public int BuffId => _pair.Value.BuffId;
        public Buff Buff => _pair.Value;
        public bool IsWorking;

        private KeyValuePair<Unit, Buff> _pair;
        
        public BuffPair(Unit unit, Buff buff, bool trigger = true)
        {
            _pair = new KeyValuePair<Unit, Buff>(unit, buff);
            IsWorking = trigger;
        }

        public BuffPair(KeyValuePair<Unit, Buff> pair, bool trigger = true)
        {
            _pair = pair;
            IsWorking = trigger;
        }

        /// <summary>
        /// Equals判断
        /// 同Unit上的同名Buff表示BuffPair为Equal
        /// </summary>
        public bool Equals(KeyValuePair<Unit, Buff> pair)
        {
            // 可运行、同Unit、同Buff
            return IsWorking && Unit == pair.Key && pair.Value.BuffId == BuffId;
        }

        public bool Equals(Unit unit, int buffId)
        {
            return IsWorking && Unit == unit && BuffId == buffId;
        }

    }
    
    /// <summary>
    /// 一个实验性的Buff新架构
    /// 使用类似ECS的模式管理Buff（广义）及其执行
    /// 详细参考Luban内数据表格食用
    /// </summary>
    public class BuffManager : Singleton<BuffManager>
    { 

        public readonly HashSet<BuffPair> BuffSet;

        public BuffManager()
        {
            BuffSet = new HashSet<BuffPair>();
        }

        /// <summary>
        /// 注册Buff
        /// 通过Buff数据与Unit对象进行注册
        /// 检测当前是否在相同对象上注册过同名Buff
        /// 实行加Buff或叠层操作
        /// </summary>
        public void AddBuff(Unit unit, Buff buff, bool trigger = true)
        {
            var pair = new KeyValuePair<Unit, Buff>(unit, buff);
            var buffPair = BuffSet.FirstOrDefault(buffPair => buffPair.Equals(pair));
            
            // 默认添加
            if (buffPair == default)
            {
                BuffSet.Add(new BuffPair(pair));
                return;
            }

            // 叠层模式
            if (buffPair.Buff.IncreaseAble)
            {
                buffPair.Buff.Storey += buff.Storey;
            }
        }

        public void DelBuff(Unit unit, int buffId, int props = default)
        {
            var buffPair = BuffSet.FirstOrDefault(buffPair => buffPair.Equals(unit,buffId));

            // 无 Buff or 不可删
            if (buffPair == default || !buffPair.Buff.DeleteAble) return;
            
            // 标准删除
            if (props == default)
            {
                buffPair.IsWorking = false;
                return;
            }
            
            // 逐层删除
            if (buffPair.Buff.Storey > props)
            {
                buffPair.Buff.Storey -= props;
            }
            else
            {
                buffPair.IsWorking = false;
            }
        }

        /// <summary>
        /// 根据时间及需求模式选择获取对应Buff集合
        /// 需求模式: 主动触发or被动销毁请求
        /// </summary>
        /// <param name="site">当前回合主体</param>
        /// <param name="roundTime">当前回合时间</param>
        /// <param name="initOrDest"></param>
        /// <returns></returns>
        private IEnumerable<BuffPair> GetSetByRoundTime(BattleSite site, RoundTime roundTime, bool initOrDest = true)
        {
            return BuffSet.Where(
                pair => pair.IsWorking && 
                (initOrDest ? pair.Buff.InitiativeTime : pair.Buff.DestructionTime) == roundTime && 
                pair.Unit.OwnerSite == site);
        }

        public void RoundProcess(BattleSite site, RoundTime roundTime)
        {
            InitiativeProcess(GetSetByRoundTime(site,roundTime,true));
            DestructionProcess(GetSetByRoundTime(site,roundTime,false));
        }

        /// <summary>
        /// 执行Buff主动触发
        /// </summary>
        /// <param name="buffPairs"></param>
        private void InitiativeProcess(IEnumerable<BuffPair> buffPairs)
        {
            List<IEffect> effects = new List<IEffect>();
            foreach (var buffPair in buffPairs)
            {
                ProcessBuff(buffPair,ref effects);
            }

            if (effects.Count == 0) return;
            EffectManager.Instance.TakeEffect(new EffectTimeStep(effects,TimeEffectType.Fixed));
        }
        
        /// <summary>
        /// 根据对应接口调用Effect函数
        /// </summary>
        private void ProcessBuff(BuffPair buffPair,ref List<IEffect> effects)
        {
            switch (buffPair.Buff.BaseBuffName)
            {
                case "DamageOverTime":
                    BuffEffect.DamageOverTime(buffPair,ref effects);
                    break;
            }
        }

        /// <summary>
        /// 执行Buff自毁检测
        /// </summary>
        /// <param name="buffPairs"></param>
        private void DestructionProcess(IEnumerable<BuffPair> buffPairs)
        {
            List<IEffect> delBuffs = 
                (from buffPair in buffPairs where buffPair.Buff.Destruction() 
                    select new NewDelBuff(null, buffPair.Unit, buffPair.BuffId)).Cast<IEffect>().ToList();

            if (delBuffs.Count > 0)
            {
                EffectManager.Instance.TakeEffect(new EffectTimeStep(delBuffs.ToList()));
            }
        }



        /// <summary>
        /// 单位寄了把buffPair一起送走
        /// </summary>
        /// <param name="unit"></param>
        public void SetActiveForUnitFall(Unit unit)
        {
            foreach (var buffPair in BuffSet.Where(buffPair => buffPair.Unit == unit))
            {
                buffPair.IsWorking = false;
            }
        }

    }
}