using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            SetGridWeight(1, this.transform.childCount);
        }
         else  SetGridHeight(5, this.transform.childCount);

        
    }
    private void SetGridHeight(int num,int childCount)
    {
        int k = (int)(childCount + num - 1) / num;
        float height = k * grid.cellSize.y;//行数乘以Cell的高度
        height += (k - 1) * grid.spacing.y;//每行之间有间隔
        height += grid.padding.top + grid.padding.bottom;//上下间隔
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
    private void SetGridWeight(int num,int childCount)
    {
        int k = (int)(childCount + num - 1) / num;
        float weight = k * grid.cellSize.x;//行数乘以Cell的宽度
        weight += (k - 1) * grid.spacing.x;//每行之间有间隔
        weight += grid.padding.left + grid.padding.right;//左右间隔
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, weight);
    }
}
