using System.Collections.Generic;
using UnityEngine;


namespace Genpai
{
    /// <summary>
    /// ��߽�ɫ������
    /// </summary>
    public class HandCharaManager
    {
        public List<GameObject> handCharas = new List<GameObject>();
        public GameObject Instantiate(Card DrawedCard, BattleSite site)
        {

            Chara chara = new Chara(DrawedCard as UnitCard, 4);

            // ��ӽ�ɫ
            GameContext.Instance.GetPlayerBySite(site).CharaList.Add(chara);

            // ���ɶ�Ӧ������������
            // TODO������Prefabs�������
            GameObject newCard;
            if (site == BattleSite.P1)
            {
                newCard = GameObject.Instantiate(PrefabsLoader.Instance.charaPrefab, PrefabsLoader.Instance.charaPool.transform);
            }
            else
            {
                newCard = GameObject.Instantiate(PrefabsLoader.Instance.charaPrefab, PrefabsLoader.Instance.chara2Pool.transform);
            }

            //������ʾ��ʼ��
            newCard.GetComponent<CharaDisplay>().PlayerSite = site;
            newCard.GetComponent<CharaDisplay>().chara = chara;

            //newCard.GetComponent<CardPlayerController>().player = GameContext.Player1;
            // newCard.transform.position = processtest.Instance.charaPool.transform.position;

            if (site == BattleSite.P1)
            {
                newCard.transform.position = PrefabsLoader.Instance.charaPool.transform.position;
            }
            else
            {
                newCard.transform.position = PrefabsLoader.Instance.chara2Pool.transform.position;
            }

            newCard.transform.localScale = new Vector3(1, 1, 1);
            handCharas.Add(newCard);

            return newCard;
        }

        public void PrintList()
        {
            for(int i = 0; i < handCharas.Count; i++)
            {
                Debug.Log("666666666" + handCharas[i].name);
            }
        }
    }
}