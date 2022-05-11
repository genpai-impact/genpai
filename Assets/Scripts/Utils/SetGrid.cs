using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Genpai
{
public static class SetGrid 
{
        public static void SetGridHeight(int num, int childCount,GridLayoutGroup grid,RectTransform rect)
        {
            int k = (int)(childCount + num - 1) / num;
            float height = k * grid.cellSize.y;//��������Cell�ĸ߶�
            height += (k - 1) * grid.spacing.y;//ÿ��֮���м��
            height += grid.padding.top + grid.padding.bottom;//���¼��
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }
       public static void SetGridWeight(int num, int childCount, GridLayoutGroup grid, RectTransform rect)
        {
            int k = (int)(childCount + num - 1) / num;
            float weight = k * grid.cellSize.x;//��������Cell�Ŀ��
            weight += (k - 1) * grid.spacing.x;//ÿ��֮���м��
            weight += grid.padding.left + grid.padding.right;//���Ҽ��
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, weight);
        }

        public static void SetHorizontalLayout(int num, int childCount, HorizontalLayoutGroup grid, RectTransform rect)
        {
            //int k = (int)(childCount + num - 1) / num;
            //float weight = k * grid.chi//��������Cell�Ŀ��
            //weight += (k - 1) * grid.spacing.x;//ÿ��֮���м��
            //weight += grid.padding.left + grid.padding.right;//���Ҽ��
        }
    }
}

