using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class NewBucketEntity : MonoBehaviour
    {
        public int serial;

        public BucketUIController UIController;
        public BucketPlayerController PlayerController;

        // ����ʱ������
        private void Awake()
        {
            UIController = gameObject.AddComponent<BucketUIController>();
            PlayerController = gameObject.AddComponent<BucketPlayerController>();
        }

        // ��ȡ��������Ϣ��ʼ��
        public void Init()
        {
            NewBucket bucket = NewBattleFieldManager.Instance.GetBucketBySerial(serial);
            UIController.serial = bucket.serial;
            UIController.ownerSite = bucket.ownerSite;
        }
    }
}