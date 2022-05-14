using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cfg.effect;

namespace Genpai
{
    /// <summary>
    /// 战场交互管理器
    /// 管理战场中的格子信息及高亮
    /// </summary>
    public class BattleFieldManager : Singleton<BattleFieldManager>
    {
        public readonly int MAX_BUCKET_NUM = 15;
        public readonly Dictionary<int, Bucket> Buckets = new Dictionary<int, Bucket>();

        // 格子属性标识
        private readonly Dictionary<int, bool> _bucketTauntFlagD = new Dictionary<int, bool>();
        private readonly Dictionary<int, bool> _bucketCharaFlagD = new Dictionary<int, bool>();
        private readonly Dictionary<int, BattleSite> _bucketSiteFlagD = new Dictionary<int, BattleSite>();

        // 格子负载标识
        private readonly Dictionary<int, bool> _bucketCarryFlagD = new Dictionary<int, bool>();

        // 场地嘲讽状态标识
        private readonly Dictionary<BattleSite, bool> _siteTauntFlagD = new Dictionary<BattleSite, bool>();


        /// <summary>
        /// 更新战场状态函数
        /// 主要更新单位承载及嘲讽情况
        /// </summary>
        /// <param name="serial">对应格子序号</param>
        /// <param name="unit"></param>
        public void SetBucketCarryFlag(int serial, Unit unit = null)
        {

            // unit == null 表示单位死亡
            if (unit == null)
            {
                _bucketCarryFlagD[serial] = false;
                Buckets[serial].BindUnit(null);
                // 判断是否召唤位单位死亡
                if (!_bucketTauntFlagD[serial]) return;
                
                switch (_bucketSiteFlagD[serial])
                {
                    case BattleSite.P1:
                        _siteTauntFlagD[BattleSite.P1] = _bucketCarryFlagD[1] || _bucketCarryFlagD[2];
                        break;
                    case BattleSite.P2:
                        _siteTauntFlagD[BattleSite.P2] = _bucketCarryFlagD[8] || _bucketCarryFlagD[9];
                        break;
                }
            }
            // 否则召唤
            else
            {
                _bucketCarryFlagD[serial] = true;
                Buckets[serial].BindUnit(unit);
                if (!_bucketTauntFlagD[serial]) return;
                
                _siteTauntFlagD[_bucketSiteFlagD[serial]] = true;
            }

        }

        public void Init()
        {
            for (var i = 0; i < MAX_BUCKET_NUM; i++)
            {
                if (Buckets.ContainsKey(i)) continue;
                
                Bucket bucketEntity = new Bucket(i);
                Buckets.Add(i, bucketEntity);

                _bucketTauntFlagD.Add(bucketEntity.serial, bucketEntity.tauntBucket);
                _bucketCharaFlagD.Add(bucketEntity.serial, bucketEntity.charaBucket);
                _bucketCarryFlagD.Add(bucketEntity.serial, bucketEntity.unitCarry != null);
                _bucketSiteFlagD.Add(bucketEntity.serial, bucketEntity.ownerSite);

            }
            if (!_siteTauntFlagD.ContainsKey(BattleSite.P1))
            {
                _siteTauntFlagD.Add(BattleSite.P1, false);
            }
            if (!_siteTauntFlagD.ContainsKey(BattleSite.P2))
            {
                _siteTauntFlagD.Add(BattleSite.P2, false);
            }


        }


        /// <summary>
        /// 检验召唤请求
        /// </summary>
        /// <param name="playerSite"></param>
        /// <param name="bucketFree"></param>
        public List<bool> CheckSummonFree(BattleSite playerSite, ref bool bucketFree)
        {
            List<bool> summonHoldList = new List<bool>();

            for (int i = 0; i < MAX_BUCKET_NUM; i++)
            {

                // 检出格子对应玩家
                bool summonHold = (playerSite == _bucketSiteFlagD[i]);
                // 检出未承载单位格子
                summonHold &= !_bucketCarryFlagD[i];
                // 检出非角色位置格子
                summonHold &= !_bucketCharaFlagD[i];

                bucketFree |= summonHold;
                summonHoldList.Add(summonHold);
            }
            return summonHoldList;
        }

        private static BattleSite RevertSite(BattleSite playerSite)
        {
            return playerSite switch
            {
                BattleSite.P1 => BattleSite.P2,
                BattleSite.P2 => BattleSite.P1,
                _ => throw new System.Exception("错误的阵营信息")
            };
        }

