using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{


    public class CardAreaInit : MonoBehaviour
    {
        // private GameObject child;//牌组单位卡牌
        private RectTransform rect;
        private GridLayoutGroup grid;
        private float VerticalSpace;
        private float TopAndBottomSpace;
        private float Height;
        private
        // Start is called before the first frame update
        void Start()
        {
            // child = this.transform.GetChild(0).gameObject;
            rect = this.transform.GetComponent<RectTransform>();
            grid = this.transform.GetComponent<GridLayoutGroup>();
            Height = grid.cellSize.y;
            VerticalSpace = grid.spacing.y;
            TopAndBottomSpace = grid.padding.top;
            if (this.CompareTag("CardTag"))
            {
                SetGrid.SetGridWeight(1, this.transform.childCount, grid, rect);
            }
            else
            {
                SetGrid.SetGridHeight(5, this.transform.childCount, grid, rect);
            }
        }
       
    }
}
