using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlotButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button button;
    private Text text;
    private Color oriColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color col;
        ColorUtility.TryParseHtmlString("#fef7b3", out col);
        text.color = col;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = oriColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        text = button.gameObject.transform.GetChild(0).GetComponent<Text>();
        oriColor = text.color;
    }

    public void EnterBattle()
    {
        SceneManager.LoadScene("PassingScene");
    }
    public void TrueEnterBattle()
    {
        SceneManager.LoadScene(3);
    }
}
