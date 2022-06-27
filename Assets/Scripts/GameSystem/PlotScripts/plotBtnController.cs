using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;
public class plotBtnController : MonoSingleton<plotBtnController>
{
  //  public Button skip;
    public static bool HasSkip;
    // Start is called before the first frame update
    void Start()
    {
     // if(skip==null)  skip = this.GetComponent<Button>();
        if (HasSkip) SceneManager.LoadScene("BattleScene");
        if (!HasSkip) DontDestroyOnLoad(this.gameObject);
        HasSkip = true;
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
