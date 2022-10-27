using Commons.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GamepadController : MonoBehaviour
    {
        [SerializeField] private CardEventChannel cardEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;

        public float threshold = 0.5f;
        public bool movementPerformed;
        public int lastHoverIndex = -1;
        private int _cardHoverColumnIndex;
        private int _cardHoverRowIndex;
        private bool _isInteractable = true;
        private bool _isGameEnd = false;
        private Table _table;

        private void Start()
        {
            _table = GetComponent<Table>();
            _cardHoverColumnIndex = 0;
            _cardHoverRowIndex = 0;
            InputSystem.onDeviceChange +=
                (device, change) =>
                {
                    if (device.description.deviceClass != "Gamepad") return;
                    if (change is InputDeviceChange.Added or InputDeviceChange.Reconnected)
                        cardEventChannel.HoverCard(0);
                    else cardEventChannel.HoverCard(-1);
                };
        }

        private void OnEnable()
        {
            gameEventChannel.OnGameEnd += OnGameEnd;
            gameEventChannel.OnPauseEvent += OnPauseEvent;
        }

        private void OnDisable()
        {
            gameEventChannel.OnGameEnd -= OnGameEnd;
            gameEventChannel.OnPauseEvent -= OnPauseEvent;
        }

        private void OnGameEnd(bool isEnable)
        {
            _isInteractable = false;
            _isGameEnd = true;
        }

        private void OnPauseEvent(bool isPause) => _isInteractable = isPause;

        public void Move(InputAction.CallbackContext context)
        {
            if (!_isInteractable) return;
            if (context.performed && !movementPerformed)
            {
                var axis = context.ReadValue<Vector2>();
                if (axis.x > threshold && axis.x > 0 && _cardHoverColumnIndex < 3) _cardHoverColumnIndex++;
                else if (axis.x < threshold * -1 && axis.x < 0 && _cardHoverColumnIndex > 0) _cardHoverColumnIndex--;
                if (axis.y > threshold && axis.y > 0 && _cardHoverRowIndex > 0) _cardHoverRowIndex--;
                else if (axis.y < threshold * -1 && axis.y < 0 && _cardHoverRowIndex < 1) _cardHoverRowIndex++;

                var cardHoverIndex = _cardHoverRowIndex * 4 + _cardHoverColumnIndex;
                if (lastHoverIndex == cardHoverIndex) return;
                cardEventChannel.HoverCard(cardHoverIndex);
                lastHoverIndex = cardHoverIndex;
                movementPerformed = true;
            }
            else if (context.canceled && movementPerformed)
            {
                movementPerformed = false;
            }
        }

        public void SelectCard(InputAction.CallbackContext context)
        {
            if(_isGameEnd) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            if (!_isInteractable) return;
            _table.SelectCard(lastHoverIndex);
        }
    }
}