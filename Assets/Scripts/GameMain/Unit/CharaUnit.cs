﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 角色类单位区别于普通单位
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d54e0edd5a93003fc68f40
    /// </summary>
    public class CharaUnit : Unit
    {
        public CharaUnit(UnitCard unitCard, PlayerID _owner) : base(unitCard, _owner)
        {
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}