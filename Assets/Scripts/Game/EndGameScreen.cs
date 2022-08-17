using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioClip[] winAudioClips;
    [SerializeField] private AudioClip[] loseAudioClips;
    [SerializeField] private GameObject[] objectsToShow;
    [SerializeField] private GameEventChannel gameEventChannel;

    private void OnEnable() => gameEventChannel.OnGameEnd += OnGameEnd;
    private void OnDisable() => gameEventChannel.OnGameEnd -= OnGameEnd;


    private void OnGameEnd(bool isAWin)
    {
        text.text = isAWin ? "You Win" : "You Lose";
        foreach (var gameObject in objectsToShow) gameObject.SetActive(true);

        var audioClips = isAWin ? winAudioClips : loseAudioClips;
        _audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        _audioSource.Play();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
