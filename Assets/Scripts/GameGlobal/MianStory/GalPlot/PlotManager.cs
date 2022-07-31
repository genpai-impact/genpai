using UnityEngine;

namespace GameSystem.GalPlot
{
    public class PlotManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public RectTransform leftImg;
        public RectTransform RightImg;
        private RectTransform nameTran;
        public float emp = 3f;
        float lastWid;
        void Start()
        {
            nameTran = this.GetComponent<RectTransform>();
            lastWid = nameTran.rect.width;
        }

        // Update is called once per frame
        void Update()
        {
            if(nameTran.rect.width!=lastWid)
            {
                leftImg.anchoredPosition = new Vector2(-nameTran.rect.width  -emp, 0);
                RightImg.anchoredPosition = new Vector2(nameTran.rect.width  + emp, 0);
            }
        
            // Debug.Log(leftImg.anchoredPosition);
        }
    
    }
}
