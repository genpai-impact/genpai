using Messager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// ��߽�ɫ������
    /// </summary>
    public class CharaManager
    {
        public BattleSite PlayerSite;

        // ��ɫ��Ƭ�б�
        private LinkedList<CharaBannerHead> CharaBanners = new LinkedList<CharaBannerHead>();

        // �����ɫ�б�
        private LinkedList<Chara> CharaList = new LinkedList<Chara>();

        //��ǰ��ɫ
        private float col = 0.9f;

        //��ǰ������ɫ���
        public CharaBannerDisplay CurrentCharaBanner;


        public CharaManager()
        {
        }

        public void Init(BattleSite site)
        {
            // ����Site��ȡ����Banner
            // Fixme�����ػ����������߼�
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
            // ���ʱ������Ӧ��
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

            //���ʵİ�ɫ��������ȷ
            newCharaCard.GetComponent<Image>().color = new Color(col, col, col, 0.9f);
            col -= 0.12f;

            //�������Ϸ�
            newCharaCard.transform.SetSiblingIndex(newCharaCard.transform.parent.childCount - 1);
            newCharaCard.transform.localScale = Vector3.one;

            CharaBanners.AddFirst(newCharaCard.GetComponent<CharaBannerHead>());
            CharaList.AddFirst(chara);

        }

        public void AddChara(Card drawedCard)
        {
            Chara chara = new Chara(drawedCard as UnitCard,
                BattleFieldManager.Instance.GetBucketBySerial(GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket.serial), false);

            AddChara(chara);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPassive">�Ƿ��Ǳ����������������������</param>
        public void Summon(bool isPassive)
        {
            //TODO��ʹ�ñ�ע�͵��Ĵ���ᱨ���̵Ĵ��Ҳ���⣬������ǲ߻������󣬴��п�����������
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
                str += it.CharaBanner.chara.unitName;
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