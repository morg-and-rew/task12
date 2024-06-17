using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private UnityEvent _enterInHome;
    [SerializeField] private UnityEvent _exitFromHome;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player Player))
        {
            _enterInHome?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player Player))
        {
            _exitFromHome?.Invoke();
        }
    }
}
