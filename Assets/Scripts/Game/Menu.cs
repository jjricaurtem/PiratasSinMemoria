using System.Collections;
using Commons.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private float menuAnimationTime;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private Button closeMenuButton;
        [SerializeField] private Slider audioVolumeSlider;
        [SerializeField] private AudioClip openMenuAudioClip;
        [SerializeField] private AudioClip closeMenuAudioClip;
        [SerializeField] private Canvas canvas;
        [SerializeField] private float closeYPosition = -300f;

        private const float OpenYPosition = 0f;
        private bool _isAnimating;
        private bool _isOpen;
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
            var targetPosition = _isOpen ? GetScreenScaledCloseMenuYPosition() : OpenYPosition;
            gameEventChannel.GamePause(!_isOpen);
            while (_isAnimating)
            {
                time += Time.deltaTime / menuAnimationTime;
                var yMovement = Mathf.Lerp(startPosition, targetPosition, time);
                var currentTransform = transform;
                var currentPosition = currentTransform.position;
                currentTransform.position = new Vector3(currentPosition.x, yMovement, currentPosition.z);
                _isAnimating = !Mathf.Approximately(yMovement, targetPosition);
                yield return null;
            }
            _isOpen = !_isOpen;
            closeMenuButton.gameObject.SetActive(_isOpen);
        }


        private float GetScreenScaledCloseMenuYPosition()
        {
            return closeYPosition * canvas.transform.localScale.y;
        }

        // Start is called before the first frame update
        private void Start()
        {
            closeMenuButton.gameObject.SetActive(false);
            // _closeYPosition = transform.position.y;

            _audioSource = GetComponent<AudioSource>();
            audioVolumeSlider.value = AudioListener.volume;
        }

        public void OnAudioVolumeChange()
        {
            AudioListener.volume = audioVolumeSlider.value;
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
