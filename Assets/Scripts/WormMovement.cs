using UnityEngine;

public class WormMovement : MonoBehaviour
{
    private float _moveSpeed = 5;
    private float _jumpHeight = 10;
    private float _rotateSpeed = 50;

    private Collider _collider;
    private Rigidbody _rigidbody;

    private bool _isGrounded;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        CheckGrounded();
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Move(float moveValue)
    {
        _rigidbody.velocity = transform.forward * _moveSpeed * moveValue + new Vector3(0, _rigidbody.velocity.y, 0);
    }
    public void Rotate(float rotateValue)
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime * rotateValue);
    }

    private void CheckGrounded()
    {
        float groundDistance = _collider.bounds.extents.y;
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f);
    }
    
    public void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.VelocityChange);
        }
    }

}
