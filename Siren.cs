using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : MonoBehaviour
{
    [SerializeField] private AudioClip _collisionSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _currentVolume = 0f;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _rateOfIncrease = 0.1f;

    private bool _isPlaying = false;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        _audioSource.clip = _collisionSound;
    }

    public void PlaySiren()
    {
        if (!_isPlaying)
        {
            _isPlaying = true;
            _audioSource.Play();
            StartSirenVolumeChange();
        }
    }

    public void StopSiren()
    {
        if (_isPlaying)
        {
            _isPlaying = false;
            StartSirenVolumeChange();
        }
    }

    private void StartSirenVolumeChange()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        float targetVolume = _isPlaying ? _maxVolume : _minVolume;

        while (_currentVolume != targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _rateOfIncrease * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return waitForEndOfFrame;
        }

        if (_currentVolume == 0f)
        {
            _audioSource.Stop();
        }
    }
}
