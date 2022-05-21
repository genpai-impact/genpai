using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Messager;

namespace Genpai
{
    public class SkillManager : Singleton<SkillManager>
    {
        private BattleSite _waitingPlayerSite;
        private NewSkill _newSkill;
        private List<bool> _targetList;
        private UnitEntity _sourceUnitEntity;
        public bool SkillWaiting { get; set; } = false;

        public void SkillCancel()
        {
            SkillWaiting = false;
        }

        /// <summary>
        /// 技能的Request
        /// <para>任何技能的施放都需要从这里经过</para>
        /// <para>需要选目标的技能让格子高亮以可选</para>
        /// <para>不需要选目标的技能直接扔去施放</para>
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="sourceUnitEntity"></param>
        public void SkillRequest(int skillId, UnitEntity sourceUnitEntity)
        {
            ClickManager.CancelAllClickAction();
            SkillWaiting = true;
            _sourceUnitEntity = sourceUnitEntity;
            _waitingPlayerSite = _sourceUnitEntity.ownerSite;
            _newSkill = SkillLoader.NewSkills.Single(newSkill => newSkill.SkillId == skillId);
            cfg.effect.TargetType selectType = _newSkill.EffectConstructorList.First().TargetType;  // 技能需要选取目标的类型

            // 不需选择目标的技能直接施放
            if (selectType == cfg.effect.TargetType.None)
            {
                SkillRelease();
                SkillWaiting = false;
                return;
            }

            _targetList = BattleFieldManager.Instance.GetTargetListByTargetType(_waitingPlayerSite, selectType);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, _targetList);  // 高亮
        }

        public void SkillConfirm(UnitEntity targetUnitEntity)
        {
            if (!SkillWaiting)
            {
                return;
            }
            //if (!_targetList[targetUnitEntity.Serial])    // 私以为这个不可能是空的，把这个判断去掉
            //{
            //    return;
            //}
            SkillWaiting = false;
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);  // 取消格子高亮
            SkillRelease(targetUnitEntity.GetUnit());
        }

        private void SkillRelease(Unit targetUnit = null)
        {
            // 如果是主动技能则消耗相应MP
            if (_newSkill.IsErupt)
            {
                int sourceUnitIndex = (_waitingPlayerSite == BattleSite.P1) ? 5 : 12;  // 施术方角色所在格子的序号
                Chara sourceChara = _sourceUnitEntity.GetUnit() as Chara;
                sourceChara.MP -= _newSkill.Cost;
                _sourceUnitEntity.unitDisplay.FreshUnitUI(new UnitView(_sourceUnitEntity.GetUnit()));  // 即时刷新MP显示
            }

            // 2022/5/23： 暂且让一个技能的每个effect都单独生成一个时间步，以后也许可以想办法把一个技能的所有effect揉进一个时间步里
            LinkedList<EffectTimeStep> effects = new LinkedList<EffectTimeStep>();
            foreach (cfg.effect.EffectConstructProperties effectProperties in _newSkill.EffectConstructorList)
            {
                effects.AddLast(new EffectConstructor(effectProperties, _waitingPlayerSite).GenerateTimeStep(targetUnit));
            }
            EffectManager.Instance.TakeEffect(effects);
        }
    }
}