using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.PlotScripts
{
   public class HidAwake : MonoBehaviour
  {
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public GameObject Hid;
    void Awake()
    {
       
    }
    void Update()
    {
            Hid.GetComponent<SetAwake>().enabled = false;
        }
  }
}