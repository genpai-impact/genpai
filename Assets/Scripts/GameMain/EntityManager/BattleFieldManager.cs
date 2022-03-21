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

        // 格子属性标识
        private Dictionary<int, bool> bucketTauntFlagD = new Dictionary<int, bool>();
        private Dictionary<int, bool> bucketCharaFlagD = new Dictionary<int, bool>();
        private Dictionary<int, BattleSite> bucketSiteFlagD = new Dictionary<int, BattleSite>();

        // 格子负载标识
        private Dictionary<int, bool> bucketCarryFlagD = new Dictionary<int, bool>();

        // 场地嘲讽状态标识
        private Dictionary<BattleSite, bool> SiteTauntFlagD = new Dictionary<BattleSite, bool>();


        /// <summary>
        /// 更新战场状态函数
        /// 主要更新单位承载及嘲讽情况
        /// </summary>
        /// <param name="_serial">对应格子序号</param>
        /// <param name="state">召唤or阵亡</param>
        public void SetBucketCarryFlag(int _serial, UnitEntity unit = null)
        {
            // unit == null 表示单位死亡
            if (unit == null)
            {
                bucketCarryFlagD[_serial] = false;
                bucketVertexs[_serial].BindUnit(unit);
                // 判断是否召唤位单位死亡
                if (bucketTauntFlagD[_serial])
                {
                    if (bucketSiteFlagD[_serial] == BattleSite.P1)
                    {
                        SiteTauntFlagD[BattleSite.P1] = bucketCarryFlagD[1] || bucketCarryFlagD[2];
                    }
                    if (bucketSiteFlagD[_serial] == BattleSite.P2)
                    {
                        SiteTauntFlagD[BattleSite.P2] = bucketCarryFlagD[8] || bucketCarryFlagD[9];
                    }
                }
            }
            // 否则召唤
            else
            {
                bucketCarryFlagD[_serial] = true;
                bucketVertexs[_serial].BindUnit(unit);
                if (bucketTauntFlagD[_serial])
                {
                    SiteTauntFlagD[bucketSiteFlagD[_serial]] = true;
                    return;
                }
            }

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

                bucketTauntFlagD.Add(bucketEntity.serial, bucketEntity.tauntBucket);
                bucketCharaFlagD.Add(bucketEntity.serial, bucketEntity.charaBucket);
                bucketCarryFlagD.Add(bucketEntity.serial, bucketEntity.unitCarry != null);
                bucketSiteFlagD.Add(bucketEntity.serial, bucket.GetComponent<BucketUIController>().ownerSite);

                bucketVertexs.Add(bucketEntity.serial, bucketEntity);
                bucketVertexsObj.Add(bucketEntity.serial, bucket);

            }

            SiteTauntFlagD.Add(BattleSite.P1, false);
            SiteTauntFlagD.Add(BattleSite.P2, false);
        }

        /// <summary>
        /// 检验召唤请求
        /// </summary>
        /// <param name="_player">待召唤玩家ID</param>
        /// <returns>元组（可否召唤，可进行召唤格子列表<bool>）</returns>
        public List<bool> CheckSummonFree(BattleSite playerSite, ref bool bucketFree)
        {
            List<bool> summonHoldList = new List<bool>();

            for (int i = 0; i < bucketVertexs.Count; i++)
            {

                // 检出格子对应玩家
                bool summonHold = (playerSite == bucketSiteFlagD[i]);
                // 检出未承载单位格子
                summonHold &= !bucketCarryFlagD[i];
                // 检出非角色位置格子
                summonHold &= !bucketCharaFlagD[i];

                bucketFree |= summonHold;
                summonHoldList.Add(summonHold);
            }
            return summonHoldList;
        }

        private BattleSite RevertSite(BattleSite playerSite)
        {
            if (playerSite == BattleSite.P1)
            {
                return BattleSite.P2;
            }
            if (playerSite == BattleSite.P2)
            {
                return BattleSite.P1;
            }
            throw new System.Exception("错误的阵营信息");
        }

        /// <summary>
        /// 获取所有敌人
        /// </summary>
        /// <param name="playerSite">需要获取谁的敌人</param>
        /// <returns></returns>
        public List<bool> CheckEnemyUnit(BattleSite playerSite)
        {
            BattleSite enemySite = RevertSite(playerSite);
            List<bool> ownList = new List<bool>();

            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                if (i == 0)
                {
                    // boss固定0号
                    ownList.Add(bucketCarryFlagD[0]);
                    continue;
                }
                bool ownUnit = (enemySite == bucketSiteFlagD[i]);
                ownUnit &= bucketCarryFlagD[i];
                ownList.Add(ownUnit);
            }
            return ownList;
        }

        /// <summary>
        /// 检测当前己方场上单位
        /// </summary>
        /// <param name="playerSite"></param>
        /// <returns></returns>
        public List<bool> CheckOwnUnit(BattleSite playerSite)
        {
            List<bool> ownList = new List<bool>();

            for(int i = 0; i < bucketVertexs.Count; i++)
            {
                bool ownUnit = (playerSite == bucketSiteFlagD[i]);
                ownUnit &= bucketCarryFlagD[i];
                ownList.Add(ownUnit);
            }
            return ownList;
        }

        /// <summary>
        /// 检测攻击请求
        /// </summary>
        /// <param name="playerSite">攻击请求玩家</param>
        /// <param name="_isRemote">是否远程攻击</param>
        /// <returns>可攻击格子列表</returns>
        public List<bool> CheckAttackable(BattleSite playerSite, bool _isRemote)
        {

            List<bool> attackableList = new List<bool>();

            // 返回Boss可攻击列表
            if (playerSite == BattleSite.Boss)
            {
                attackableList.Add(false);
                for (int i = 1; i < bucketCarryFlagD.Count; i++)
                {
                    attackableList.Add(bucketCarryFlagD[i]);
                }
                return attackableList;
            }

            // 返回玩家可攻击列表
            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                // 非己方的非空格子均可
                attackableList.Add((bucketVertexs[i].ownerSite != playerSite) && bucketCarryFlagD[i]);
            }

            // 远程单位直接返回
            if (_isRemote)
            {
                return attackableList;
            }

            // 判断是否受嘲讽限制
            if ((playerSite == BattleSite.P1 && SiteTauntFlagD[BattleSite.P2]) ||
                (playerSite == BattleSite.P2 && SiteTauntFlagD[BattleSite.P1]))
            {
                // 近战单位判断嘲讽
                for (int i = 0; i < bucketVertexs.Count; i++)
                {
                    // 进一步限制仅可选择嘲讽 & Boss地块
                    attackableList[i] &= bucketTauntFlagD[i] | (bucketSiteFlagD[i] == BattleSite.Boss);
                }
            }

            return attackableList;
        }

        /// <summary>
        /// 获取Boss单体出伤优先级
        /// </summary>
        /// <param name="site">Boss优先攻击阵营</param>
        /// <returns>Boss攻击格子</returns>
        public GameObject GetDangerousBucket(BattleSite site)
        {
            int count = bucketVertexsObj.Count;
            // 阵营偏移
            int bias = (site == BattleSite.P1) ? 1 : 8;

            // 根据偏移顺序查找全场格子
            for (int i = bias; i < bias + count; i++)
            {
                // 跳过boss自身
                if (i == 0) continue;

                if (bucketCarryFlagD[i % count]) { return bucketVertexsObj[i % count]; }
            }
            return null;
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

        /// <summary>
        /// 获取符合条件格子集合
        /// </summary>
        /// <param name="bucketMask">boolMask列表</param>
        /// <returns></returns>
        public List<GameObject> GetBucketSet(List<bool> bucketMask)
        {
            List<GameObject> buckets = new List<GameObject>();

            foreach (KeyValuePair<int, GameObject> kvp in bucketVertexsObj)
            {
                if (bucketMask[kvp.Key])
                {
                    buckets.Add(kvp.Value);
                }
            }

            return buckets;
        }

        /// <summary>
        /// 获取相邻格子（服务于AOE
        /// </summary>
        /// <param name="bucket">待获取格子</param>
        /// <returns></returns>
        public List<GameObject> GetNeighbors(GameObject bucket)
        {
            List<GameObject> neighbors = new List<GameObject>();

            int index = bucket.GetComponent<BucketEntity>().serial;
            int correct = 0;
            if (index > 7)
            {
                index -= 7;
                correct = 7;
            }
            switch (index)
            {
                case 1:
                    neighbors.Add(bucketVertexsObj[correct + 2]);
                    neighbors.Add(bucketVertexsObj[correct + 3]);
                    neighbors.Add(bucketVertexsObj[correct + 5]);
                    break;
                case 2:
                    neighbors.Add(bucketVertexsObj[correct + 1]);
                    neighbors.Add(bucketVertexsObj[correct + 4]);
                    neighbors.Add(bucketVertexsObj[correct + 5]);
                    break;
                case 3:
                    neighbors.Add(bucketVertexsObj[correct + 1]);
                    neighbors.Add(bucketVertexsObj[correct + 5]);
                    neighbors.Add(bucketVertexsObj[correct + 6]);
                    break;
                case 4:
                    neighbors.Add(bucketVertexsObj[correct + 2]);
                    neighbors.Add(bucketVertexsObj[correct + 5]);
                    neighbors.Add(bucketVertexsObj[correct + 7]);
                    break;
                case 5:
                    neighbors.Add(bucketVertexsObj[correct + 1]);
                    neighbors.Add(bucketVertexsObj[correct + 2]);
                    neighbors.Add(bucketVertexsObj[correct + 3]);
                    neighbors.Add(bucketVertexsObj[correct + 4]);
                    neighbors.Add(bucketVertexsObj[correct + 6]);
                    neighbors.Add(bucketVertexsObj[correct + 7]);
                    break;
                case 6:
                    neighbors.Add(bucketVertexsObj[correct + 3]);
                    neighbors.Add(bucketVertexsObj[correct + 5]);
                    neighbors.Add(bucketVertexsObj[correct + 7]);
                    break;
                case 7:
                    neighbors.Add(bucketVertexsObj[correct + 4]);
                    neighbors.Add(bucketVertexsObj[correct + 5]);
                    neighbors.Add(bucketVertexsObj[correct + 6]);
                    break;
            }

            return neighbors;
        }


    }
}