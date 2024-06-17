using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-_speed * Time.fixedDeltaTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(_speed * Time.fixedDeltaTime, 0f, 0f);
        }
    }

    private void OnValidate()
    {
        if (_speed < 0f)
            _speed = 2f;
    }
}
