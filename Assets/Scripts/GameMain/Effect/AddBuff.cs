using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Genpai
{
    /// <summary>
    /// 记录添加Buff的效果
    /// </summary>
    public class AddBuff : IEffect
    {
        public UnitEntity source;
        public UnitEntity target;

        public BuffEnum BuffID;
        public int BuffNum;
        //
        public AddBuff(UnitEntity _source, UnitEntity _target, BuffEnum _buffID, int _num = 1)
        {
            this.source = _source;
            this.target = _target;
            this.BuffID = _buffID;
            this.BuffNum = _num;
        }

        public UnitEntity GetSource()
        {
            return source;
        }

        public UnitEntity GetTarget()
        {
            return target;
        }

        public void Add()
        {
            BaseBuff index = target.buffAttachment.FirstOrDefault(buff => buff.buffName == BuffID);
            if (index.Equals(null))
            {
                // TODO：检验类名获取是否正确
                Type type = Type.GetType(BuffID.ToString());

                // 构造参数
                object[] parameters = new object[1];
                parameters[0] = target;
                parameters[1] = BuffNum;

                // 创建对应类对象
                object obj = Activator.CreateInstance(type, parameters);
                target.buffAttachment.AddLast((BaseBuff)obj);
            }

        }
    }
}