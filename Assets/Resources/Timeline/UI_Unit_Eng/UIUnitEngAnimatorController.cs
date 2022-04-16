using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUnitEngAnimatorController : MonoBehaviour
{
    public Material UIUnitMaterial;

    public float Width;
    
    private int propertyID;
    
    void Start()
    {
        UIUnitMaterial = this.GetComponent<Image>().material;

        propertyID = Shader.PropertyToID("_Width");
    }

    void Update()
    {
        if(UIUnitMaterial == null) return;

        UIUnitMaterial.SetFloat(propertyID, Width);
    }
}
