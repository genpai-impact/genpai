using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GameSystem.PlotScripts
{
    public class SaveName : MonoBehaviour
    {
        // Start is called before the first frame update

        public InputField InputName;
        public GameObject Name;
        public GameObject Level;
        public GameObject Text;
        
        public bool _FadeOut = false;
        private float start = 1;//此值为开始递减的默认值
        private float max = 1;//修改此值设置递减比例
        public float duration = 3f;//持续时间
        private float timer;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
            if (_FadeOut)
            {
                FadeOut();
            }
        }

        

        public void OnClick()
        {
            if (!string.IsNullOrEmpty(InputName.text))//判断输入框是否为空
            {
                PlayerPrefs.SetString("Name", InputName.text);
                Level.GetComponent<JudgeMouse>().enabled = true;
                Text.SetActive(false);
                Name.SetActive(false);
            }
            else
            {
                start = 1;//初始值
                Text.SetActive(true);
                _FadeOut = true;
            }
        }
        private void FadeOut()
        {
            if (start > 0)
            {
                start -= max * Time.deltaTime / duration;
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    Debug.LogError(timer);
                    timer = 0;
                }
            }
            else
            {
                start = 0;
                
            }
            Text.GetComponent<CanvasGroup>().alpha = start;
        }
    }
}

            //cs.text = PlayerPrefs.GetString("Name");
            //AddControl.transform.localPosition = new Vector3(0, 0, 0);

            //public void DeText()
            //{
            //    Text.SetActive(false);
            //}
            //待完善代码暂时注释
