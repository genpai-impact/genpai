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
        /// <summary>
        /// 当前角色
        /// </summary>
        private UnitEntity chara;

        /// <summary>
        /// 等待攻击单位
        /// </summary>
        private GameObject waitingUnit;

        /// <summary>
        /// 请求攻击玩家
        /// </summary>
        public BattleSite waitingPlayer;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool attackWaiting;

        /// <summary>
        /// 攻击对象
        /// </summary>
        public GameObject waitingTarget;

        /// <summary>
        /// 当前(上一次）可攻击列表，每调用CheckAttackable更新一次
        /// 考虑是否在回合开始就载入每个位置的可攻击列表
        /// </summary>
        public List<bool> atkableList;

        private MagicManager()
        {
            Subscribe();
            attackWaiting = false;
        }

        public void Init()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sourceUnit"></param>
        void MagicRequest(GameObject _sourceUnit)
        {
            Debug.Log("MagicRequest");
            SpellCard spellCard = _sourceUnit.GetComponent<SpellPlayerController>().spellCard;
            if(spellCard is DamageSpellCard)
            {
                if (!attackWaiting)
                {
                    attackWaiting = true;

                    chara = BattleFieldManager.Instance.bucketVertexs[5].unitCarry;

                    waitingPlayer = chara.GetComponent<UnitEntity>().ownerSite;
                    waitingUnit = _sourceUnit;

                    atkableList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, true);
                    Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, atkableList);
                }
            }
            

        }

        void MagicConfirm(GameObject _targetUnit)
        {
            Debug.Log("Magic Confirm");
            if (attackWaiting)
            {
                attackWaiting = false;

                Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight);

                if (atkableList[chara.GetComponent<UnitEntity>().carrier.serial])
                {
                    Attack(waitingUnit, _targetUnit);
                }

            }

        }

        public void Attack(GameObject _sourceUnit, GameObject _targetUnit)
        {
            DamageSpellCard source = _sourceUnit.GetComponent<SpellPlayerController>().spellCard as DamageSpellCard;
            UnitEntity target = _targetUnit.GetComponent<UnitEntity>();
            // 置位攻击来源行动状态
            chara.BeActed();

            LinkedList<List<IEffect>> DamageList = MakeAttack(source, target);

            // 将列表传予效果管理器(待改用消息系统实现
            EffectManager.Instance.TakeEffect(DamageList);
        }

        public LinkedList<List<IEffect>> MakeAttack(DamageSpellCard source, UnitEntity target)
        {
            LinkedList<List<IEffect>> DamageMessage = new LinkedList<List<IEffect>>();

            // 攻击受击时间错开方案
            // 创建攻击时间步
            List<IEffect> AttackList = new List<IEffect>();
            AttackList.Add(new Damage(chara, target, new DamageStruct(source.atk, source.atkElement)));
            DamageMessage.AddLast(AttackList);

            return DamageMessage;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message = null)
        {
            switch (areaCode)
            {
                case MessageArea.UI:
                    switch (eventCode)
                    {
                        case MessageEvent.UIEvent.AttackHighLight:
                            MessageManager.Instance.Dispatch<List<bool>>(areaCode, eventCode, message as List<bool>);
                            break;
                        case MessageEvent.UIEvent.ShutUpHighLight:
                            MessageManager.Instance.Dispatch<bool>(areaCode, eventCode, true);
                            break;
                    }
                    break;
            }
        }

        public void Subscribe()
        {
            // 订阅单位发布的魔法攻击请求消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<GameObject>(MessageEvent.MagicEvent.MagicRequest, MagicRequest);

            // 订阅单位发布的魔法攻击确认消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<GameObject>(MessageEvent.MagicEvent.MagicConfirm, MagicConfirm);
        }
    }
}
