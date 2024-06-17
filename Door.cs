using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Siren _siren;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player Player))
        {
            _siren.OnPlaySiren(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player Player))
        {
            _siren.OnStopSiren(); 
        }
    }
}