using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoSingleton<LoadScene>
{

        public string Lscene;
    
        public void LoadNewScene() {
            SceneManager.LoadScene(Lscene);
        }
    }
