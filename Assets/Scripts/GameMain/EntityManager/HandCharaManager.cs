using Messager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// ��߽�ɫ������
    /// </summary>
    public class HandCharaManager : IMessageReceiveHandler
    {
        //CharaBanners
        private LinkedList<GameObject> CharaCards = new LinkedList<GameObject>();
        //�����ɫ
        //private List<Chara> HandChara = new List<Chara>();
        //��ǰ��ɫ
        private static float col = 0.9f;
        //��ǰ������ɫ���
        public CharaBannerDisplay CharaOnBattle;

        public BattleSite PlayerSite;

        public HandCharaManager()
        {
            Subscribe();
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
            // ���ɶ�Ӧ��ɫ��ǩ
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

            //��ɫ��ǩ��ʾ��ʼ��
            newCharaCard.GetComponent<CharaCardDisplay>().Init(chara, site);

            //���ʵİ�ɫ��������ȷ
            newCharaCard.GetComponent<Image>().color = new Color(col, col, col, 0.9f);
            col -= 0.12f;

            //�������Ϸ�
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
            {//ɾ�����Ͻ�ɫ�Ľ�ɫ��ǩ����Ƭʵ��
                /*foreach (GameObject item in CharaCards)
                {
                    /*if (item.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().chara == tempChara)
                    {
                        //item.GetComponent<CharaCardDisplay>().DeleteBanner(); �Ƿ���Ҫ��
                        CharaCards.Remove(item);
                        break;
                    }

                }*/
            }

            //��ɾ���Ľ�ɫ�����ϻ������һλ
            CharaCards.Remove(CharaCards.Last);

            //�ؽ����Ͻ�ɫ�Ľ�ɫ��ǩ����Ƭʵ��
            AddChara(tempChara, site);
            
        }

        public void Remove(GameObject node)
        {
            CharaCards.Remove(node);
        }

        public void CDDisplay(BattleSite site)
        {
            foreach (GameObject item in CharaCards)
            {
                item.GetComponent<CharaCardDisplay>().CharaBanner.GetComponent<CharaBannerDisplay>().CDDisplay();
            }
        }

        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, CDDisplay);

        }
    }
}