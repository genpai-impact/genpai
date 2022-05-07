﻿using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public enum UnitType
    {
        Monster,    // 怪物，基准单位
        Chara,      // 角色，特殊单位
        Boss        // Boss，特殊单位
    }

    public enum UnitState
    {
        ActiveAttack,       // 主动攻击
        CounterattackAttack,// 反击
        SkillUsing,         // 使用技能
        ChangeChara,        // 更换角色
    }
}