using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    //点击组件队列
    public Queue<GameObject> _clickedObjs = new Queue<GameObject>();

    //提供点击组件要完成的委托，即通过委托改变_clickedObj队列_clickedObj
    public void ClickDelegate(GameObject gameObject)
    {
        //此处加锁更加严谨一些，可以实现一个管程？shyx 2022/8/1
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
                    Debug.LogWarning("你在淦神魔主函数里没有的操作：" + prossingClick.name);
                    break;
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
