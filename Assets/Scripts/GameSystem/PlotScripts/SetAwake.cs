using UnityEngine;

namespace GameSystem.PlotScripts
{
    public class SetAwake : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
       
        }

        // Update is called once per frame
        public GameObject Awake;
        public GameObject Level;
        public GameObject AddControl;
        public int Condition_x;
        public Vector3 localPosition;
        public bool JudgeHidScript;
        //True为禁用鼠标交互
        public void AwakeObject()
        {
            Awake.SetActive(true);
        }
        public void HidObject()
        {
            Awake.SetActive(false);
        }
        public void AwakeScript()
        {
            Level.GetComponent<JudgeMouse>().enabled = true;
        }
        public void HidScript()
        {
            Level.GetComponent<JudgeMouse>().enabled = false;
        }
        void Update()
        {
            if(AddControl.transform.localPosition.x == Condition_x)
            {
                AwakeObject();
                //HidScript();
            }
            else
            {
                HidObject();
                //AwakeScript();
            }
            if (AddControl.transform.localPosition.x == Condition_x && JudgeHidScript)
            {
                HidScript();
            }
        }
    }
}
