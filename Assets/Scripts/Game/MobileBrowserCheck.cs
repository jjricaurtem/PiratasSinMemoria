using UnityEngine;

public class MobileBrowserCheck : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToHide;

    void Start()
    {
        gameObjectToHide.SetActive(Application.isMobilePlatform);
    }
}
