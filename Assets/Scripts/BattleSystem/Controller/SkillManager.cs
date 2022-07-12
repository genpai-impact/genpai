using System.Collections.Generic;
using System.Linq;
using BattleSystem.Controller.Unit;
using BattleSystem.Service;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Effect;
using BattleSystem.Service.Player;
using BattleSystem.Service.Skill;
using BattleSystem.Service.Unit;
using DataScripts.DataLoader;
using Utils;
using Utils.Messager;
using UnityEngine;

namespace BattleSystem.Controller
{
    public class SkillManager : Singleton<SkillManager>
    {
        private BattleSite _waitingPlayerSite;
        private Skill _skill;
        private List<bool> _targetList;
        private UnitEntity _sourceUnitEntity;
        public bool IsWaiting { get; set; } = false;

        public void SkillCancel()
        {
            IsWaiting = false;
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
            IsWaiting = true;
            _sourceUnitEntity = sourceUnitEntity;
            _waitingPlayerSite = _sourceUnitEntity.ownerSite;
            _skill = SkillLoader.Skills.Single(newSkill => newSkill.SkillId == skillId);
            cfg.effect.TargetType selectType = _skill.EffectConstructorList.First().TargetType;  // 技能需要选取目标的类型

            // 不需选择目标的技能直接施放
            if (selectType == cfg.effect.TargetType.None)
            {
                SkillRelease();
                IsWaiting = false;
                return;
            }

            _targetList = BattleFieldManager.Instance.GetTargetListByTargetType(_waitingPlayerSite, selectType);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, _targetList);  // 高亮
        }

        public void SkillConfirm(UnitEntity targetUnitEntity)
        {
            if (!IsWaiting)
            {
                return;
            }
            //if (!_targetList[targetUnitEntity.Serial])    // 私以为这个不可能是空的，把这个判断去掉
            //{
            //    return;
            //}
            IsWaiting = false;
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);  // 取消格子高亮
            SkillRelease(targetUnitEntity.GetUnit());
        }

        private void SkillRelease(Service.Unit.Unit targetUnit = null)
        {
            // 如果是主动技能则消耗相应MP
            if (_skill.IsErupt)
            {
                Chara sourceChara = _sourceUnitEntity.GetUnit() as Chara;
                sourceChara.MP -= _skill.Cost;
                _sourceUnitEntity.unitDisplay.Display(_sourceUnitEntity.GetUnit().GetView());  // 即时刷新MP显示
            }

            // 2022/5/23： 暂且让一个技能的每个effect都单独生成一个时间步，以后也许可以想办法把一个技能的所有effect揉进一个时间步里
            LinkedList<EffectTimeStep> effects = new LinkedList<EffectTimeStep>();
            foreach (cfg.effect.EffectConstructProperties effectProperties in _skill.EffectConstructorList)
            {
                effects.AddLast(new EffectConstructor(effectProperties, _waitingPlayerSite).GenerateTimeStep(targetUnit));
            }
            EffectManager.Instance.TakeEffect(effects);
        }
    }
}