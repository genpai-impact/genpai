using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Genpai
{


    public class unitInfoCanva :MonoBehaviour, IPointerClickHandler,IPointerEnterHandler
    {
        public UnitInfoDisplay UID;
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
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        // Update is called once per frame
        void Update()
        {

        }
        
    }
}
