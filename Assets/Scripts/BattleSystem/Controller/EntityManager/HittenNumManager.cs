using BattleSystem.Service.Common;
using BattleSystem.Service.Unit;
using BattleSystem.Service.Effect;
using BattleSystem.Controller.EntityController;
using UnityEngine;
using Utils;

namespace BattleSystem.Controller.EntityManager
{
    /// <summary>
    /// 伤害数字管理
    /// </summary>
    public class HittenNumManager : Singleton<HittenNumManager>
    {

        private readonly GameObject _hittenNum;

        private HittenNumManager()
        {
            _hittenNum = Resources.Load("Prefabs/伤害数字/HittenNum") as GameObject;
        }

        public void Init()
        {

        }

        // 只是为了在GameContextScript中进行新游戏的fresh的时候保持形式同一，没有特殊作用
        public void Fresh()
        {

        }

        public void PlayDamage(Damage damage)
        {
            GameObject obj = Object.Instantiate(_hittenNum);
            obj.GetComponent<HittenNumController>().Play(damage);
            
            // 若对象为Boss，则还需要维护展示用的血条
            if(damage.Target.UnitType == CardType.Boss)
            {
                GameContext.TheBoss.DecreaseHpDisplay(damage.DamageStructure.DamageValue);
            }
        }


    }
}