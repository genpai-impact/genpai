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
    class MagicManager : Singleton<MagicManager>, IMessageHandler
    {
        private UnitEntity waitingUnitEntity;
        private GameObject spellCard;

        public BattleSite waitingPlayer;

        //事实上这里不仅是攻击列表，还是治疗列表
        public List<bool> atkableList;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool attackWaiting;
        public bool cureWaiting;


        private MagicManager()
        {
            Subscribe();
            attackWaiting = false;
            cureWaiting = false;
        }

        public void Init()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sourceUnit"></param>
        void AttackRequest((UnitEntity, GameObject) arg)
        {
            //魔法攻击或许该在魔法管理器实现，而不是攻击管理器
            //Debug.Log("Magic Attack Request");
            MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.MagicAttackRequest, arg);
        }


        void CureRequest((UnitEntity, GameObject) arg)
        {
            if (!cureWaiting)
            {
                Debug.Log("Cure Request");
                cureWaiting = true;

                waitingPlayer = arg.Item1.ownerSite;
                waitingUnitEntity = arg.Item1;
                spellCard = arg.Item2;

                //这里本来该检查可治疗的，但懒，用的检查可攻击
                //然而这不是治疗对方，所以玩家该反转
                //但是又会治疗boss，以后再说，能跑就行
                waitingPlayer = (waitingPlayer == BattleSite.P1) ? BattleSite.P2 : BattleSite.P1;
                // 高亮传参
                atkableList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, atkableList);
            }
        }

        void CureConfirm(GameObject _targetUnit)
        {
            if (cureWaiting)
            {
                cureWaiting = false;
                MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.MagicSummon, spellCard);
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight,true);

                CureSpellCard cureSpellCard = spellCard.GetComponent<SpellPlayerController>().spellCard as CureSpellCard;
                int cureValue = cureSpellCard.HP;

                //这里或许该传个buff什么的实现治疗
                //并且现在这样用不了
                _targetUnit.GetComponent<UnitEntity>().unit.HP += cureValue;
            }


        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message = null)
        {
        }

        public void Subscribe()
        {
            // 订阅单位发布的魔法攻击请求消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<(UnitEntity, GameObject)>(MessageEvent.MagicEvent.AttackRequest, AttackRequest);
            // 订阅单位发布的治疗请求消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<(UnitEntity, GameObject)>(MessageEvent.MagicEvent.CureRequest, CureRequest);
            // 订阅单位发布的治疗确认消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<GameObject>(MessageEvent.MagicEvent.CureConfirm, CureConfirm);

        }
    }
}
