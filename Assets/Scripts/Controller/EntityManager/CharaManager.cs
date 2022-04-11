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
        private LinkedList<GameObject> CharaBanners = new LinkedList<GameObject>();

        // �����ɫ�б�
        private LinkedList<Chara> CharaList = new LinkedList<Chara>();

        //��ǰ��ɫ
        private float col = 0.9f;

        //��ǰ������ɫ���
        public GameObject CurrentCharaBanner;


        public CharaManager()
        {
        }

        public void Init(BattleSite site)
        {

            PlayerSite = site;
            if (site == BattleSite.P1)
            {
                CurrentCharaBanner = PrefabsLoader.Instance.charaBannerOnBattle;
            }
            else
            {
                CurrentCharaBanner = PrefabsLoader.Instance.charaBanner2OnBattle;
            }
        }

        public int Count()
        {
            return CharaList.Count;
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

            CharaBanners.AddFirst(newCharaCard);
            CharaList.AddFirst(chara);


            newCharaCard.GetComponent<CharaBannerHead>().Init(chara, PlayerSite);

            //���ʵİ�ɫ��������ȷ
            newCharaCard.GetComponent<Image>().color = new Color(col, col, col, 0.9f);
            col -= 0.12f;

            //�������Ϸ�
            newCharaCard.transform.SetSiblingIndex(newCharaCard.transform.parent.childCount - 1);
            newCharaCard.transform.localScale = Vector3.one;
        }

        public void AddChara(Card drawedCard)
        {
            Chara chara = new Chara(drawedCard as UnitCard, BattleFieldManager.Instance.GetBucketBySerial(GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket.serial));

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
            CharaBanners.Last.Value.GetComponent<CharaBannerHead>().CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);

            CDRefresh();
        }

        public void HideAllBanners()
        {
            foreach (GameObject item in CharaBanners)
            {
                item.GetComponent<CharaBannerHead>().HideBanner();
            }
        }

        public void CharaReturnHand(Chara tempChara)
        {
            foreach (var it in CharaBanners)
            {
                if (it.GetComponent<CharaBannerDisplay>().chara == tempChara)
                    CharaBanners.Remove(it);
            }

            AddChara(tempChara);

        }

        public void Remove(GameObject node)
        {
            CharaBanners.Remove(node);
        }

        public void CDRefresh()
        {
            foreach (GameObject item in CharaBanners)
            {
                item.GetComponent<CharaBannerHead>().CharaBanner.GetComponent<CharaBannerDisplay>().CDDisplay();
            }
        }

        public void RefreshCharaUI()
        {
            CurrentCharaBanner.GetComponent<CharaBannerDisplay>().RefreshUI();
        }

        public void RefreshCharaUI(UnitView unitView)
        {
            CurrentCharaBanner.GetComponent<CharaBannerDisplay>().RefreshUI(unitView);
        }
    }
}