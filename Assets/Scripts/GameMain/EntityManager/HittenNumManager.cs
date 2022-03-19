using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 伤害数字管理
    /// </summary>
    public class HittenNumManager : Singleton<HittenNumManager>
    {

        private GameObject HittenNum;

        private HittenNumManager()
        {
            HittenNum = Resources.Load("Prefabs/伤害数字/HittenNum") as GameObject;
        }

        public void Init()
        {

        }

        public void PlayDamage(Damage damage)
        {
            GameObject obj = GameObject.Instantiate(HittenNum);
            obj.GetComponent<HittenNumController>().Play(damage);
        }


    }
}