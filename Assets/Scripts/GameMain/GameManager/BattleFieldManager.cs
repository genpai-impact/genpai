using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class BattleFieldManager : MonoSingleton<BattleFieldManager>
    {
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


        public (bool bucketFree, List<bool> summonHoldList) CheckSummonFree(PlayerID _player)
        {
            bool bucketFree = false;
            List<bool> summonHoldList = new List<bool>();

            for (int i = 0; i < bucketVertexs.Count; i++)
            {
                // 当前顺位格子能否召唤
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

        // 根据Mask返回格子集合
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

        // AOE查找待实现
        public List<GameObject> GetNeighbors(GameObject bucket)
        {
            return bucketVertexsObj;
        }


        // Start is called before the first frame update
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