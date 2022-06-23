using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
public static class SetGrid 
{
        public static void SetGridHeight(int num, int childCount,GridLayoutGroup grid,RectTransform rect)
        {
            int k = (int)(childCount + num - 1) / num;
            float height = k * grid.cellSize.y;//行数乘以Cell的高度
            height += (k - 1) * grid.spacing.y;//每行之间有间隔
            height += grid.padding.top + grid.padding.bottom;//上下间隔
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }
       public static void SetGridWeight(int num, int childCount, GridLayoutGroup grid, RectTransform rect)
        {
            int k = (int)(childCount + num - 1) / num;
            float weight = k * grid.cellSize.x;//行数乘以Cell的宽度
            weight += (k - 1) * grid.spacing.x;//每行之间有间隔
            weight += grid.padding.left + grid.padding.right;//左右间隔
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, weight);
        }

        public static void SetHorizontalLayout(int num, int childCount, HorizontalLayoutGroup grid, RectTransform rect)
        {
        }
    }
}

