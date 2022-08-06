using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataScripts;

public class LevelScene : MonoBehaviour
{
    
    public GameObject levelPerfab;
    public GameObject levelContainer;
    private  Queue<GameObject> _clickedObjs = new Queue<GameObject>();//����������


    //�ṩ������Ҫ��ɵ�ί�У���ͨ��ί�иı�_clickedObj����_clickedObj,�������ÿ��������Ҫ�ã����԰�����scene��������
    public void ClickDelegate(GameObject gameObject)
    {
        //�˴����������Ͻ�һЩ������ʵ��һ���̣ܳ�shyx 2022/8/1
        if (gameObject.name == "Button")
        {
            _clickedObjs.Enqueue(gameObject.transform.parent.gameObject);
        }
        else { 
            _clickedObjs.Enqueue(gameObject);
        }
        //Debug.Log(gameObject.name);
    }



    //����ʵ��һ�������������ɹؿ��ĺ�����
    private void GenerateLevels() {
        foreach (var temp in LubanLoader.GetTables().LevelItems.DataList) {
            GameObject tempobj = Instantiate(levelPerfab, levelContainer.transform);
            tempobj.AddComponent<Level>().Init(temp.Id, temp.LevelName);//.transform.Find("Button").gameObject.
        }
    }


    private void FreshGameSave(){
        //Dictionary<int,int>
    }


    void Start()
    {
        GenerateLevels();
        FreshGameSave();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ClickTrigger");
        foreach (GameObject obj in objs)
        {
            obj.AddComponent<ClickSender>();
            obj.GetComponent<ClickSender>().Init(ClickDelegate);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (_clickedObjs.Count != 0)
        {
            GameObject processingClick = _clickedObjs.Dequeue();
            Debug.Log(processingClick);
            if (processingClick.GetComponent<Level>() != null)
            {
                LevelBehavior.ClickChoice(processingClick.GetComponent<Level>().id, processingClick.GetComponent<Level>().levelOrEvent);
            }
            else {
                switch (processingClick.name)
                {
                    case "����":
                        //SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
                        break;
                    case "ͼ��":
                        ShowIllustrated();
                        break;
                    case "�˳�":
                        ShowBattle();
                        break;
                    case "�ؿ�":
                        ShowExplored();
                        break;
                    case "Quit":
                        ShowQuit();
                        break;
                    default:
                        Debug.LogWarning("��������ħ��������û�еĲ�����" + processingClick.name);
                        break;
                }
            }
            
        }
    }

    private void ShowIllustrated()
    {
        Debug.Log("ͼ��Ҫ��ʲô��");
    }

    private void ShowBattle()
    {
        Debug.Log("ս��Ҫ��ʲô��");
    }

    private void ShowExplored()
    {
        Debug.Log("̽��Ҫ��ʲô��");
    }

    private void ShowSetting()
    {
        Debug.Log("����Ҫ��ʲô��");
    }

    private void ShowQuit()
    {
        Debug.Log("���Ҫ�˳����˳�Ҫ��ʲô��");
    }


    private void ShowGameSave()
    {
        GameObject board = GameObject.Find("����");
        board.GetComponent<Animator>().SetTrigger("Up");
    }

}
