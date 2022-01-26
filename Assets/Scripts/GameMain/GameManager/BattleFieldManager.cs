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
        public List<Bucket> bucketVertexs;
        private List<List<bool>> bucketEdges;

        // 格子归属标识(单次定义)
        private List<bool> bucketTauntFlag = new List<bool>();
        private List<bool> bucketCharaFlag = new List<bool>();
        private List<bool> P1Flag = new List<bool>();
        private List<bool> P2Flag = new List<bool>();
        private List<bool> BossFlag = new List<bool>();

        // 战场信息标识
        private List<bool> bucketCarryFlag = new List<bool>();

        private bool P1Taunt;
        private bool P2Taunt;

        public void SetEdges()
        {
            bucketEdges = new List<List<bool>>();
        }

        /// <summary>
        /// 检验召唤请求
        /// </summary>
        /// <param name="_player">待召唤玩家ID</param>
        /// <returns>元组（可否召唤，可进行召唤格子列表<bool>）</returns>
        public List<bool> CheckSummonFree(GenpaiPlayer _player, ref bool bucketFree)
        {
            List<bool> summonHoldList = new List<bool>();

            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                // 当前顺位格子能否召唤(怪兽卡)
                // 检出格子对应玩家
                bool summonHold =
                    ((_player.playerSite == PlayerSite.P1) && P1Flag[i]) |
                    ((_player.playerSite == PlayerSite.P2) && P2Flag[i]);
                // 检出未承载单位格子
                summonHold &= !bucketCarryFlag[i];
                // 检出非角色位置格子
                summonHold &= !bucketCharaFlag[i];


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
            int numOfBucket = bucketVertexs.Count;
            List<bool> attackableList = new List<bool>();

            foreach (Bucket grid in bucketVertexs)//非己方的非空格子均可
                attackableList.Add((grid.owner != _AtkPlayer.playerSite) & (grid.unitCarry != null));
            if (_isRemote == false)
            {
                for (int i = 0; i < numOfBucket; i++)//要么Boss，要么敌方嘲讽格
                    attackableList[i] = attackableList[i] & ((bucketVertexs[i].owner == PlayerSite.Boss) | bucketVertexs[i].tauntBucket);
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


        void Start()
        {
            // 收集场景部件
            foreach (var _bucket in bucketVertexsObj)
            {
                Bucket bucket = _bucket.GetComponent<BucketDisplay>().bucket;

                bucketVertexs.Add(bucket);
                bucketCarryFlag.Add(bucket.unitCarry != null);
                bucketTauntFlag.Add(bucket.tauntBucket);
                bucketCharaFlag.Add(bucket.charaBucket);

                // 格子归属标识
                P1Flag.Add(bucket.owner == PlayerSite.P1);
                P2Flag.Add(bucket.owner == PlayerSite.P2);
                BossFlag.Add(bucket.owner == PlayerSite.Boss);
            }
            SetEdges();
        }

    }
}