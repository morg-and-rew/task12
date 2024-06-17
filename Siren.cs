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
    private float _fadeTime = 0.1f;
    private float _magnificationTime = 0.1f;

    private bool _isEnter = true;

    private Coroutine _currentCoroutine;

    public delegate void SirenStateChanged(bool isOn); 
    public event SirenStateChanged OnSirenStateChanged; 

    private void Start()
    {
        _audioSource.clip = _collisionSound;
    }

    private void _startChangeSirenVolume()
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
        float time = increase ? _magnificationTime : _fadeTime;

        while (_currentVolume != targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, time * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return waitForEndOfFrame;
        }

        OnSirenStateChanged?.Invoke(_isEnter);
    }

    public void OnPlaySiren()
    {
        _isEnter = true;

        _audioSource.Play();

        _startChangeSirenVolume();
    }

    public void OnStopSiren()
    {
        _isEnter = !_isEnter;

        _startChangeSirenVolume();
    }
}