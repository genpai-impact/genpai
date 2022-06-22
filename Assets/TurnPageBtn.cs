using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
public class TurnPageBtn : MonoBehaviour
{
    public Button left;
    public Button right;
    public Button up;
    public Button down;
        private int leftIdxL = 0;
        private int leftIdxR = 8;//左边一页最多显示9张卡
        private int RightIdxL = 0;
        private int RightIdxR = 11;//右边一页最多12张卡
    public GameObject GetCardsListLeft()
    {
        for(int i=0;i<CardGroupManager.Instance.LeftCards.transform.childCount;i++)
            {
                if (CardGroupManager.Instance.LeftCards.transform.GetChild(i).gameObject.activeInHierarchy)
                    return CardGroupManager.Instance.LeftCards.transform.GetChild(i).gameObject; 
            }
            return null;
    }
    private void LeftCardsShown(int l,int r)
    {
            GameObject cards = GetCardsListLeft(); 
            for(int i=0;i<cards.transform.childCount;i++)
            {
                if (i >= l && i <= r) cards.transform.GetChild(i).gameObject.SetActive(true);
                else cards.transform.GetChild(i).gameObject.SetActive(false);
            }
    }
    private void LeftCardsChange()
    {
        LeftCardsShown(leftIdxL, leftIdxR);
    }
        public void btnLeft()
        {
            leftIdxL -= 9;
            leftIdxL = Mathf.Max(leftIdxL, 0);
            leftIdxR = leftIdxL + 8;
            LeftCardsChange();
        }
        public void btnRight()
        {
            GameObject cards = GetCardsListLeft();
            leftIdxL += 9;
            if(leftIdxL>=cards.transform.childCount)
            {
                leftIdxL -= 9;
                return;
            }
            leftIdxR = leftIdxL + 8;
            leftIdxR = Mathf.Min(leftIdxR, cards.transform.childCount - 1);
            LeftCardsChange();
        }

}

}
