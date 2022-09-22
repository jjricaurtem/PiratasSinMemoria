using UnityEngine;

namespace Game
{
    public class MobileBrowserCheck : MonoBehaviour
    {
        [SerializeField] private GameObject gameObjectToHide;

        private void Start()
        {
#if !UNITY_ANDROID
        gameObjectToHide.SetActive(Application.isMobilePlatform);
#endif
        }
    }
}
