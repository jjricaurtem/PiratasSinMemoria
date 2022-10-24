using Commons.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
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
        private bool _isGameEnd;
        private bool _isInteractable = true;
        private Table.Table _table;

        private void Start()
        {
            _table = GetComponent<Table.Table>();
            _cardHoverColumnIndex = 0;
            _cardHoverRowIndex = 0;
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

        private void Update()
        {
            Move();
            SelectCard();
        }

        private void Move()
        {
            if (!_isInteractable) return;
            var axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
            if (!Input.GetButton("Fire1")) return;
            if (_isGameEnd) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            if (!_isInteractable || lastHoverIndex < 0) return;
            _table.SelectCard(lastHoverIndex);
        }
    }
}