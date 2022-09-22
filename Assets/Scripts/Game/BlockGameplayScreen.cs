using Commons.Events;
using UnityEngine;

namespace Game
{
    public class BlockGameplayScreen : MonoBehaviour
    {
        [SerializeField] private GameEventChannel gameEventChannel;
        private Collider2D _collider;
        private Renderer _renderer;

        private void OnEnable() => gameEventChannel.OnPauseEvent += OnPauseEvent;
        private void OnDisable() => gameEventChannel.OnPauseEvent -= OnPauseEvent;

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();

            _collider.enabled = false;
            _renderer.enabled = false;
        }

        private void OnPauseEvent(bool isPause)
        {
            _collider.enabled = isPause;
            _renderer.enabled = isPause;
        }
    }
}
