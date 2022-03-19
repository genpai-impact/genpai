using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 角色组件
    /// 追加在UnitEntity上以实现角色功能
    /// 主要体现为释放技能
    /// </summary>
    public class CharaComponent : MonoBehaviour
    {
        public Chara unit;

        /// <summary>
        /// 当前能量值
        /// </summary>
        public int MP  // MP有随时被修改的需求
        {
            get => unit.MP;
            set => unit.MP = value;
        }

        /// <summary>
        /// 覆盖UnitEntity中的Awake()
        /// </summary>
        public void Awake()
        {
            // 待实现：从数据库获取技能、充能等信息
        }

        public void Init(Chara _unit)
        {
            this.unit = _unit;

        }

        /// <summary>
        /// 充一点能量，如果满了就不充
        /// </summary>
        public void AddMP()
        {
            if (0 <= MP && MP < unit.MPMax)
            {
                MP++;
            }
        }
    }
}