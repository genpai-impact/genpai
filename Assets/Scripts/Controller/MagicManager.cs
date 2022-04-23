using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 暂用MagicManager处理类型列表
    /// </summary>
    public enum MagicType
    {
        Magic
    }
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
        public bool needTarget;
        public List<bool> TargetList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool isWaiting;


        private MagicManager()
        {
            MagicCancel();
        }

        public void Init()
        {

        }

        public void MagicCancel()
        {
            isWaiting = false;
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
            // SummonManager.Instance.MagicSummon(spellCardObject);
            spell.Release(waitingUnit, targetUnit);
            spell = null;

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

        void MagicAttackRequest(UnitEntity arg)
        {
            if (isWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            isWaiting = true;
            waitingPlayer = arg.ownerSite;
            waitingUnitEntity = arg;
            // 高亮传参
            TargetList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
        }

        void CureRequest(UnitEntity arg)
        {
            if (isWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            isWaiting = true;
            waitingPlayer = arg.ownerSite;
            waitingUnitEntity = arg;
            TargetList = BattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);

            if (spellCardObject != null)
            {
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);

            }
            if (skill != null)
            {
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
            }
        }

        private void NotEnemyRequest(UnitEntity sourceUnit)
        {
            if (isWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            isWaiting = true;
            waitingPlayer = sourceUnit.ownerSite;
            waitingUnitEntity = sourceUnit;
            TargetList = BattleFieldManager.Instance.CheckNotEnemyUnit(waitingPlayer);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
        }

        public void MagicConfirm(UnitEntity target)
        {
            // 判断是否错误检查
            if (!isWaiting)
            {
                return;
            }

            // 判断目标是否可选
            if (TargetList[target.carrier.serial])
            {
                targetUnitEntity = target;
            }

            isWaiting = false;
            Effect();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        /// <summary>
        /// 暂用技能请求
        /// </summary>
        /// <param name="unitEntity"></param>
        /// <param name="skill"></param>
        /// <exception cref="System.Exception"></exception>
        public void SkillRequest(UnitEntity unitEntity, ISkill skill)
        {
            this.skill = skill;
            switch (skill.GetSkillDamageType())
            {

                case SkillDamageType.Attack:
                    MagicAttackRequest(unitEntity);
                    break;
                case SkillDamageType.Cure:
                    CureRequest(unitEntity);
                    break;
                case SkillDamageType.NotNeedTarget:
                    DirectSkill(unitEntity);
                    break;
                default:
                    throw new System.Exception("错误的技能类型");
            }
        }

        /// <summary>
        /// 暂用魔法卡请求
        /// </summary>
        /// <param name="sourceUnit"></param>
        /// <param name="_spellCardObject"></param>
        public void SpellRequest(UnitEntity sourceUnit, GameObject _spellCardObject)
        {
            var _spellCard = _spellCardObject.GetComponent<SpellPlayerController>().spellCard;
            this.spellCardObject = _spellCardObject;
            this.spell = _spellCard.Spell;
            switch (spell.GetSelectType())
            {
                case SelectTargetType.NotSelf:
                    MagicAttackRequest(sourceUnit);
                    break;
                case SelectTargetType.Self:
                    CureRequest(sourceUnit);
                    break;
                case SelectTargetType.NotEnemy:
                    NotEnemyRequest(sourceUnit);
                    break;
                case SelectTargetType.None:
                    DirectSpell(sourceUnit);
                    break;
            }
        }

        /// <summary>
        /// 使用后注销卡牌
        /// </summary>
        public void SpellCardUsing()
        {
            waitingPlayer = spellCardObject.GetComponent<SpellPlayerController>().playerSite;
            spellCardObject.SetActive(false);
            GenpaiPlayer player = GameContext.Instance.GetPlayerBySite(spellCardObject.GetComponent<SpellPlayerController>().playerSite);
            player.HandCardManager.HandCardsort(spellCardObject);
        }
    }
}
