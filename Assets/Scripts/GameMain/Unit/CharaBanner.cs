using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 角色侧栏交互
    /// 实现角色出场
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d54e0edd5a93003fc68f40
    /// </summary>
    public class CharaBanner : MonoBehaviour, IPointerDownHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            // 点击上场，储存原角色数据，更新地块链接
        }


    }
}