using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneController : MonoBehaviour
    {
        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
