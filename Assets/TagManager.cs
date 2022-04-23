using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Genpai
{
    public class TagManager : MonoBehaviour
    {
        public GameObject curStateTag;
        public GameObject ProSkiTag;
        public GameObject PasSkiTag;
        private GameObject unitInfo;
        // Start is called before the first frame update
        void Start()
        {
            unitInfo = transform.parent.gameObject;
            //  curStateTag.GetComponent<Button>().onClick.AddListener()
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
