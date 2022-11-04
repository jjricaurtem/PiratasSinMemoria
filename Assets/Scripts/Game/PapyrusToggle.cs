using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class PapyrusToggle : MonoBehaviour
    {
        [SerializeField] private float menuAnimationTime;
        [SerializeField] private Button closeMenuButton;
        [SerializeField] private AudioClip openMenuAudioClip;
        [SerializeField] private AudioClip closeMenuAudioClip;
        [SerializeField] private Canvas canvas;
        [SerializeField] private float closeYPosition = -300f;
        
        public UnityAction<bool> OnStartTogglePapyrus;
        private const float OpenYPosition = 0f;
        private bool _isAnimating;
        private bool _isOpen;
        private AudioSource _audioSource;

        public void CloseOnlyMenu()
        {
            if (_isOpen) CloseMenuButton_clicked();
        }

        public void ToggleMenu()
        {
            if (_isOpen) CloseMenuButton_clicked();
            else OnMenuClickHandler();
        }
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
            OnStartTogglePapyrus?.Invoke(_isOpen);
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
        }
    }
}
