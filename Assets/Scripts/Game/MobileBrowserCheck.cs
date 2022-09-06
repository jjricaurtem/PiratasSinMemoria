using UnityEngine;

public class MobileBrowserCheck : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToHide;

    void Start()
    {
#if !UNITY_ANDROID
        gameObjectToHide.SetActive(Application.isMobilePlatform);
#endif
    }
}
