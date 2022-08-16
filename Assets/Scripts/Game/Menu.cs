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

    private Vector3 _closePosition;
    private Vector3 _openPosition;
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
        var startPosition = _isOpen ? _openPosition : _closePosition;
        var targetPosition = _isOpen ? _closePosition : _openPosition;
        gameEventChannel.GamePause(!_isOpen);
        while (_isAnimating)
        {
            time += Time.deltaTime / menuAnimationTime;
            var yMovement = Mathf.Lerp(startPosition.y, targetPosition.y, time);
            transform.position = new Vector3(transform.position.x, yMovement, transform.position.z);
            _isAnimating = !Mathf.Approximately(transform.position.y, targetPosition.y);
            yield return null;
        }
        _isOpen = !_isOpen;
        closeMenuButton.gameObject.SetActive(_isOpen);
    }

    // Start is called before the first frame update
    void Start()
    {
        closeMenuButton.gameObject.SetActive(false);
        _closePosition = transform.position;

        _openPosition = _closePosition;
        _openPosition.y = 1f;

        _audioSource = GetComponent<AudioSource>();
        audioVolumenSlider.value = AudioListener.volume;
    }

    public void OnAudioVolumenChange()
    {
        AudioListener.volume = audioVolumenSlider.value;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
