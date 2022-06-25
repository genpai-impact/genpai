using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class LoadScene : MonoBehaviour
    {

        public string Lscene;
    
        public void LoadNewScene() {
            SceneManager.LoadScene(Lscene);
        }
    }
}