using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using System.Linq;
using Spine;

namespace Genpai
{

    /// <summary>
    /// 单位实体mono，储存部分本地数据
    /// </summary>
    public class UnitEntity : MonoBehaviour
    {
      
        public BattleSite ownerSite;
        public GenpaiPlayer owner
        {
            get => GameContext.Instance.GetPlayerBySite(ownerSite);
        }

        public BucketEntity carrier;

        public int serial { get => carrier.serial; }

        /// <summary>
        /// 描述当前单位是否死亡？可否交互？
        /// </summary>
        public bool available;

        public UnitModelDisplay UnitModelDisplay;
        public UnitDisplay UnitDisplay;

        /// <summary>
        /// 获取Unit数据
        /// </summary>
        /// <returns></returns>
        public Unit GetUnit()
        {
            return available ? BattleFieldManager.Instance.GetBucketBySerial(serial).unitCarry : null;
        }

        public void Init(BattleSite _owner, BucketEntity _carrier)
        {
            ownerSite = _owner;
            carrier = _carrier;
            available = true;
            UnitModelDisplay = GetComponent<UnitModelDisplay>();
            UnitDisplay = GetComponent<UnitDisplay>();
        }

        public void SetFall()
        {
            available = false;

        }
    }
}