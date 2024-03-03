using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
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
    [SerializeField]
    private float _marioJumpBoostImpulse = 50f;

    [SerializeField]
    private LayerMask _groundMask;

    private bool _isJumping = false;
    private bool _isCrouching = false;

    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private WallBehindCheck _wallBehindCheck;
    private Animator _animator;

    [SerializeField]
    private AudioClip _jumpClip;
    [SerializeField]
    private AudioClip _crouchClip;

    private AudioSource _audioSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _wallBehindCheck = GetComponentInChildren<WallBehindCheck>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Jump();
        _animator.SetBool("isJumping", _isJumping);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleLookDirection();

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var isGoingStraight = horizontalInput == transform.localScale.x;

        if (_rb.velocity.y == 0.0f) _isJumping = false;

        if (_groundCheck.IsGrounded && !_isJumping)
        {
            if (_isCrouching && !isGoingStraight && horizontalInput != 0 && _wallBehindCheck.IsAgainstWall)
            {
                HandleMarioBoost(horizontalInput);
            } else
            {
                var velocityMultiplier = isGoingStraight ? _forwardVelocityMultiplicator : _backwardVelocityMultiplicator;
                var newVelocity = (_velocitySpeed * horizontalInput * velocityMultiplier);
                var currVelocityBiggerThanSpeed = (((newVelocity < 0 && _rb.velocity.x < 0) || (newVelocity > 0 && _rb.velocity.x > 0))
                    && Mathf.Abs(newVelocity) < Mathf.Abs(_rb.velocity.x));


                if (_groundCheck.IsOnSlope)
                {
                    _rb.velocity = new Vector2(newVelocity * -_groundCheck.SlopeNormalPerp.x, newVelocity * -_groundCheck.SlopeNormalPerp.y);
                }
                else
                {
                    if(!currVelocityBiggerThanSpeed) _rb.velocity = new Vector2(newVelocity, 0.0f);
                }

                _animator.SetBool("isWalking", _rb.velocity.x != 0.0f);
            }
        } else
        {
            if ((horizontalInput > 0 && _rb.velocity.x < _maxJumpVelocity) || (horizontalInput < 0 && _rb.velocity.x > -_maxJumpVelocity))
            {
                _rb.AddForce(Vector2.right * horizontalInput * _airControlForce, ForceMode2D.Force);
            }

            //If looking begind when going up, or looking in front when going down, add bunnyHop force
            if (_isJumping && (_rb.velocity.y > 0 && !isGoingStraight) || (_rb.velocity.y < 0 && isGoingStraight))
            {
                _rb.AddForce(Vector2.right * horizontalInput * _bunnyHopForce, ForceMode2D.Force);
            }
        }

        if(Input.GetKey(KeyCode.S))
        {
            if(!_isCrouching)
            {
                _audioSource.clip = _crouchClip;
                _audioSource.Play();
            }
            _animator.SetBool("isCrouching", true);
            _animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(transform.localScale.x, 0.8f, transform.localScale.z);
            _isCrouching = true;
        } else
        {
            _animator.SetBool("isCrouching", false);
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            _isCrouching = false;
        }
    }

    void HandleMarioBoost(float horizontalInput)
    {
        _rb.AddForce(Vector2.right * horizontalInput * _marioJumpBoostImpulse, ForceMode2D.Force);
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && _groundCheck.IsGrounded)
        {
            _animator.SetBool("isWalking", false);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumping = true;
            _audioSource.clip = _jumpClip;
            _audioSource.Play();
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
