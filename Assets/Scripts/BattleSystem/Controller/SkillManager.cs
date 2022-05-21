using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Messager;

namespace Genpai
{
    public class SkillManager : Singleton<SkillManager>
    {
        private bool _skillWaiting = false;
        private BattleSite _waitingPlayerSite;
        private NewSkill _newSkill;
        private List<bool> _targetList;

        public void SkillCancel()
        {
            //1
        }



        public void SkillRequest(int skillId, BattleSite playerSite)
        {
            ClickManager.CancelAllClickAction();
            _skillWaiting = true;
            _newSkill = SkillLoader.NewSkills.Single(newSkill => newSkill.SkillId == skillId);
            cfg.effect.TargetType selectType = _newSkill.EffectConstructorList.First().TargetType;  // 技能需要选取目标的类型

            // 不需选择目标的技能直接施放
            if (selectType == cfg.effect.TargetType.None)
            {
                SkillRelease();
                _skillWaiting = false;
                return;
            }

            _targetList = BattleFieldManager.Instance.GetTargetListByTargetType(playerSite, selectType);
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, _targetList);  // 高亮
        }

        public void SkillConfirm()
        {
            //1
        }

        private void SkillRelease()
        {
            //1
        }
    }
}