using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _velocitySpeed = 4f;
    [SerializeField]
    private float _backwardVelocityMultiplicator = 1.5f;
    [SerializeField]
    private float _forwardVelocityMultiplicator = 1f;
    [SerializeField]
    private float _jumpForce = 100f;
    [SerializeField]
    private float _airControlForce = 50f;
    [SerializeField]
    private float _maxJumpVelocity = 8f;
    [SerializeField]
    private float _bunnyHopForce = 50f;

    private bool _hasJumped = false;

    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        HandleLookDirection();

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var isGoingStraight = horizontalInput == transform.localScale.x;

        if (_groundCheck.IsGrounded)
        {
            _hasJumped = false;
            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _hasJumped = true;
            } else
            {   
                var velocityMultiplier = isGoingStraight ? _forwardVelocityMultiplicator : _backwardVelocityMultiplicator;

                 _rb.velocity = new Vector2(_velocitySpeed * horizontalInput * velocityMultiplier, _rb.velocity.y);
            }
        } else
        {
            if ((horizontalInput > 0 && _rb.velocity.x < _maxJumpVelocity) || (horizontalInput < 0 && _rb.velocity.x > -_maxJumpVelocity))
            {
                _rb.AddForce(Vector2.right * horizontalInput * _airControlForce, ForceMode2D.Force);
            }

            //If looking begind when going up, or looking in front when going down, add bunnyHop force
            if (_hasJumped && (_rb.velocity.y > 0 && !isGoingStraight) || (_rb.velocity.y < 0 && isGoingStraight))
            {
                _rb.AddForce(Vector2.right * horizontalInput * _bunnyHopForce, ForceMode2D.Force);
            }
        }
    }

    

    void HandleLookDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        var lookDirection = (worldPosition - transform.position).normalized;


        if (lookDirection.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (lookDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
