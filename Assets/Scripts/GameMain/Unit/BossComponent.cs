using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Boss组件
    /// 追加在UnitEntity上以实现Boss功能
    /// 主要体现为释放技能
    /// </summary>
    public class BossComponent : MonoBehaviour, IMessageHandler
    {

        public Boss unit;

        public int MPMax_1
        {
            get { return unit.MPMax_1; }
        }
        public int MPMax_2
        {
            get { return unit.MPMax_2; }
        }
        public int MP_1
        {
            get => unit.MP_1;
            set { unit.MP_1 = value; }
        }
        public int MP_2
        {
            get => unit.MP_2;
            set { unit.MP_2 = value; }
        }

        public void Awake()
        {
            // 从数据库获取技能等插件

            Subscribe();
        }



        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            // 主要发送技能相关
        }

        public void Init(Boss _unit)
        {
            this.unit = _unit;

        }

        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnBossStart, AddMP);  // 把充一点MP这件事情添加到新回合开始时要做的事情中
        }


        public void AddMP(BattleSite site)
        {
            if (site == GetComponent<UnitEntity>().ownerSite)
            {
                // Debug.Log("Add MP");
                if (0 <= MP_1 && MP_1 < MPMax_1)
                {
                    MP_1++;
                }
                if (0 <= MP_2 && MP_2 < MPMax_2)
                {
                    MP_2++;
                }
            }

        }
    }
}