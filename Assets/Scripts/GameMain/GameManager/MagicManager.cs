using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 技能管理器，受理技能攻击请求
    /// 现在给魔法卡使用，参考AttackManager实现
    /// </summary>
    public partial class MagicManager : Singleton<MagicManager>
    {
        //source
        private UnitEntity waitingUnitEntity;
        //target
        private UnitEntity targetUnitEntity;
        private GameObject spellCard;
        private BaseSkill skill;

        public BattleSite waitingPlayer;

        //攻击和治疗对象列表
        public List<bool> TargetList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool magicAttackWaiting;
        public bool cureWaiting;


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
            //spellCard = null;
            //skill = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sourceUnit"></param>
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
            TargetList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
        }

        public void MagicAttackConfirm(GameObject _targetUnit)
        {
            if (TargetList[_targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                magicAttackWaiting = false;
                targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
                MagicEffect();
                SkillEffect();
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            }
        }

        private void SkillEffect()
        {
            if (skill != null)
            {
                skill.Release(targetUnitEntity);
                skill = null;
            }
        }

        private void MagicEffect()
        {
            if (spellCard != null)
            {
                SummonManager.Instance.MagicSummon(spellCard);
                SpellCardEffect();
                spellCard = null;
            }
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
            TargetList = BattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);
            if (Preprocessing())
            {
                // 高亮传参
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
                return;
            }
            CureConfirm(null);
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
            MagicEffect();
            SkillEffect();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        public void DrawRequest(UnitEntity arg)
        {
            waitingPlayer = arg.ownerSite;
            waitingUnitEntity = arg;
            SummonManager.Instance.MagicSummon(spellCard);
            SpellCardEffect();
            ClickManager.Instance.CancelAllClickAction();
        }

        public void SkillRequest(UnitEntity unitEntity, BaseSkill skill)
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
                default:
                    throw new System.Exception("错误的技能类型");
            }
        }

        public void MagicRequest(UnitEntity unitEntity, GameObject spellCard)
        {
            SpellCard _spell = spellCard.GetComponent<SpellPlayerController>().spellCard;
            this.spellCard = spellCard;
            if (_spell is DamageSpellCard)
            {
                MagicAttackRequest(unitEntity);
            }
            else if (_spell is CureSpellCard)
            {
                CureRequest(unitEntity);
            }
            else if(_spell is DrawSpellCard)
            {
                DrawRequest(unitEntity);
            }
        }
    }
}
