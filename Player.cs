using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    private float _horizontalSpeed = 0f;
    private float _verticalSpeed = 0f;
    private float _zeroSpeed = 0f; 

    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 2f);
    }

    private void FixedUpdate()
    {
        _horizontalSpeed = _zeroSpeed; 
        _verticalSpeed = Input.GetKey(KeyCode.W) ? -_speed : Input.GetKey(KeyCode.S) ? _speed : _zeroSpeed;
        transform.Translate(_horizontalSpeed * Time.fixedDeltaTime, _verticalSpeed * Time.fixedDeltaTime, _zeroSpeed);
    }
}