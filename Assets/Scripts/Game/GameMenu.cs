using System;
using Commons.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Slider audioVolumeSlider;
        [SerializeField] private GameEventChannel gameEventChannel;

        private PapyrusToggle _papyrusToggle;
        public void Start()
        {
            audioVolumeSlider.value = AudioListener.volume;
        }

        private void OnEnable()
        {
            _papyrusToggle = GetComponent<PapyrusToggle>();
            _papyrusToggle.OnStartTogglePapyrus += OnStartTogglePapyrus;
        }

        private void OnDisable() => _papyrusToggle.OnStartTogglePapyrus -= OnStartTogglePapyrus;

        private void OnStartTogglePapyrus(bool isOpen) => gameEventChannel.OnPauseEvent(!isOpen);

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