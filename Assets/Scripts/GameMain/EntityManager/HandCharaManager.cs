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
        //角色名片
        private LinkedList<GameObject> CharaCards = new LinkedList<GameObject>();
        //当前颜色
        private float col = 0.9f;
        //当前出场角色面板
        public GameObject CharaOnBattle;

        public BattleSite PlayerSite;

        public HandCharaManager()
        {
        }

        public void Init(BattleSite site)
        {
            PlayerSite = site;
            if (site == BattleSite.P1) {
                CharaOnBattle = PrefabsLoader.Instance.charaBannerOnBattle; 
            }
            else
            {
                CharaOnBattle = PrefabsLoader.Instance.charaBanner2OnBattle;
            }
        }

        public int Count()
        {
            return CharaCards.Count;
        }

        private void AddChara(Chara chara, BattleSite site)
        {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPassive">是否是被动出场（即死亡后出场）</param>
        public void Summon(bool isPassive)
        {
            //TODO：使用被注释掉的代码会报流程的错，我不理解，但这才是策划的需求，大佬看到改下试试

            /*bool Selected = false;
            if (isPassive)
            {
                foreach(GameObject it in CharaCards)
                {
                    CharaCardDisplay t = it.GetComponent<CharaCardDisplay>();
                    if (!t.isFold)
                    {
                        t.CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);
                        Selected = true;
                        break;
                    }
                }
                if (!Selected)
                {
                    CharaCards.Last.Value.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);
                }
            }*/
            CharaCards.Last.Value.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);

            CDRefresh();
        }

        public void HideAllBanners()
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaCardDisplay>().HideBanner();
            }
        }

        public void CharaToCard(Chara tempChara, BattleSite site)
        {
            foreach(GameObject it in CharaCards)
            {
                if(it.GetComponent<CharaCardDisplay>().chara == tempChara)
                CharaCards.Remove(it);
            }

            AddChara(tempChara, site);

        }

        public void Remove(GameObject node)
        {
            CharaCards.Remove(node);
        }

        public void CDRefresh()
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().CDDisplay();
            }
        }

        public void RefreshCharaUI(UnitEntity CurState)
        {
            CharaOnBattle.GetComponent<CharaBannerDisplay>().RefreshUI(CurState);
        }
        
        public void RefreshCharaUI(int CurHP, int CurATK, int CurEng)
        {
            CharaOnBattle.GetComponent<CharaBannerDisplay>().RefreshUI(CurHP,CurATK,CurEng);
        }
    }
}