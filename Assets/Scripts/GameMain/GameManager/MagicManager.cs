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
        private ISkill skill;

        public BattleSite waitingPlayer;

        //攻击和治疗对象列表
        public List<bool> TargetList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool magicAttackWaiting;
        public bool cureWaiting;
        public bool buffWaiting;


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
            buffWaiting = false;
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
                skill.Release(waitingUnitEntity, targetUnitEntity);
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

        private void DirectSkill(UnitEntity arg)
        {
            ClickManager.Instance.CancelAllClickAction();
            waitingUnitEntity = arg;
            SkillEffect();
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

            if (spellCard != null)
            {
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
                //if (Preprocessing())
                //{
                //    MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
                //}
                //else
                //{
                //    CureConfirm(null);
                //}
            }
            if (skill != null)
            {
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
            }
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

        public void BuffRequest(UnitEntity arg)
        {
            if (buffWaiting)
            {
                return;
            }
            ClickManager.Instance.CancelAllClickAction();
            buffWaiting = true;
            waitingPlayer = arg.ownerSite;
            waitingUnitEntity = arg;
            TargetList = CheckBuffTargets(waitingPlayer);
            if (TargetList == null)  // 点击即施放的卡直接进入施放阶段
            {
                MagicEffect();
            }
            else
            {
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
            }
        }

        public void BuffConfirm(GameObject _targetUnit)
        {
            if (TargetList[_targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                buffWaiting = false;
                targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
                MagicEffect();
                SkillEffect();
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            }
        }
        /// <summary>
        /// 寻找Buff可作用列表
        /// </summary>
        /// <param name="_playerSite"></param>
        /// <returns></returns>
        public List<bool> CheckBuffTargets(BattleSite _playerSite)
        {
            List<bool> retList = new List<bool>();
            SpellCard _spellCard = spellCard.GetComponent<SpellPlayerController>().spellCard;
            switch (_spellCard.targetType)
            {
                case TargetType.Enemy:
                    retList = BattleFieldManager.Instance.CheckEnemyUnit(waitingPlayer);
                    break;
                case TargetType.Self:
                    retList = BattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);
                    break;
                case TargetType.NotEnemy:
                    retList = BattleFieldManager.Instance.CheckNotEnemyUnit(waitingPlayer);
                    break;
                case TargetType.None:
                case TargetType.All:
                    return null;
            }
            return retList;
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
            else if(_spell is BuffSpellCard)
            {
                BuffRequest(unitEntity);
            }
        }
    }
}
