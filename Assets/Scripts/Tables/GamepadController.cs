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
        public int lastHoverIndex = -1;
        private int _cardHoverColumnIndex;
        private int _cardHoverRowIndex;
        private Controls _controls;
        private bool _isGameEnd;
        private bool _isInteractable = true;
        private Menu _menu;
        private Table _table;

        private void Awake()
        {
            _controls = new Controls();
            _controls.Default.DefaultAction.performed += ctx => SelectCard();
            _controls.Default.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
            _controls.Default.ToogleMenu.performed += ctx => ToggleMenu();
        }

        private void Start()
        {
            _table = GetComponent<Table>();
            _menu = FindObjectOfType<Menu>();
            _cardHoverColumnIndex = 0;
            _cardHoverRowIndex = 0;
            // Controls controls = new Controls();
        }

        private void Update()
        {
            // ManageMenu();
            // Move();
            // SelectCard();
        }

        private void OnEnable()
        {
            gameEventChannel.OnGameEnd += OnGameEnd;
            gameEventChannel.OnPauseEvent += OnPauseEvent;
            _controls.Default.Enable();
            InputSystem.onDeviceChange += InputSystemOnonDeviceChange;
        }

        private void OnDisable()
        {
            gameEventChannel.OnGameEnd -= OnGameEnd;
            gameEventChannel.OnPauseEvent -= OnPauseEvent;
            _controls.Default.Disable();
            InputSystem.onDeviceChange -= InputSystemOnonDeviceChange;
        }

        private void ToggleMenu() => _menu.ToggleMenu();

        private void InputSystemOnonDeviceChange(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("New device added: " + device);
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("Device removed: " + device);
                    break;
            }
        }

        private void OnGameEnd(bool isEnable)
        {
            _isInteractable = false;
            _isGameEnd = true;
        }

        private void OnPauseEvent(bool isPause) => _isInteractable = !isPause;

        private void Move(Vector2 axis)
        {
            if (!_isInteractable) return;
            var performed = axis.magnitude > 0.2;
            if (performed && !movementPerformed)
            {
                if (axis.x > threshold && axis.x > 0 && _cardHoverColumnIndex < 3) _cardHoverColumnIndex++;
                else if (axis.x < threshold * -1 && axis.x < 0 && _cardHoverColumnIndex > 0) _cardHoverColumnIndex--;
                if (axis.y > threshold && axis.y > 0 && _cardHoverRowIndex > 0) _cardHoverRowIndex--;
                else if (axis.y < threshold * -1 && axis.y < 0 && _cardHoverRowIndex < 1) _cardHoverRowIndex++;

                var cardHoverIndex = _cardHoverRowIndex * 4 + _cardHoverColumnIndex;
                if (lastHoverIndex == cardHoverIndex) return;
                tableEventChannel.HoverCard(cardHoverIndex);
                lastHoverIndex = cardHoverIndex;
                movementPerformed = true;
            }
            else if (!performed && movementPerformed)
            {
                movementPerformed = false;
            }
        }

        private void SelectCard()
        {
            if (_isGameEnd) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            if (!_isInteractable || lastHoverIndex < 0) return;
            _table.SelectCard(lastHoverIndex);
        }
    }
}