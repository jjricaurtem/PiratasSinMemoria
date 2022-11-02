using Commons.Events;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tables
{
    public class GamepadController : MonoBehaviour
    {
        [SerializeField] private TableEventChannel tableEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;


        public float threshold = 0.5f;
        public bool movementPerformed;
        private int _cardHoverIndex;
        private Controls _controls;
        private bool _isGameEnd;
        private bool _isInteractable = true;
        private PapyrusToggle _papyrusToggle;
        private Table _table;

        private void Awake()
        {
            _controls = new Controls();
            _controls.Default.DefaultAction.performed += _ => SelectCard();
            _controls.Default.Move.performed += ctx => Move(ctx.ReadValue<Vector2>(), true);
            _controls.Default.Move.started += ctx => Move(ctx.ReadValue<Vector2>(), true);
            _controls.Default.Move.canceled += ctx => Move(ctx.ReadValue<Vector2>(), false);
            _controls.Default.ToogleMenu.performed += _ => ToggleMenu();
        }

        private void Start()
        {
            _table = GetComponent<Table>();
            _papyrusToggle = FindObjectOfType<PapyrusToggle>();
            _cardHoverIndex = -1;
        }

        private void OnEnable()
        {
            gameEventChannel.OnGameEnd += OnGameEnd;
            gameEventChannel.OnPauseEvent += OnPauseEvent;
            _controls.Default.Enable();
            InputSystem.onDeviceChange += InputSystemOnDeviceChange;
        }

        private void OnDisable()
        {
            gameEventChannel.OnGameEnd -= OnGameEnd;
            gameEventChannel.OnPauseEvent -= OnPauseEvent;
            _controls.Default.Disable();
            InputSystem.onDeviceChange -= InputSystemOnDeviceChange;
        }

        private void ToggleMenu() => _papyrusToggle.ToggleMenu();

        private void InputSystemOnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("New device added: " + device);
                    _cardHoverIndex = 0;
                    tableEventChannel.HoverCard(_cardHoverIndex);
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("Device removed: " + device);
                    _cardHoverIndex = -1;
                    tableEventChannel.HoverCard(_cardHoverIndex);
                    break;
            }
        }

        private void OnGameEnd(bool isEnable)
        {
            _isInteractable = false;
            _isGameEnd = true;
        }

        private void OnPauseEvent(bool isPause) => _isInteractable = !isPause;

        private void Move(Vector2 axis, bool performed)
        {
            if (!_isInteractable) return;
            switch (performed)
            {
                case true when !movementPerformed:
                {

                    var cardHoverColumnIndex = _cardHoverIndex % 4;
                    var cardHoverRowIndex = _cardHoverIndex / 4;

                    if (axis.x > threshold && axis.x > 0 && cardHoverColumnIndex < 3) cardHoverColumnIndex++;
                    else if (axis.x < threshold * -1 && axis.x < 0 && cardHoverColumnIndex > 0) cardHoverColumnIndex--;
                    if (axis.y > threshold && axis.y > 0 && cardHoverRowIndex > 0) cardHoverRowIndex--;
                    else if (axis.y < threshold * -1 && axis.y < 0 && cardHoverRowIndex < 1) cardHoverRowIndex++;

                    var cardHoverIndex = cardHoverRowIndex * 4 + cardHoverColumnIndex;
                    if (_cardHoverIndex == cardHoverIndex) return;
                    
                    _cardHoverIndex = cardHoverIndex;
                    tableEventChannel.HoverCard(_cardHoverIndex);
                    movementPerformed = true;
                    break;
                }
                case false:
                    movementPerformed = false;
                    break;
            }
        }

        private void SelectCard()
        {
            if (_isGameEnd) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            if (!_isInteractable || _cardHoverIndex < 0) return;
            _table.SelectCard(_cardHoverIndex);
        }
    }
}