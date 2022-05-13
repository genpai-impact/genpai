using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Genpai
{
    public class UnitInfoCanva :MonoSingleton<UnitInfoCanva>, IPointerClickHandler
    {
        public RectTransform PasSkill;
       public Vector2 PasOriginPos;
        private void Start()
        {
            if (PasSkill != null) PasOriginPos = PasSkill.anchoredPosition;
        }
       
        public void OnPointerClick(PointerEventData eventData)
        {
            if(!eventData.pointerCurrentRaycast.gameObject.CompareTag("unitInfo"))
            {
                if(UnitInfoDisplay.Instance.STATE==UnitInfoDisplay.state.show)
                {
                    UnitInfoDisplay.Instance.EmptyArea.SetActive(false);
                    UnitInfoDisplay.Instance.isShow = false;
                    UnitInfoDisplay.Instance.isHide = true;
                    UnitInfoDisplay.Instance.curPos = UnitInfoDisplay.Instance.transform.localPosition;
                    UnitInfoDisplay.Instance.curAlpha= UnitInfoDisplay.Instance.transform.GetComponent<CanvasGroup>().alpha;

                    UnitInfoDisplay.Instance.slideTime = 0;
                    UnitInfoDisplay.Instance.STATE = UnitInfoDisplay.state.hide;

                }
                if (PasSkill != null)
                {
                    Invoke("SetOriginPos", 0.5f);
                    UnitInfoDisplay.Instance.moveFlag = false;
                }
            }
        }
        void SetOriginPos()
        {
            PasSkill.anchoredPosition = PasOriginPos;
        }

    }
}
