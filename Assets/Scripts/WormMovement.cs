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
    private Camera _wormCamera;
    private AudioListener _audioListener;

    private bool _isGrounded;

    public bool _isActive;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        CheckGrounded();

        if (_isActive)
        {
            MoveFromInput();
            JumpFromInput();
        }
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _wormCamera = GetComponentInChildren<Camera>();
        _audioListener = GetComponentInChildren<AudioListener>();
        EndTurn();
    }

    private void MoveFromInput()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        _rigidbody.velocity = transform.forward * _moveSpeed * zMove + new Vector3(0, _rigidbody.velocity.y, 0);
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime * xMove);

    }

    private void JumpFromInput()
    {
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

    
    public void StartTurn()
    {
        _isActive = true;
        _wormCamera.depth = 10;
        _audioListener.enabled = true;
    }

    public void EndTurn()
    {
        _rigidbody.velocity = new Vector3(0, 0, 0);
        _isActive = false;
        _wormCamera.depth = 0;
        _audioListener.enabled = false;
    }
}
