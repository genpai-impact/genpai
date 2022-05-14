using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using cfg.effect;  

namespace Genpai
{

    /// <summary>
    /// 用于执行技能、魔法卡的点击施放流程
    /// </summary>
    public class MagicManager : Singleton<MagicManager>
    {
        private UnitEntity _waitingUnitEntity;
        private UnitEntity _targetUnitEntity;

        private GameObject _spellCardObject;

        private ISkill _skill;
        private ISpell _spell;

        private BattleSite _waitingPlayer;

        private TargetType _targetType;
        private List<bool> _targetList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool IsWaiting;
        
        private MagicManager()
        {
            MagicCancel();
        }

        public void Init() { }

        public void MagicCancel()
        {
            IsWaiting = false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// 技能请求
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="skill"></param>
        /// <exception cref="System.Exception"></exception>
        public void SkillRequest(UnitEntity unit, ISkill skill)
        {
            this._skill = skill;

            _targetType = skill.GetSelectType();
            if (_targetType == TargetType.None)
            {
                DirectSkill(unit);
            }

            MagicRequest(unit);
        }

        /// <summary>
        /// 魔法卡请求
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="spellCardObject"></param>
        public void SpellRequest(UnitEntity unit, GameObject spellCardObject)
        {
            SpellCard spellCard = spellCardObject.GetComponent<CardPlayerController>().Card as SpellCard;
            this._spellCardObject = spellCardObject;
            if (spellCard != null) this._spell = spellCard.Spell;

            _targetType = _spell.GetSelectType();
            if (_targetType == TargetType.None)
            {
                DirectSpell(unit);
            }

            MagicRequest(unit);

        }


        void MagicRequest(UnitEntity unit)
        {
            if (IsWaiting)
            {
                return;
            }
            ClickManager.CancelAllClickAction();
            IsWaiting = true;
            _waitingPlayer = unit.ownerSite;
            _waitingUnitEntity = unit;

            // 获取目标
            _targetList = BattleFieldManager.Instance.GetTargetListByTargetType(_waitingPlayer, _targetType);

            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, _targetList);
        }

        public void MagicConfirm(UnitEntity target)
        {
            if (!IsWaiting)
            {
                return;
            }

            if (_targetList[target.carrier.serial])
            {
                _targetUnitEntity = target;
            }

            IsWaiting = false;
            Effect();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        private void DirectSkill(UnitEntity arg)
        {
            ClickManager.CancelAllClickAction();
            _waitingUnitEntity = arg;
            Effect();
        }

        private void DirectSpell(UnitEntity sourceUnit)
        {
            ClickManager.CancelAllClickAction();
            _waitingUnitEntity = sourceUnit;
            Effect();
        }

        private void Effect()
        {
            if (_skill != null)
            {
                SkillEffect();
            }
            if (_spell != null)
            {
                SpellEffect();
            }

        }

        private void SkillEffect()
        {
            Unit targetUnit = new Unit();
            Unit waitingUnit = _waitingUnitEntity.GetUnit();

            if (_targetUnitEntity != null)
            {
                targetUnit = _targetUnitEntity.GetUnit();
            }

            _skill.Release(waitingUnit, targetUnit);
            _skill = null;

        }

        private void SpellEffect()
        {
            Unit targetUnit = new Unit();
            Unit waitingUnit = _waitingUnitEntity.GetUnit();

            if (_targetUnitEntity != null)
            {
                targetUnit = _targetUnitEntity.GetUnit();
            }

            SpellCardUsing();
            _spell.Release(waitingUnit, targetUnit);
            _spell = null;

        }

        /// <summary>
        /// 使用后注销卡牌
        /// </summary>
        private void SpellCardUsing()
        {
            _waitingPlayer = _spellCardObject.GetComponent<CardPlayerController>().playerSite;
            _spellCardObject.SetActive(false);
            GenpaiPlayer player = GameContext.GetPlayerBySite(_waitingPlayer);
            player.HandCardManager.HandCardSort(_spellCardObject);
        }
    }
}
