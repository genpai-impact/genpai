using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Genpai
{
    public class unitInfoCanva :MonoBehaviour, IPointerClickHandler,IPointerEnterHandler
    {
        public UnitInfoDisplay UID;
        public GameObject TagBtn;
        public RectTransform curState;
        public RectTransform ProSkill;
        public RectTransform PasSkill;
        Vector2 PasOriginPos;
        private void Start()
        {
            PasOriginPos = PasSkill.anchoredPosition;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(!eventData.pointerCurrentRaycast.gameObject.CompareTag("unitInfo"))
            {
                if(UID.STATE==UnitInfoDisplay.state.show)
                {
                    UID.EmptyArea.SetActive(false);
                    UID.isShow = false;
                    UID.isHide = true;
                    UID.curPos = UID.transform.localPosition;
                    UID.curAlpha=UID.transform.GetComponent<CanvasGroup>().alpha;
                   
                    UID.slideTime = 0;
                    UID.STATE = UnitInfoDisplay.state.hide;
                }
                PasSkill.anchoredPosition = PasOriginPos;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            string tag = eventData.pointerCurrentRaycast.gameObject.tag;
            if (tag=="BattleCard"||tag=="SpellCard")
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        }
    }
}
