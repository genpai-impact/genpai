using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using System.Linq;
using Spine;
using UnityEngine.Serialization;

namespace Genpai
{

    /// <summary>
    /// 单位实体mono，储存部分本地数据
    /// </summary>
    public class UnitEntity : MonoBehaviour
    {
      
        public BattleSite ownerSite;
        public BucketEntity carrier;
        
        public GenpaiPlayer Owner
        {
            get => GameContext.GetPlayerBySite(ownerSite);
        }
        
        public int Serial => carrier.serial;

        /// <summary>
        /// 描述当前单位是否死亡？可否交互？
        /// </summary>
        public bool available;

        public UnitModelDisplay unitModelDisplay;
        public UnitDisplay unitDisplay;

        /// <summary>
        /// 获取Unit数据
        /// </summary>
        /// <returns></returns>
        public Unit GetUnit()
        {
            return available ? BattleFieldManager.Instance.GetBucketBySerial(Serial).unitCarry : null;
        }

        public void Init(BattleSite owner, BucketEntity bucketCarrier)
        {
            ownerSite = owner;
            carrier = bucketCarrier;
            available = true;
            unitModelDisplay = GetComponent<UnitModelDisplay>();
            unitDisplay = GetComponent<UnitDisplay>();
        }

        public void SetFall()
        {
            available = false;
        }
    }
}