using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{

    /// <summary>
    /// 用于执行技能、魔法卡的点击施放流程
    /// </summary>
    public class MagicManager : Singleton<MagicManager>
    {
        //source
        private UnitEntity waitingUnitEntity;
        //target
        private UnitEntity targetUnitEntity;

        private GameObject spellCardObject;

        private ISkill skill;
        private ISpell spell;

        public BattleSite waitingPlayer;

        //攻击和治疗对象列表
        public SelectTargetType targetType;
        public List<bool> TargetList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool isWaiting;


        private MagicManager()
        {
            MagicCancel();
        }

        public void Init() { }

        public void MagicCancel()
        {
            isWaiting = false;
        }

        /// <summary>
        /// 技能请求
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="skill"></param>
        /// <exception cref="System.Exception"></exception>
        public void SkillRequest(UnitEntity unit, ISkill skill)
        {
            this.skill = skill;

            targetType = skill.GetSelectType();
            if (targetType == SelectTargetType.None)
            {
                DirectSkill(unit);
            }

            MagicRequest(unit);
        }

        /// <summary>
        /// 魔法卡请求
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="_spellCardObject"></param>
        public void SpellRequest(UnitEntity unit, GameObject _spellCardObject)
        {
            var _spellCard = _spellCardObject.GetComponent<SpellPlayerController>().spellCard;
            this.spellCardObject = _spellCardObject;
            this.spell = _spellCard.Spell;

            targetType = spell.GetSelectType();
            if (targetType == SelectTargetType.None)
            {
                DirectSpell(unit);
            }

            MagicRequest(unit);

        }


        void MagicRequest(UnitEntity unit)
        {
            if (isWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            isWaiting = true;
            waitingPlayer = unit.ownerSite;
            waitingUnitEntity = unit;

            // 获取目标
            TargetList = BattleFieldManager.Instance.GetTargetListBySelectType(waitingPlayer, targetType);

            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
        }

        public void MagicConfirm(UnitEntity target)
        {
            if (!isWaiting)
            {
                return;
            }

            if (TargetList[target.carrier.serial])
            {
                targetUnitEntity = target;
            }

            isWaiting = false;
            Effect();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        private void DirectSkill(UnitEntity arg)
        {
            ClickManager.Instance.CancelAllClickAction();
            waitingUnitEntity = arg;
            Effect();
        }

        private void DirectSpell(UnitEntity sourceUnit)
        {
            ClickManager.Instance.CancelAllClickAction();
            waitingUnitEntity = sourceUnit;
            Effect();
        }

        private void Effect()
        {
            if (skill != null)
            {
                SkillEffect();
            }
            if (spell != null)
            {
                SpellEffect();
            }

        }

        private void SkillEffect()
        {
            Unit targetUnit = new Unit();
            Unit waitingUnit = waitingUnitEntity.GetUnit();

            if (targetUnitEntity != null)
            {
                targetUnit = targetUnitEntity.GetUnit();
            }

            skill.Release(waitingUnit, targetUnit);
            skill = null;

        }

        private void SpellEffect()
        {
            Unit targetUnit = new Unit();
            Unit waitingUnit = waitingUnitEntity.GetUnit();

            if (targetUnitEntity != null)
            {
                targetUnit = targetUnitEntity.GetUnit();
            }

            SpellCardUsing();
            spell.Release(waitingUnit, targetUnit);
            spell = null;

        }

        /// <summary>
        /// 使用后注销卡牌
        /// </summary>
        private void SpellCardUsing()
        {
            waitingPlayer = spellCardObject.GetComponent<SpellPlayerController>().playerSite;
            spellCardObject.SetActive(false);
            GenpaiPlayer player = GameContext.Instance.GetPlayerBySite(spellCardObject.GetComponent<SpellPlayerController>().playerSite);
            player.HandCardManager.HandCardsort(spellCardObject);
        }
    }
}
