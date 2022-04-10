using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsyncScene : MonoBehaviour
{
    AsyncOperation op = null;
    public string targetScene;          //Ŀ�곡��
    public Image loadImage;             //������ͼƬ
    public Image loadingPointer;        //����ָ��ͼƬ
    public Image backGround;            //����ͼƬ
    public float maxFill = 14.62f;      //����������ȱ���

    private void Awake()
    {
        loadImage.rectTransform.position = new Vector3((Screen.width - 1462)/2, 0, 0);          //��ʼ��������λ��
        loadingPointer.rectTransform.position = loadImage.rectTransform.position +              //��ʼ������ָ��λ��
            new Vector3(loadImage.rectTransform.rect.width * loadImage.rectTransform.localScale.x,
            loadImage.rectTransform.rect.height * loadImage.rectTransform.localScale.y,
            0);
    }
    public void Start()
    {
       StartCoroutine(processLoading());
    }
    /// <summary>
    /// ���ؽ���
    /// </summary>
    IEnumerator processLoading()
    {
        op = SceneManager.LoadSceneAsync(targetScene);
        while (!op.isDone)
        {
           loadImage.rectTransform.localScale= new Vector3(maxFill*op.progress,0.1f,0); // ������ȡֵ��Χ0~1
                                                                                        
            loadingPointer.rectTransform.position = loadImage.rectTransform.position +              //���¼���ָ��λ��
           new Vector3(loadImage.rectTransform.rect.width * loadImage.rectTransform.localScale.x,
           loadImage.rectTransform.rect.height * loadImage.rectTransform.localScale.y,
           0);
            yield return null;
        }

        yield return null;
        
    }
}
