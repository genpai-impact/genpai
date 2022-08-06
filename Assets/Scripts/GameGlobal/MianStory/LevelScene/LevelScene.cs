using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataScripts;

public class LevelScene : MonoBehaviour
{
    
    public GameObject levelPerfab;
    public GameObject levelContainer;
    private  Queue<GameObject> _clickedObjs = new Queue<GameObject>();//点击组件队列


    //提供点击组件要完成的委托，即通过委托改变_clickedObj队列_clickedObj,这个函数每个场景都要用，可以把所有scene整个父类
    public void ClickDelegate(GameObject gameObject)
    {
        //此处加锁更加严谨一些，可以实现一个管程？shyx 2022/8/1
        if (gameObject.name == "Button")
        {
            _clickedObjs.Enqueue(gameObject.transform.parent.gameObject);
        }
        else { 
            _clickedObjs.Enqueue(gameObject);
        }
        //Debug.Log(gameObject.name);
    }



    //可以实现一个按照数据生成关卡的函数？
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
                    case "卡牌":
                        //SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
                        break;
                    case "图鉴":
                        ShowIllustrated();
                        break;
                    case "退出":
                        ShowBattle();
                        break;
                    case "关卡":
                        ShowExplored();
                        break;
                    case "Quit":
                        ShowQuit();
                        break;
                    default:
                        Debug.LogWarning("你在淦神魔主函数里没有的操作：" + processingClick.name);
                        break;
                }
            }
            
        }
    }

    private void ShowIllustrated()
    {
        Debug.Log("图鉴要干什么捏");
    }

    private void ShowBattle()
    {
        Debug.Log("战斗要干什么捏");
    }

    private void ShowExplored()
    {
        Debug.Log("探索要干什么捏");
    }

    private void ShowSetting()
    {
        Debug.Log("设置要干什么捏");
    }

    private void ShowQuit()
    {
        Debug.Log("真的要退出吗，退出要干什么捏");
    }


    private void ShowGameSave()
    {
        GameObject board = GameObject.Find("画板");
        board.GetComponent<Animator>().SetTrigger("Up");
    }

}
