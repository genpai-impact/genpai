using Messager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// 侧边角色管理器
    /// </summary>
    public class HandCharaManager
    {
        //CharaBanners
        private LinkedList<GameObject> CharaCards = new LinkedList<GameObject>();
        //当前颜色
        private static float col = 0.9f;
        //当前出场角色面板
        public CharaBannerDisplay CharaOnBattle;

        public BattleSite PlayerSite;

        public HandCharaManager()
        {
        }

        public void Init(BattleSite site)
        {
            PlayerSite = site;
        }

        public int Count()
        {
            return CharaCards.Count;
        }

        private void AddChara(Chara chara, BattleSite site)
        {
            // 生成对应角色标签
            GameObject newCharaCard;
            if (site == BattleSite.P1)
            {
                newCharaCard = GameObject.Instantiate(PrefabsLoader.Instance.chara_cardPrefab, PrefabsLoader.Instance.charaPool.transform);
            }
            else
            {
                newCharaCard = GameObject.Instantiate(PrefabsLoader.Instance.chara_cardPrefab, PrefabsLoader.Instance.chara2Pool.transform);
            }
            CharaCards.AddFirst(newCharaCard);

            //角色标签显示初始化
            newCharaCard.GetComponent<CharaCardDisplay>().Init(chara, site);

            //草率的暗色处理（不正确
            newCharaCard.GetComponent<Image>().color = new Color(col, col, col, 0.9f);
            col -= 0.12f;

            //设置最上方
            newCharaCard.transform.SetSiblingIndex(newCharaCard.transform.parent.childCount - 1);

            newCharaCard.transform.localScale = Vector3.one;
        }

        public void AddChara(Card drawedCard, BattleSite site)
        {
            Chara chara = new Chara(drawedCard as UnitCard, 4);

            AddChara(chara, site);
        }

        public void Summon()
        {
            CharaCards.Last.Value.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara();
            CDDisplay();
        }

        public void HideAllBanners()
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaCardDisplay>().HideBanner();
            }
        }

        public void Update(Chara tempChara, BattleSite site)
        {
            //被删除的角色理论上会在最后一位
            CharaCards.Remove(CharaCards.Last);
            //重建场上角色的角色标签和名片实体
            AddChara(tempChara, site);

        }

        public void Remove(GameObject node)
        {
            CharaCards.Remove(node);
        }

        public void CDDisplay()
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().CDDisplay();
            }
        }
    }
}