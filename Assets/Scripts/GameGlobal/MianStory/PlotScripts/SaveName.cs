using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GameSystem.PlotScripts
{
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
        public GameObject Name;
        public GameObject Level;
        public GameObject Text;
        //public GameObject AddControl;
        //public Vector3 localPosition;
        //public Text cs;
        public void OnClick()
        {
            if (!string.IsNullOrEmpty(InputName.text))//ÅÐ¶ÏÊäÈë¿òÊÇ·ñÎª¿Õ
            {
                PlayerPrefs.SetString("Name", InputName.text);
                Level.GetComponent<JudgeMouse>().enabled = true;
                Name.SetActive(false);
            }
            else
            {
                Text.SetActive(true);
                //Invoke(DeText, 1);
            }

            //cs.text = PlayerPrefs.GetString("Name");
            //AddControl.transform.localPosition = new Vector3(0, 0, 0);

            //public void DeText()
            //{
            //    Text.SetActive(false);
            //}
            //´ýÍêÉÆ´úÂëÔÝÊ±×¢ÊÍ
        }
    }
}
