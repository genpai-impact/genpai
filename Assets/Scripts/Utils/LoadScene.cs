using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string Lscene;
    
    public void LoadNewScene() {
        SceneManager.LoadScene(Lscene);
    }
}