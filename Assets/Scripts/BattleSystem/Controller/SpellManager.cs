using System.Collections.Generic;
using cfg;
using cfg.effect;
using UnityEngine;
using Utils;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 用于取代MagicManager的新魔法卡管理器
    /// 结合NewSpellCard和EffectConstructor设计
    /// </summary>
    public class SpellManager : Singleton<SpellManager>
    {
        private BattleSite _waitingPlayerSite;
        public bool IsWaiting;

        private GameObject _spellCardObj;
        private NewSpellCard _spellCard;

        private EffectConstructor _constructor;
        private List<bool> _targetList;

        public void Init(){ }

        public void MagicCancel()
        {
            IsWaiting = false;
        }

        public void SpellRequest(GameObject spellCardObj)
        {
            if (IsWaiting) return;
            
            _spellCardObj = spellCardObj;
            CardPlayerController cpc = _spellCardObj.GetComponent<CardPlayerController>();
            _spellCard = cpc.Card as NewSpellCard;
            
            if(_spellCard == null) return;
            
            ClickManager.CancelAllClickAction();
            IsWaiting = true;
            _waitingPlayerSite = cpc.playerSite;

            // 根据角色元素检查施术模式（普通/超绝）
            bool buffFlag = _spellCard.BuffElement == GameContext.GetPlayerBySite(_waitingPlayerSite).Chara.AtkElement;
            _constructor = new EffectConstructor(
                buffFlag ? _spellCard.buffedEffect : _spellCard.baseEffect, 
                _waitingPlayerSite);
            
            // 无目标施术
            if (_constructor.TargetType == TargetType.None)
            {
                Spell();
                return;
            }
            
            // 高亮可选目标地块
            _targetList = _constructor.GetTargetsChoiceAble();
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, _targetList);
        }

        public void SpellConfirm(UnitEntity target)
        {
            if (!IsWaiting) return;
            if (!_targetList[target.Serial]) return;
            
            IsWaiting = false;
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            
            Spell(target.GetUnit());
        }

        public void Spell(Unit target = null)
        {
            EffectTimeStep effectTimeStep = _constructor.GenerateTimeStep(target);

            if (effectTimeStep != null)
            {
                EffectManager.Instance.InsertTimeStep(effectTimeStep);
            }
            
            SpellCardUsing();
        }
        
        /// <summary>
        /// 使用后注销卡牌
        /// </summary>
        private void SpellCardUsing()
        {
            _spellCardObj.SetActive(false);
            GenpaiPlayer player = GameContext.GetPlayerBySite(_waitingPlayerSite);
            player.HandCardManager.HandCardSort(_spellCardObj);
        }
    }
}