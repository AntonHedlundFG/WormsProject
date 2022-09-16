using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WormMovement : MonoBehaviour
{
    private float _moveSpeed = 5;
    private float _jumpHeight = 10;
    private float _rotateSpeed = 50;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private WormHandler _wormHandler;

    private bool _isGrounded;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        CheckGrounded();

        if (_wormHandler.IsActive())
        {
            MoveFromInput();
        }
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _wormHandler = GetComponent<WormHandler>();
    }

    private void MoveFromInput()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        _rigidbody.velocity = transform.forward * _moveSpeed * zMove + new Vector3(0, _rigidbody.velocity.y, 0);
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime * xMove);


        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.VelocityChange);
        }

    }

    private void CheckGrounded()
    {
        float groundDistance = _collider.bounds.extents.y;
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f);
    }

    public void Stop() 
    {
        _rigidbody.velocity = new Vector3(0, 0, 0);
    }
    
}
