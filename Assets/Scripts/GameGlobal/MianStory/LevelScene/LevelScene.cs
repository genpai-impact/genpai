using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    //����������
    public Queue<GameObject> _clickedObjs = new Queue<GameObject>();

    //�ṩ������Ҫ��ɵ�ί�У���ͨ��ί�иı�_clickedObj����_clickedObj
    public void ClickDelegate(GameObject gameObject)
    {
        //�˴����������Ͻ�һЩ������ʵ��һ���̣ܳ�shyx 2022/8/1
        _clickedObjs.Enqueue(gameObject);
        Debug.Log(gameObject.name);
    }

    void Start()
    {
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
            GameObject prossingClick = _clickedObjs.Dequeue();
            switch (prossingClick.name)
            {
                case "LevelA":
                    ShowGameSave();
                    break;
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
                    Debug.LogWarning("��������ħ��������û�еĲ�����" + prossingClick.name);
                    break;
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
