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
        // 格子储存模式待修正，可以考虑使用字典

        // 外部输入盒子列表
        public List<GameObject> bucketVertexsObj;
        public List<BucketEntity> bucketVertexs = new List<BucketEntity>();
        private List<List<bool>> bucketEdges;

        // 格子属性标识
        private Dictionary<int, bool> bucketTauntFlagD = new Dictionary<int, bool>();
        private Dictionary<int, bool> bucketCharaFlagD = new Dictionary<int, bool>();
        private Dictionary<int, BattleSite> bucketSiteFlagD = new Dictionary<int, BattleSite>();

        // 格子负载标识
        private Dictionary<int, bool> bucketCarryFlagD = new Dictionary<int, bool>();

        // 场地嘲讽状态标识
        private Dictionary<BattleSite, bool> SiteTauntFlagD = new Dictionary<BattleSite, bool>();


        public void SetEdges()
        {
            bucketEdges = new List<List<bool>>();
        }

        /// <summary>
        /// 更新战场状态函数
        /// 主要更新单位承载及嘲讽情况
        /// </summary>
        /// <param name="_serial">对应格子序号</param>
        /// <param name="state">召唤or阵亡</param>
        public void SetBucketCarryFlag(int _serial, bool state = true)
        {
            bucketCarryFlagD[_serial] = state;

            // 如果在嘲讽位召唤
            if (state && bucketTauntFlagD[_serial])
            {
                SiteTauntFlagD[bucketSiteFlagD[_serial]] = true;
                return;
            }

            // 如果嘲讽位阵亡
            if (!state && bucketTauntFlagD[_serial])
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

                bucketVertexs.Add(bucketEntity);
                bucketVertexsObj.Add(bucket);

            }

            SiteTauntFlagD.Add(BattleSite.P1, false);
            SiteTauntFlagD.Add(BattleSite.P2, false);

            SetEdges();
        }

        /// <summary>
        /// 检验召唤请求
        /// </summary>
        /// <param name="_player">待召唤玩家ID</param>
        /// <returns>元组（可否召唤，可进行召唤格子列表<bool>）</returns>
        public List<bool> CheckSummonFree(BattleSite playerSite, ref bool bucketFree)
        {
            List<bool> summonHoldList = new List<bool>();
            // Debug.LogWarning("count" + bucketVertexs.Count);
            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                // 当前顺位格子能否召唤(怪兽卡)

                // 检出格子对应玩家
                bool summonHold = playerSite == bucketSiteFlagD[i];
                // 检出未承载单位格子
                summonHold &= !bucketCarryFlagD[i];
                // 检出非角色位置格子
                summonHold &= !bucketCharaFlagD[i];

                bucketFree |= summonHold;
                summonHoldList.Add(summonHold);
            }
            return summonHoldList;
        }

        /// <summary>
        /// 检测攻击请求
        /// </summary>
        /// <param name="_AtkPlayer">攻击请求玩家</param>
        /// <param name="_isRemote">是否远程攻击</param>
        /// <returns>可攻击格子列表</returns>
        public List<bool> CheckAttackable(GenpaiPlayer _AtkPlayer, bool _isRemote = false)
        {

            List<bool> attackableList = new List<bool>();

            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                //非己方的非空格子均可
                attackableList.Add((bucketVertexs[i].owner != _AtkPlayer) && bucketCarryFlagD[i]);

            }

            // 是否为远程
            if (_isRemote)
            {
                return attackableList;
            }
            else
            {
                for (int i = 0; i < bucketVertexs.Count; i++)
                {
                    // 判断是否受嘲讽限制
                    if ((_AtkPlayer.playerSite == BattleSite.P1 && SiteTauntFlagD[BattleSite.P2]) ||
                        (_AtkPlayer.playerSite == BattleSite.P2 && SiteTauntFlagD[BattleSite.P1]))
                    {
                        // 进一步限制仅可选择嘲讽 & Boss地块
                        attackableList[i] &= bucketTauntFlagD[i] | (bucketSiteFlagD[i] == BattleSite.Boss);
                    }
                }
            }

            return attackableList;
        }

        /// <summary>
        /// 获取符合条件格子集合
        /// </summary>
        /// <param name="bucketMask">boolMask列表</param>
        /// <returns></returns>
        public List<GameObject> GetBucketSet(List<bool> bucketMask)
        {
            List<GameObject> buckets = new List<GameObject>();
            var bucket = bucketVertexsObj.GetEnumerator();
            var boolMask = bucketMask.GetEnumerator();
            while (bucket.MoveNext() && boolMask.MoveNext())
            {
                if (boolMask.Current)
                {
                    buckets.Add(bucket.Current);
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
            // 读取bucketEdges
            return bucketVertexsObj;
        }


    }
}