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
    public partial class MagicManager : Singleton<MagicManager>, IMessageHandler
    {
        //source
        private UnitEntity waitingUnitEntity;
        //target
        private UnitEntity targetUnitEntity;
        private GameObject spellCard;

        public BattleSite waitingPlayer;

        //攻击和治疗对象列表
        public List<bool> TargetList;

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

        public void MagicCancel()
        {
            attackWaiting = false;
            cureWaiting = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sourceUnit"></param>
        void AttackRequest((UnitEntity, GameObject) arg)
        {
            Debug.Log("Magic Attack Request");
            if (!attackWaiting)
            {
                attackWaiting = true;

                waitingPlayer = arg.Item1.ownerSite;
                waitingUnitEntity = arg.Item1;
                spellCard = arg.Item2;

                // 高亮传参
                TargetList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
            }
        }

        public void AttackConfirm(GameObject _targetUnit)
        {
            //魔法卡的攻击
            if (TargetList[_targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                attackWaiting = false;
                //Debug.Log("Magic Attack Confirm");
                targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
                MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.MagicSummon, spellCard);
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
                SpellCardEffect();
            }
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

                TargetList = BattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);

                if (Preprocessing())
                {
                    // 高亮传参
                    MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, TargetList);
                }
                else
                {
                    //SpellCardEffect();
                    CureConfirm(null);
                }
            }
        }

        public void CureConfirm(GameObject _targetUnit)
        {
            if (cureWaiting)
            {
                cureWaiting = false;

                if (_targetUnit != null)
                {
                    targetUnitEntity = _targetUnit.GetComponent<UnitEntity>();
                }
                
                MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.MagicSummon, spellCard);
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);

                SpellCardEffect();
            }


        }

        public void DrawRequest((UnitEntity, GameObject) arg)
        {
            waitingPlayer = arg.Item1.ownerSite;
            waitingUnitEntity = arg.Item1;
            spellCard = arg.Item2;

            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.MagicSummon, spellCard);

            SpellCardEffect();
        }

        void MagicRequest((UnitEntity, GameObject) arg)
        {
            SpellCard _spell = arg.Item2.GetComponent<SpellPlayerController>().spellCard;
            if (_spell is DamageSpellCard)
            {
                AttackRequest(arg);
            }
            else if (_spell is CureSpellCard)
            {
                CureRequest(arg);
            }
            else if(_spell is DrawSpellCard)
            {
                DrawRequest(arg);
            }
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message = null)
        {
        }

        public void Subscribe()
        {
            // 订阅单位发布的魔法释放请求消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<(UnitEntity, GameObject)>(MessageEvent.MagicEvent.MagicRequest, MagicRequest);

            // 订阅单位发布的攻击确认消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<GameObject>(MessageEvent.MagicEvent.AttackConfirm, AttackConfirm);

            // 订阅单位发布的治疗确认消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<GameObject>(MessageEvent.MagicEvent.CureConfirm, CureConfirm);

        }
    }
}
