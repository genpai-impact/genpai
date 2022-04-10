using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏关卡选择的及其通关记录保存
/// </summary>
public class LevelRecordManager: MonoBehaviour
{
    //SerializeField有序列化的意思，使其在inspector中序列化可见private变量
    //默认为false
    [SerializeField] private bool unlocked;
    public Image unlockImage;
    //储存星星代表等级
    public GameObject[] stars;

    private int currentstarSNum = 0;

    public int levelIndex;

    public Sprite starSprite;

    private void Start()
    {
        //清除记录，只需要运行一次，用完就注释掉，谨慎使用
        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        //没必要放在这，不需要每帧都调用，但目前没好的地方可以放，暂且放着，看看哪位大佬可以修一下
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        //查看上一关是否已通关
        int previousLevelNum = int.Parse(gameObject.name) - 1;

        if (PlayerPrefs.GetInt("Lv" + previousLevelNum) > 0)
        {
            unlocked = true; 
        }
    }

    private void UpdateLevelImage()
    {

        //如果未通过前关卡则为锁住状态
        if (!unlocked)
        {
            //用来激活锁住的UI动画（如果有的话），如果是隐藏关卡则需要修改
            unlockImage.gameObject.SetActive(true);

            for(int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        //关卡解开状态
        else
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
            {
                //可以将星级图片赋值给stars数组的第i个元素
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }

    /// <summary>
    /// 保存关卡最高记录
    /// </summary>
    /// <param name="_starsNum"></param>
    public void PressStar(int _starsNum)
    {
        currentstarSNum = _starsNum;
        //由数字作为关卡名
        if (currentstarSNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _starsNum);
        }

        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, _starsNum));
    }


    /// <summary>
    /// 按下关卡按钮时调用，进行场景转换，输入关卡场景名字
    /// </summary>
    /// <param name="_LevelName"></param>
    public void PressSelection(string _LevelName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(_LevelName);
        }
    }
}
