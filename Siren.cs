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

    private bool _isEnter = true;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        _audioSource.clip = _collisionSound;
    }

    public void PlaySiren()
    {
        _isEnter = true;

        _audioSource.Play();

        _startSirenVolumeChange();
    }

    public void StopSiren()
    {
        _isEnter = !_isEnter;

        _startSirenVolumeChange();
    }

    private void _startSirenVolumeChange()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeVolume(_isEnter));
    }

    private IEnumerator ChangeVolume(bool increase)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        float targetVolume = increase ? _maxVolume : _minVolume;

        while (_currentVolume != targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _rateOfIncrease * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return waitForEndOfFrame;
        }
    }
}