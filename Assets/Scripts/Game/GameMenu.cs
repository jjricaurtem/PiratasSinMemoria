using Commons.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Slider audioVolumeSlider;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private float sliderInputSpeed = 0.1f;

        private Controls _controls;
        private PapyrusToggle _papyrusToggle;

        public void Start()
        {
            audioVolumeSlider.value = AudioListener.volume;
        }

        private void OnEnable()
        {
            _papyrusToggle = GetComponent<PapyrusToggle>();
            _papyrusToggle.OnStartTogglePapyrus += OnStartTogglePapyrus;
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Default.ToggleMenu.performed += OnToggleMenu;
                _controls.Default.CancelAction.performed += OnCancelAction;
                _controls.Default.Move.performed += ChangeVolume;
                _controls.Default.ConfirmAction.performed += _ =>
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }

            _controls.Default.Enable();
        }

        private void OnDisable()
        {
            _papyrusToggle.OnStartTogglePapyrus -= OnStartTogglePapyrus;
            _controls?.Default.Disable();
        }

        private void ChangeVolume(InputAction.CallbackContext ctx)
        {
            var inputAxis = ctx.ReadValue<Vector2>();
            audioVolumeSlider.value += inputAxis.x * sliderInputSpeed;
        }

        private void OnStartTogglePapyrus(bool isOpen) => gameEventChannel.OnPauseEvent(!isOpen);

        public void OnAudioVolumeChange()
        {
            AudioListener.volume = audioVolumeSlider.value;
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        private void OnCancelAction(InputAction.CallbackContext ctx)
        {
            _papyrusToggle.CloseOnlyMenu();
        }

        private void OnToggleMenu(InputAction.CallbackContext ctx)
        {
            _papyrusToggle.ToggleMenu();
        }
    }
}