        /// <summary>
        /// 根据技能/魔法的选择类型确定可选目标
        /// 待All in one的优雅重构
        /// </summary>
        /// <param name="playerSite">施术者阵营</param>
        /// <param name="type">法术目标类型</param>
        /// <returns></returns>
        public List<bool> GetTargetListByTargetType(BattleSite playerSite, TargetType type)
        {
            List<bool> list = new List<bool>();
            switch (type)
            {
                case TargetType.NotSelf:
                    list = CheckAttackable(playerSite, true);
                    break;
                case TargetType.Self:
                    list = CheckOwnUnit(playerSite);
                    break;
                case TargetType.NotEnemy:
                    list = CheckNotEnemyUnit(playerSite);
                    break;
                case TargetType.All:
                    list = new List<bool>(15){true};
                    break;
            }
            return list;
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

            for (int i = 0; i < MAX_BUCKET_NUM; i++)
            {
                if (i == 0)
                {
                    // boss固定0号
                    ownList.Add(_bucketCarryFlagD[0]);
                    continue;
                }
                bool ownUnit = (enemySite == _bucketSiteFlagD[i]);
                ownUnit &= _bucketCarryFlagD[i];
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

            for (int i = 0; i < MAX_BUCKET_NUM; i++)
            {
                bool ownUnit = (playerSite == _bucketSiteFlagD[i]);
                ownUnit &= _bucketCarryFlagD[i];
                ownList.Add(ownUnit);
            }
            return ownList;
        }

        /// <summary>
        /// 检测攻击请求
        /// </summary>
        /// <param name="playerSite">攻击请求玩家</param>
        /// <param name="isRemote">是否远程攻击</param>
        /// <returns>可攻击格子列表</returns>
        public List<bool> CheckAttackable(BattleSite playerSite, bool isRemote)
        {

            List<bool> attackableList = new List<bool>();

            // 返回Boss可攻击列表
            if (playerSite == BattleSite.Boss)
            {
                attackableList.Add(false);
                for (int i = 1; i < _bucketCarryFlagD.Count; i++)
                {
                    attackableList.Add(_bucketCarryFlagD[i]);
                }
                return attackableList;
            }

            // 返回玩家可攻击列表
            for (int i = 0; i < MAX_BUCKET_NUM; i++)
            {
                // 非己方的非空格子均可
                attackableList.Add((Buckets[i].ownerSite != playerSite) && _bucketCarryFlagD[i]);
            }

            // 远程单位直接返回
            if (isRemote)
            {
                return attackableList;
            }

            // 判断是否受嘲讽限制
            if ((playerSite == BattleSite.P1 && _siteTauntFlagD[BattleSite.P2]) ||
                (playerSite == BattleSite.P2 && _siteTauntFlagD[BattleSite.P1]))
            {
                // 近战单位判断嘲讽
                for (int i = 0; i < MAX_BUCKET_NUM; i++)
                {
                    // 进一步限制仅可选择嘲讽 & Boss地块
                    attackableList[i] &= _bucketTauntFlagD[i] | (_bucketSiteFlagD[i] == BattleSite.Boss);
                }
            }

            return attackableList;
        }

        public List<bool> CheckNotEnemyUnit(BattleSite playerSite)
        {
            List<bool> notEnemyList = new List<bool>();
            for (int i = 0; i < MAX_BUCKET_NUM; i++)
            {
                notEnemyList.Add((Buckets[i].ownerSite == playerSite
                    || Buckets[i].ownerSite == BattleSite.Boss) && _bucketCarryFlagD[i]);
            }
            return notEnemyList;
        }

        public bool CheckCarryFlag(int index)
        {
            return _bucketCarryFlagD[index];
        }
        /// <summary>
        /// 获取Boss单体出伤优先级
        /// </summary>
        /// <param name="site">Boss优先攻击阵营</param>
        /// <returns>Boss攻击格子</returns>
        public Bucket GetDangerousBucket(BattleSite site)
        {
            int count = Buckets.Count;
            // 阵营偏移
            int bias = (site == BattleSite.P1) ? 1 : 8;

            // 根据偏移顺序查找全场格子
            for (int i = bias; i < bias + count; i++)
            {
                // 跳过boss自身
                if (i == 0) continue;

                if (_bucketCarryFlagD[i % count]) { return Buckets[i % count]; }
            }
            return null;
        }

        /// <summary>
        /// 快捷获取格子
        /// </summary>
        /// <param name="serial">格子编号</param>
        /// <returns></returns>
        public Bucket GetBucketBySerial(int serial)
        {
            return Buckets[serial];
        }

        /// <summary>
        /// 获取符合条件格子集合
        /// </summary>
        /// <param name="bucketMask">boolMask列表</param>
        /// <returns></returns>
        public List<Bucket> GetBucketSet(List<bool> bucketMask)
        {
            List<Bucket> getBuckets = new List<Bucket>();

            foreach (KeyValuePair<int, Bucket> kvp in Buckets)
            {
                if (bucketMask[kvp.Key])
                {
                    getBuckets.Add(kvp.Value);
                }
            }

            return getBuckets;
        }

        public List<Bucket> GetAllTargets(TargetType targetType,BattleSite selfSite)
        {
            return GetBucketBySiteSet(GetSiteSetByTargetInfo(targetType, selfSite));
        }

        private HashSet<BattleSite> GetSiteSetByTargetInfo(TargetType targetType, BattleSite selfSite)
        {
            HashSet<BattleSite> siteSet = new HashSet<BattleSite>();
            
            if (selfSite == BattleSite.Boss)
            {
                switch (targetType)
                {
                    case TargetType.NotSelf:
                        siteSet.Add(BattleSite.P1);
                        siteSet.Add(BattleSite.P2);
                        break;
                    case TargetType.Self:
                        siteSet.Add(BattleSite.Boss);
                        break;
                }
                return siteSet;
            }

            switch (targetType)
            {
                case TargetType.Boss:
                    siteSet.Add(BattleSite.Boss);
                    break;
                case TargetType.Self:
                    siteSet.Add(selfSite);
                    break;
                case TargetType.NotEnemy:
                    siteSet.Add(selfSite);
                    siteSet.Add(BattleSite.Boss);
                    break;
                case TargetType.Enemy:
                    siteSet.Add(RevertSite(selfSite));
                    break;
                case TargetType.NotSelf:
                    siteSet.Add(RevertSite(selfSite));
                    siteSet.Add(BattleSite.Boss);
                    break;
                case TargetType.All:
                    siteSet.Add(selfSite);
                    siteSet.Add(RevertSite(selfSite));
                    siteSet.Add(BattleSite.Boss);
                    break;
                case TargetType.None:
                case TargetType.Random:
                case TargetType.RandomNotSelf:
                default:
                    break;
            }

            return siteSet;
        }

        /// <summary>
        /// 获取目标site的格子
        /// </summary>
        /// <param name="siteSet"></param>
        /// <returns></returns>
        private List<Bucket> GetBucketBySiteSet(HashSet<BattleSite> siteSet)
        {
            List<Bucket> buckets = new List<Bucket>();
            foreach (var pair in _bucketSiteFlagD)
            {
                if (siteSet.Contains(pair.Value))
                {
                    buckets.Add(Buckets[pair.Key]);
                }
            }
            return buckets;
        }

        /// <summary>
        /// 获取相邻格子（服务于AOE
        /// </summary>
        /// <param name="bucket">待获取格子</param>
        /// <returns></returns>
        public List<Bucket> GetNeighbors(Bucket bucket)
        {
            List<Bucket> neighbors = new List<Bucket>();

            int index = bucket.serial;
            int correct = 0;
            if (index > 7)
            {
                index -= 7;
                correct = 7;
            }
            switch (index)
            {
                case 1:
                    neighbors.Add(Buckets[correct + 2]);
                    neighbors.Add(Buckets[correct + 3]);
                    neighbors.Add(Buckets[correct + 5]);
                    break;
                case 2:
                    neighbors.Add(Buckets[correct + 1]);
                    neighbors.Add(Buckets[correct + 4]);
                    neighbors.Add(Buckets[correct + 5]);
                    break;
                case 3:
                    neighbors.Add(Buckets[correct + 1]);
                    neighbors.Add(Buckets[correct + 5]);
                    neighbors.Add(Buckets[correct + 6]);
                    break;
                case 4:
                    neighbors.Add(Buckets[correct + 2]);
                    neighbors.Add(Buckets[correct + 5]);
                    neighbors.Add(Buckets[correct + 7]);
                    break;
                case 5:
                    neighbors.Add(Buckets[correct + 1]);
                    neighbors.Add(Buckets[correct + 2]);
                    neighbors.Add(Buckets[correct + 3]);
                    neighbors.Add(Buckets[correct + 4]);
                    neighbors.Add(Buckets[correct + 6]);
                    neighbors.Add(Buckets[correct + 7]);
                    break;
                case 6:
                    neighbors.Add(Buckets[correct + 3]);
                    neighbors.Add(Buckets[correct + 5]);
                    neighbors.Add(Buckets[correct + 7]);
                    break;
                case 7:
                    neighbors.Add(Buckets[correct + 4]);
                    neighbors.Add(Buckets[correct + 5]);
                    neighbors.Add(Buckets[correct + 6]);
                    break;
            }

            return neighbors;
        }


    }
}