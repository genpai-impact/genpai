using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class SummonManager : MonoSingleton<SummonManager>, IMessageHandler
    {


        private GameObject waitingUnit;
        private List<bool> waitingBucket;
        public bool summonWaiting;
        public PlayerID waitingPlayer;


        /// <summary>
        /// У��&ִ���ٻ�����
        /// </summary>
        /// <param name="_unitCard">�ٻ�ý�鵥λ��</param>
        public void SummonRequest(GameObject _unitCard)
        {
            PlayerID tempPlayer = _unitCard.GetComponent<BattleCard>().player;
            // ���õ���ս����������ѯ��ҳ��ؿ���
            (bool bucketFree, List<bool> summonHoldList) = BattleFieldManager.Instance.CheckSummonFree(tempPlayer);

            if (bucketFree)
            {
                waitingPlayer = tempPlayer;
                waitingUnit = _unitCard;
                waitingBucket = summonHoldList;
                summonWaiting = true;
                // ���ظ�����ʾ��Ϣ���ɳ���UI����������
                Dispatch(MessageArea.UI, 0, 0);
            }
        }


        /// <summary>
        /// ȷ���ٻ�����
        /// </summary>
        /// <param name="_targetBucket">�ٻ�Ŀ�����</param>
        public void SummonConfirm(BucketDisplay _targetBucket)
        {
            // ����׷���ٻ��������飨ս����������
            if (true && summonWaiting)
            {
                // ������ظ������ɳ���UI����������
                Dispatch(MessageArea.UI, 0, 0);
                Summon(waitingPlayer, waitingUnit, _targetBucket);
            }
        }

        /// <summary>
        /// ʵ���ٻ�
        /// </summary>
        /// <param name="_player">�����ٻ���ɫ</param>
        /// <param name="_unitCard">�ٻ��ο���λ�������޸�Ϊ��ID�����ݿ⣩</param>
        /// <param name="_targetBucket">�ٻ�Ŀ�����</param>
        public void Summon(PlayerID _player, GameObject _unitCard, BucketDisplay _targetBucket)
        {
            UnitCard summonCard = _unitCard.GetComponent<CardDisplay>().card as UnitCard;
            Unit unit = new Unit(summonCard, _player);
            // �ɳ��ع������ӹ��ٻ����̣�����obj��������Ϣ��
            // BattleFieldManager.Instance.
        }

        public void Execute(int eventCode, object message)
        {
            // �����ٻ���Ϣ
        }


        public void Subscribe()
        {
            // ��ģ�������׷�Ӷ���
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}