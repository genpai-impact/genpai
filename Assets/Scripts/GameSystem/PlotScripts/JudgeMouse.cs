using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCenter = GalForUnity.System.Event.EventCenter;
using UnityEngine.EventSystems;


public class JudgeMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
        {
            //��ȡ��ǰ���ʱѡ�������
            GameObject btn = EventSystem.current.currentSelectedGameObject;
            if (btn == null)
            {
                Debug.Log("����Ϸ�ڲ���");
                EventCenter.GetInstance().OnMouseDown.Invoke(Input.mousePosition);             
            }
            else
            {
                Debug.Log("��UI����");
            }
        }
    }
}

