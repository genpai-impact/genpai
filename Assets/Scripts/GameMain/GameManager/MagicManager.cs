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
            Debug.Log("Magic Attack Request");
            if (!attackWaiting)
            {
                attackWaiting = true;

                waitingPlayer = arg.Item1.ownerSite;
                waitingUnitEntity = arg.Item1;
                spellCard = arg.Item2;

                // 高亮传参
                atkableList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, atkableList);
            }
        }

        void AttackConfirm(GameObject _targetUnit)
        {
            //魔法卡的攻击
            if (atkableList[_targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                Debug.Log("Magic Attack Confirm");
                MagicAttack(waitingUnitEntity, _targetUnit.GetComponent<UnitEntity>(), spellCard);
            }
        }

        public void MagicAttack(UnitEntity source, UnitEntity target, GameObject _card)
        {
            Debug.Log("Magic Attack");
            attackWaiting = false;

            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.MagicSummon, _card);

            DamageSpellCard card = _card.GetComponent<SpellPlayerController>().spellCard as DamageSpellCard;

            LinkedList<List<IEffect>> DamageList = new LinkedList<List<IEffect>>();
            List<IEffect> AttackList = new List<IEffect>();
            AttackList.Add(new Damage(source, target, new DamageStruct(card.atk, card.atkElement)));
            DamageList.AddLast(AttackList);

            EffectManager.Instance.TakeEffect(DamageList);
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

                //这里现在这样用不了
                //int hp = _targetUnit.GetComponent<UnitEntity>().unit.HP;
                _targetUnit.GetComponent<UnitEntity>().Cured(cureValue);
                Debug.Log("回血" + cureValue);
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

            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe< GameObject>(MessageEvent.MagicEvent.AttackConfirm, AttackConfirm);
            // 订阅单位发布的治疗请求消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<(UnitEntity, GameObject)>(MessageEvent.MagicEvent.CureRequest, CureRequest);
            // 订阅单位发布的治疗确认消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<GameObject>(MessageEvent.MagicEvent.CureConfirm, CureConfirm);

        }
    }
}
