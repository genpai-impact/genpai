using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 战场交互管理器
    /// 管理战场中的格子信息及高亮
    /// </summary>
    public class BucketEntityManager : MonoSingleton<BucketEntityManager>
    {
        //通过格子的序号获取对应的物体/Buckert
        public Dictionary<int, GameObject> bucketObj = new Dictionary<int, GameObject>();
        public Dictionary<int, BucketEntity> buckets = new Dictionary<int, BucketEntity>();
        

        /// <summary>
        /// 更新战场状态函数
        /// 主要更新单位承载及嘲讽情况
        /// </summary>
        /// <param name="_serial">对应格子序号</param>
        /// <param name="state">召唤or阵亡</param>
        public void SetBucketCarryFlag(int _serial, UnitEntity unit = null)
        {
            buckets[_serial].BindUnit(unit);
        }

        public void SetBucketFall(int _serial)
        {
            Destroy(bucketObj[_serial].GetComponent<UnitDisplay>());
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


                buckets.Add(bucketEntity.serial, bucketEntity);
                bucketObj.Add(bucketEntity.serial, bucket);

            }

        }


        /// <summary>
        /// 快捷获取格子
        /// </summary>
        /// <param name="serial">格子编号</param>
        /// <returns></returns>
        public GameObject GetBucketBySerial(int serial)
        {
            return bucketObj[serial];
        }

        /// <summary>
        /// 通过unit快捷获取unitEntity
        /// </summary>
        public UnitEntity GetUnitEntityByUnit(Unit unit)
        {
            return buckets[unit.Carrier.serial].unitCarry;
        }

    }
}