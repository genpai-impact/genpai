using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public InputField InputName;
    public GameObject AddControl;
    public Vector3 localPosition;
    public Text cs;
    public void OnClick()
    {
        PlayerPrefs.SetString("Name", InputName.text);
        cs.text = PlayerPrefs.GetString("Name");
        AddControl.transform.localPosition = new Vector3(0, 0, 0);
    }
}
