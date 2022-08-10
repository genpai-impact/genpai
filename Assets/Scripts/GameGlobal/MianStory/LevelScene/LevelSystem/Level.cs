using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int id;
    public string name;

    public bool levelOrEvent=true;
    GameObject[] stars= new GameObject[4];
    GameObject button;


    /// <summary>
    /// 改变星级 0<=num<=4 !!!
    /// </summary>
    public void ChangeStars(int num){
        if (num < 0 || num > 4) {
            Debug.LogWarning("星级为0-4");
            return;
        }
        int count = 0;
        foreach (GameObject var in stars) { 
            var.SetActive(false);
        }

        foreach (GameObject var in stars)
        {
            if (count == num)
                break;
            var.SetActive(true);
            count++;
        }
    }

    /// <summary>
    /// 关卡初始化 param1：关卡id，param2：关卡名称
    /// </summary>
    public void Init(int param1,string param2) {
        GameObject star = transform.Find("Star").gameObject;
        Transform[] obj = star.transform.GetComponentsInChildren<Transform>(true);

        for (int i=0;i<4;i++) {
            stars[i] = obj[i+1].gameObject;
            //Debug.Log(stars[i]);
        }
       
        button = transform.Find("Button").gameObject;

        id = param1;
        name= param2;
        gameObject.name = param2;
        button.transform.Find("Text").GetComponent<Text>().text = param2;
    }

    // Start is called before the first frame update
    void Awake(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
