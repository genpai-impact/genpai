using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 战场交互管理器
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d96d961824ff003fdfe532
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
        public (bool bucketFree, List<bool> summonHoldList) CheckSummonFree(PlayerID _player)
        {
            bool bucketFree = false;
            List<bool> summonHoldList = new List<bool>();

            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                // 当前顺位格子能否召唤(怪兽卡)
                bool summonHold =
                    ((_player == PlayerID.P1) && P1Flag[i]) |
                    ((_player == PlayerID.P2) && P2Flag[i]);
                summonHold = summonHold & !bucketCarryFlag[i];
                summonHold = summonHold & !bucketCharaFlag[i];

                bucketFree = bucketFree | summonHold;

                summonHoldList.Add(summonHold);

            }
            return (bucketFree, summonHoldList);
        }

        /// <summary>
        /// 检测攻击请求
        /// </summary>
        /// <param name="_AtkPlayer">攻击请求玩家</param>
        /// <param name="_isRemote">是否远程攻击</param>
        /// <returns>可攻击格子列表</returns>
        public List<bool> CheckAttackable(PlayerID _AtkPlayer, bool _isRemote = false)
        {
            return P2Flag;
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
                P1Flag.Add(bucket.owner == PlayerID.P1);
                P2Flag.Add(bucket.owner == PlayerID.P2);
                BossFlag.Add(bucket.owner == PlayerID.Boss);
            }
            SetEdges();
        }

    }
}