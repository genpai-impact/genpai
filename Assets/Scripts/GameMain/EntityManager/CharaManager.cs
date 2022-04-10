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
        private LinkedList<GameObject> CharaCards = new LinkedList<GameObject>();

        // �����ɫ�б�
        private LinkedList<NewChara> CharaList = new LinkedList<NewChara>();

        //��ǰ��ɫ
        private float col = 0.9f;

        //��ǰ������ɫ���
        public GameObject CharaOnBattle;


        public CharaManager()
        {
        }

        public void Init(BattleSite site)
        {

            PlayerSite = site;
            if (site == BattleSite.P1)
            {
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

        private void AddChara(NewChara chara)
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

            CharaCards.AddFirst(newCharaCard);


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
            NewChara chara = new NewChara(drawedCard as UnitCard, NewBattleFieldManager.Instance.GetBucketBySerial(GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket.serial));

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
            }
            */
            CharaCards.Last.Value.GetComponent<CharaBannerHead>().CharaBanner.GetComponent<CharaBannerDisplay>().SummonChara(isPassive);

            CDRefresh();
        }

        public void HideAllBanners()
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaBannerHead>().HideBanner();
            }
        }

        public void CharaToCard(NewChara tempChara)
        {
            foreach (var it in CharaList)
            {
                if (it == tempChara)
                    CharaList.Remove(it);
            }

            AddChara(tempChara);

        }

        public void Remove(GameObject node)
        {
            CharaCards.Remove(node);
        }

        public void CDRefresh()
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaBannerHead>().CharaBanner.GetComponent<CharaBannerDisplay>().CDDisplay();
            }
        }

        public void RefreshCharaUI()
        {
            CharaOnBattle.GetComponent<CharaBannerDisplay>().RefreshUI();
        }

        public void RefreshCharaUI(int CurHP, int CurATK, int CurEng)
        {
            CharaOnBattle.GetComponent<CharaBannerDisplay>().RefreshUI(CurHP, CurATK, CurEng);
        }
    }
}