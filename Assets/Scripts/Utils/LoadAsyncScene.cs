using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadAsyncScene : MonoBehaviour
{
    AsyncOperation op = null;
    public string targetScene;          //目标场景
    public Image loadImage;             //进度条图片
    public Image loadingPointer;        //加载指针图片
    public Image backGround;            //背景图片
    public float maxFill = 17.38f;      //进度条最大宽度比例
    private string SentencePath = "Data\\LoadingSentence";
    public TextMeshProUGUI PassingText;

    private void Awake()
    {
        loadImage.rectTransform.position = new Vector3((Screen.width - 1738) / 2, (Screen.height / 2 - 467), 0);          //初始化进度条位置
        loadingPointer.rectTransform.position = loadImage.rectTransform.position;// +              //初始化加载指针位置
            //new Vector3(loadImage.rectTransform.rect.width * loadImage.rectTransform.localScale.x,
            //loadImage.rectTransform.rect.height * loadImage.rectTransform.localScale.y,
            //0);、、
        PassingSentenceInit.Instance.ReadSentence(SentencePath);
    }
    public void Start()
    {
        PassingText.text = PassingSentenceInit.Instance.SentenceList[Random.Range(0, PassingSentenceInit.Instance.SentenceList.Count - 1)];
        StartCoroutine(processLoading());
    }
    /// <summary>
    /// 加载进度
    /// </summary>
    IEnumerator processLoading()
    {
        op = SceneManager.LoadSceneAsync(targetScene);
        while (!op.isDone)
        {
             Debug.Log("press" + op.progress);
           loadingPointer.rectTransform.localScale = new Vector3(maxFill * op.progress, 0.1f, 0); // 进度条取值范围0~1

           // loadingPointer.rectTransform.position = loadImage.rectTransform.position +              //更新加载指针位置
           //new Vector3(loadImage.rectTransform.rect.width * loadImage.rectTransform.localScale.x,
           //loadImage.rectTransform.rect.height * loadImage.rectTransform.localScale.y,
           //0);
            yield return null;
        }

        yield return null;

    }
}
