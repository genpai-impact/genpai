using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharaAniController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 beginPos;
    private Vector3 _Offset;
    private Image image;

    void Start()
    {
        beginPos = gameObject.transform.position;
        image = transform.GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        beginPos = gameObject.transform.position;
        _Offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _Offset;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter != null)
        {
            CharaAniController drag = eventData.pointerEnter.GetComponent<CharaAniController>();
            if (drag.transform != transform)
            {
                Vector3 pos = drag.transform.position;
                drag.transform.position = beginPos;
                transform.position = pos;
                transform.localScale = Vector3.one;
            }
        }
        else
        {
            transform.position = beginPos;
            transform.localScale = Vector3.one;
        }
        image.raycastTarget = true;
    }

}
