using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickSender : MonoBehaviour, IPointerClickHandler{

    private UnityAction<GameObject> _act;

    public void Init(UnityAction<GameObject> act)
    {
        _act = act;
    }

    public void OnPointerClick(PointerEventData eventData){
        _act.Invoke(gameObject);
    }

}
