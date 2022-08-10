using System.Collections.Generic;
using BattleSystem.Controller.UI;
using BattleSystem.Controller.Unit.UnitView;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using BattleSystem.Service.Unit;
using DataScripts.Card;
using DataScripts.DataLoader;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem.Controller.EntityManager
{
    /// <summary>
    /// 侧边角色管理器
    /// </summary>
    public class CharaManager
    {
        public BattleSite PlayerSite;

        // 角色名片列表
        private LinkedList<CharaBannerHead> CharaBanners = new LinkedList<CharaBannerHead>();

        // 储存角色列表
        private LinkedList<Chara> CharaList = new LinkedList<Chara>();

        //当前颜色
        private float col = 0.9f;

        //当前出场角色面板
        public CharaBannerDisplay CurrentCharaBanner;


        public CharaManager()
        {
        }

        public void Init(BattleSite site)
        {
            // 根据Site获取各自Banner
            // Fixme：严重怀疑是冗余逻辑
            PlayerSite = site;
            if (site == BattleSite.P1)
            {
                CurrentCharaBanner = PrefabsLoader.Instance.charaBannerOnBattle.GetComponent<CharaBannerDisplay>();
            }
            else
            {
                CurrentCharaBanner = PrefabsLoader.Instance.charaBanner2OnBattle.GetComponent<CharaBannerDisplay>();
            }
        }

        public int Count()
        {
            return CharaBanners.Count;
        }

        private void AddChara(Chara chara)
        {
            // 添加时创建对应卡
            GameObject newCharaCard;
            if (PlayerSite == BattleSite.P1)
            {
                newCharaCard = GameObject.Instantiate(PrefabsLoader.Instance.chara_cardPrefab, PrefabsLoader.Instance.charaPool.transform);
            }
            else
            {
                newCharaCard = GameObject.Instantiate(PrefabsLoader.Instance.chara_cardPrefab, PrefabsLoader.Instance.chara2Pool.transform);
            }
            newCharaCard.GetComponent<CharaBannerHead>().Init(chara, PlayerSite);

            //草率的暗色处理（不正确
            newCharaCard.GetComponent<Image>().color = new Color(col, col, col, 0.9f);
            col -= 0.12f;

            //设置最上方
            newCharaCard.transform.SetSiblingIndex(newCharaCard.transform.parent.childCount - 1);
            newCharaCard.transform.localScale = Vector3.one;

            CharaBanners.AddFirst(newCharaCard.GetComponent<CharaBannerHead>());
            CharaList.AddFirst(chara);

        }

        public void AddChara(Card drawedCard)
        {
            Chara chara = new Chara(drawedCard as UnitCard,
                BattleFieldManager.Instance.GetBucketBySerial(GameContext.GetPlayerBySite(PlayerSite).CharaBucket.serial), false);

            AddChara(chara);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPassive">是否是被动出场（即死亡后出场）</param>
        public void Summon(bool isPassive)
        {
            //fixme：使用被注释掉的代码会报流程的错，我不理解，但这才是策划的需求，大佬看到改下试试
            /*
            bool Selected = false;

            if (isPassive)
            {

                foreach (GameObject it in CharaBanners)
                {
                    CharaBannerHead t = it.GetComponent<CharaBannerHead>();
                    if (!t.isFold)
                    {
                        t.CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);
                        Selected = true;
                        break;
                    }
                }
                if (!Selected)
                {
                    CharaBanners.Last.Value.GetComponent<CharaBannerHead>().CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);
                }
            }
            */
            CharaBanners.First.Value.CharaBanner.SummonChara(isPassive);

            CDRefresh();
        }

        public void HideAllBanners()
        {
            foreach (CharaBannerHead item in CharaBanners)
            {
                item.HideBanner();
            }
        }

        public void CharaReturnHand(Chara tempChara)
        {
            foreach (var it in CharaBanners)
            {
                // Debug.Log(it.CharaBanner.chara.unitName);
                if (it.CharaBanner.chara == tempChara)
                    CharaBanners.Remove(it);
            }

            AddChara(tempChara);

        }

        public void PrintRemainsChara()
        {
            string str = "";
            foreach (var it in CharaBanners)
            {
                str += it.CharaBanner.chara.UnitName;
            }
            //Debug.Log("Remains" + str);
        }

        public void Remove(CharaBannerHead node)
        {
            CharaBanners.Remove(node);
            //PrintRemainsChara();
        }

        public void CDRefresh()
        {
            foreach (CharaBannerHead item in CharaBanners)
            {
                item.CharaBanner.CDDisplay();
            }
        }

        public void RefreshCharaUI()
        {
            CurrentCharaBanner.RefreshUI();
        }

        public void RefreshCharaUI(UnitView unitView)
        {
            CurrentCharaBanner.RefreshUI(unitView);
        }
    }
}