using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetAwake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public GameObject Awake;
    public GameObject AddControl;
    public int Condition_x;
    public Vector3 localPosition;
    public void AwakeObject()
    {
        Awake.SetActive(true);
    }
    public void HidObject()
    {
        Awake.SetActive(false);
    }
    void Update()
    {
        if(AddControl.transform.localPosition.x == Condition_x)
        {
            AwakeObject();
        }
        else
        {
            HidObject();
        }
    }
}
