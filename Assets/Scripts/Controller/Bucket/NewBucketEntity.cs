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

        // 启动时添加组件
        private void Awake()
        {
            UIController = gameObject.AddComponent<BucketUIController>();
            PlayerController = gameObject.AddComponent<BucketPlayerController>();
        }

        // 获取服务器信息初始化
        public void Init()
        {
            NewBucket bucket = NewBattleFieldManager.Instance.GetBucketBySerial(serial);
            UIController.serial = bucket.serial;
            UIController.ownerSite = bucket.ownerSite;
        }
    }
}