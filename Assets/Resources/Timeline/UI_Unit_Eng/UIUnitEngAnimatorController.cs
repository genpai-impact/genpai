using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUnitEngAnimatorController : MonoBehaviour
{
    public Material UIUnitMaterial;

    public Animator BreathingLightAnimator;

    public float Width;
    
    private int propertyID;
    
    void Start()
    {
        UIUnitMaterial = this.GetComponent<Image>().material;

        BreathingLightAnimator = this.GetComponent<Animator>();

        propertyID = Shader.PropertyToID("_Width");
    }

    void Update()
    {
        if(UIUnitMaterial == null) return;

        if(BreathingLightAnimator.GetInteger("eng")>=BreathingLightAnimator.GetInteger("expectEng"))
            UIUnitMaterial.SetFloat(propertyID, Width);
    }
}
