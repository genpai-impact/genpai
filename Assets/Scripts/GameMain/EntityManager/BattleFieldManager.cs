using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 战场交互管理器
    /// 管理战场中的格子信息及高亮
    /// </summary>
    public class BattleFieldManager : MonoSingleton<BattleFieldManager>
    {

        public Dictionary<int, GameObject> bucketVertexsObj = new Dictionary<int, GameObject>();
        public Dictionary<int, BucketEntity> bucketVertexs = new Dictionary<int, BucketEntity>();

        // 格子负载标识
        private Dictionary<int, bool> bucketCarryFlagD = new Dictionary<int, bool>();

        /// <summary>
        /// 更新战场状态函数
        /// 主要更新单位承载及嘲讽情况
        /// </summary>
        /// <param name="_serial">对应格子序号</param>
        /// <param name="state">召唤or阵亡</param>
        public void SetBucketCarryFlag(int _serial, UnitEntity unit = null)
        {
            bucketCarryFlagD[_serial] = unit != null;

            bucketVertexs[_serial].BindUnit(unit);

        }

        /// <summary>
        /// 初始化场地信息
        /// </summary>
        void Awake()
        {

            GameObject[] stancesObject = GameObject.FindGameObjectsWithTag("stance");

            foreach (GameObject bucket in stancesObject)
            {
                bucket.GetComponent<BucketUIController>().Init();

                BucketEntity bucketEntity = bucket.GetComponent<BucketEntity>();
                bucketCarryFlagD.Add(bucketEntity.serial, bucketEntity.unitCarry != null);

                bucketVertexs.Add(bucketEntity.serial, bucketEntity);
                bucketVertexsObj.Add(bucketEntity.serial, bucket);

            }

        }


        /// <summary>
        /// 快捷获取格子
        /// </summary>
        /// <param name="serial">格子编号</param>
        /// <returns></returns>
        public GameObject GetBucketBySerial(int serial)
        {
            return bucketVertexsObj[serial];
        }


    }
}