using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Genpai
{
    public class UnitInfoCanva :MonoBehaviour, IPointerClickHandler,IPointerEnterHandler
    {
        public UnitInfoDisplay UID;
        public RectTransform PasSkill;
        Vector2 PasOriginPos;
        private void Start()
        {
            if (PasSkill != null) PasOriginPos = PasSkill.anchoredPosition;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           // Debug.Log(eventData.pointerCurrentRaycast.gameObject.tag);
            if(!eventData.pointerCurrentRaycast.gameObject.CompareTag("unitInfo"))
            {
                if(UID.STATE==UnitInfoDisplay.state.show)
                {
                    Debug.Log("hhh");
                    UID.EmptyArea.SetActive(false);
                    UID.isShow = false;
                    UID.isHide = true;
                    UID.curPos = UID.transform.localPosition;
                    UID.curAlpha=UID.transform.GetComponent<CanvasGroup>().alpha;
                   
                    UID.slideTime = 0;
                    UID.STATE = UnitInfoDisplay.state.hide;

                }
              if(PasSkill!=null) PasSkill.anchoredPosition = PasOriginPos;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            string tag = eventData.pointerCurrentRaycast.gameObject.tag;
           // if (tag=="BattleCard"||tag=="SpellCard")
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        }
    }
}
