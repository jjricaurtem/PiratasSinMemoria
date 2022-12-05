using System.Collections;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEmitter : MonoBehaviour
    {
        private AudioSource _audioSource;

        public bool IsPlaying { get; private set; } = false;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;

            if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();
        }


        public void ReproduceClip(AudioClip clip)
        {
            IsPlaying = true;
            _audioSource.Stop();
            _audioSource.time = 0f;
            _audioSource.clip = clip;
            _audioSource.Play();

            StartCoroutine(FinishedPlaying(clip.length));
        }

        private IEnumerator FinishedPlaying(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            IsPlaying = false;
        }
    }
}