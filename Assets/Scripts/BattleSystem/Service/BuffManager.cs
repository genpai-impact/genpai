using System;
using cfg.effect;
using cfg.buff;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public string BuffName => _pair.Value.BuffName;
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
            return pair.Key == Unit && pair.Value.BuffName == BuffName;
        }

    }
    
    /// <summary>
    /// 一个实验性的Buff新架构
    /// 使用类似ECS的模式管理Buff（广义）及其执行
    /// 详细参考Luban内数据表格食用
    /// </summary>
    public class BuffManager : Singleton<BuffManager>
    { 
        // Dictionary<KeyValuePair<Unit,Buff>,bool>
        // HashSet<Tuple<Unit,Buff,bool>>
        // HashSet<KeyValuePair<Unit,Buff>>
        public HashSet<BuffPair> BuffSet;

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
        /// <param name="unit"></param>
        /// <param name="buff"></param>
        /// <param name="trigger"></param>
        public void AddBuff(Unit unit,Buff buff,bool trigger = true)
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

    }
}