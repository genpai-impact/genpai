using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadAsyncScene : MonoBehaviour
{
    AsyncOperation op = null;
    public string targetScene;          //Ŀ�곡��
    public Image loadImage;             //������ͼƬ
    public Image loadingPointer;        //����ָ��ͼƬ
    public Image backGround;            //����ͼƬ
    public float maxFill = 14.62f;      //����������ȱ���
    private string SentencePath = "Data\\LoadingSentence";
    public TextMeshProUGUI PassingText;

    private void Awake()
    {
        loadImage.rectTransform.position = new Vector3((Screen.width - 1738) / 2, (Screen.height / 2 - 467), 0);          //��ʼ��������λ��
        loadingPointer.rectTransform.position = loadImage.rectTransform.position +              //��ʼ������ָ��λ��
            new Vector3(loadImage.rectTransform.rect.width * loadImage.rectTransform.localScale.x,
            loadImage.rectTransform.rect.height * loadImage.rectTransform.localScale.y,
            0);
        PassingSentenceInit.Instance.ReadSentence(SentencePath);
    }
    public void Start()
    {
        PassingText.text = PassingSentenceInit.Instance.SentenceList[Random.Range(0, PassingSentenceInit.Instance.SentenceList.Count - 1)];
       // StartCoroutine(processLoading());
    }
    /// <summary>
    /// ���ؽ���
    /// </summary>
    IEnumerator processLoading()
    {
        op = SceneManager.LoadSceneAsync(targetScene);
        while (!op.isDone)
        {
            //  Debug.Log("press" + op.progress);
            loadImage.rectTransform.localScale = new Vector3(maxFill * op.progress, 0.1f, 0); // ������ȡֵ��Χ0~1

            loadingPointer.rectTransform.position = loadImage.rectTransform.position +              //���¼���ָ��λ��
           new Vector3(loadImage.rectTransform.rect.width * loadImage.rectTransform.localScale.x,
           loadImage.rectTransform.rect.height * loadImage.rectTransform.localScale.y,
           0);
            yield return null;
        }

        yield return null;

    }
}
