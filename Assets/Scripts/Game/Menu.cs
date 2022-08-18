using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private float menuAnimationTime;
    [SerializeField] private GameEventChannel gameEventChannel;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private Slider audioVolumenSlider;
    [SerializeField] private AudioClip openMenuAudioClip;
    [SerializeField] private AudioClip closeMenuAudioClip;
    [SerializeField] private Canvas canvas;

    private float _closeYPosition = -418f;
    private float _openYPosition = 0f;
    private bool _isAnimating = false;
    private bool _isOpen = false;
    private AudioSource _audioSource;

    public void CloseMenuButton_clicked()
    {
        if (_isAnimating) return;
        _audioSource.clip = closeMenuAudioClip;
        _audioSource.Play();
        StartCoroutine(SwitchMenuDisplayMode());
    }

    public void OnMenuClickHandler()
    {
        if (_isAnimating || _isOpen) return;
        _audioSource.clip = openMenuAudioClip;
        _audioSource.Play();
        StartCoroutine(SwitchMenuDisplayMode());
    }

    private IEnumerator SwitchMenuDisplayMode()
    {
        _isAnimating = true;

        float time = 0;
        var startPosition = transform.position.y;
        var targetPosition = _isOpen ? GetScreenScaledCloseMenuYposition() : _openYPosition;
        gameEventChannel.GamePause(!_isOpen);
        while (_isAnimating)
        {
            time += Time.deltaTime / menuAnimationTime;
            var yMovement = Mathf.Lerp(startPosition, targetPosition, time);
            transform.position = new Vector3(transform.position.x, yMovement, transform.position.z);
            _isAnimating = !Mathf.Approximately(transform.position.y, targetPosition);
            yield return null;
        }
        _isOpen = !_isOpen;
        closeMenuButton.gameObject.SetActive(_isOpen);
    }


    private float GetScreenScaledCloseMenuYposition()
    {
        return _closeYPosition * canvas.transform.localScale.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        closeMenuButton.gameObject.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
        audioVolumenSlider.value = AudioListener.volume;
    }

    public void OnAudioVolumenChange()
    {
        AudioListener.volume = audioVolumenSlider.value;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
