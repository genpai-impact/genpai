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
        public List<bool> TargetList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool magicAttackWaiting;
        public bool cureWaiting;
        public bool notEnemyWaiting;

        private MagicManager()
        {
            MagicCancel();
        }

        public void Init()
        {

        }

        public void MagicCancel()
        {
            magicAttackWaiting = false;
            cureWaiting = false;
            notEnemyWaiting = false;
        }

        void MagicAttackRequest(UnitEntity arg)
        {
            if (magicAttackWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            magicAttackWaiting = true;
            waitingPlayer = arg.ownerSite;
            waitingUnitEntity = arg;
            // 高亮传参
            TargetList = NewBattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
        }

        public void MagicAttackConfirm(GameObject _targetUnit)
        {
            if (TargetList[_targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                magicAttackWaiting = false;
                targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
                SkillEffect();
                SpellEffect();
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            }
        }

        private void SkillEffect()
        {
            NewUnit targetUnit = new NewUnit();
            NewUnit waitingUnit = NewBattleFieldManager.Instance.GetBucketBySerial(waitingUnitEntity.carrier.serial).unitCarry;
            if (targetUnitEntity != null)
            {
                targetUnit = NewBattleFieldManager.Instance.GetBucketBySerial(targetUnitEntity.carrier.serial).unitCarry;
            }


            if (skill != null)
            {
                skill.Release(waitingUnit, targetUnit);
                skill = null;
            }
        }

        private void SpellEffect()
        {
            NewUnit waitingUnit = NewBattleFieldManager.Instance.GetBucketBySerial(waitingUnitEntity.carrier.serial).unitCarry;
            NewUnit targetUnit = NewBattleFieldManager.Instance.GetBucketBySerial(targetUnitEntity.carrier.serial).unitCarry;
            if (spell != null)
            {
                SummonManager.Instance.MagicSummon(spellCardObject);
                spell.Release(waitingUnit, targetUnit);
                spell = null;
            }
        }

        private void DirectSkill(UnitEntity arg)
        {
            ClickManager.Instance.CancelAllClickAction();
            waitingUnitEntity = arg;
            SkillEffect();
        }

        private void DirectSpell(UnitEntity sourceUnit)
        {
            ClickManager.Instance.CancelAllClickAction();
            waitingUnitEntity = sourceUnit;
            SpellEffect();
        }

        void CureRequest(UnitEntity arg)
        {
            if (cureWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            cureWaiting = true;
            waitingPlayer = arg.ownerSite;
            waitingUnitEntity = arg;
            TargetList = NewBattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);

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
            if (notEnemyWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            notEnemyWaiting = true;
            waitingPlayer = sourceUnit.ownerSite;
            waitingUnitEntity = sourceUnit;
            TargetList = NewBattleFieldManager.Instance.CheckNotEnemyUnit(waitingPlayer);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
        }

        public void NotEnemyConfirm(GameObject _targetUnit)
        {
            if (!notEnemyWaiting)
            {
                return;
            }
            notEnemyWaiting = false;
            if (_targetUnit != null)
            {
                targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
            }

            // 2022/4/5 loveYoimiya :
            // 虽然目前技能和魔法卡用的不是同一套目标类型枚举
            // 但为了魔法新建的这一套更合理
            // 以后要试着把技能的目标枚举也变为新的这一套
            // 故虽然技能暂时不可能用到此方法，也先把SkillEffect写在这里
            SkillEffect();
            SpellEffect();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        public void CureConfirm(GameObject _targetUnit)
        {
            if (!cureWaiting)
            {
                return;
            }
            cureWaiting = false;
            if (_targetUnit != null)
            {
                targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
            }
            SkillEffect();
            SpellEffect();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

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
    }
}
