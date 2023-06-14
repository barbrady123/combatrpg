using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private float _dashSpeedFactor = 4f;

    private float _dashTime = 0.2f;

    private float _dashCooldown = 1.0f;

    private bool _isDashing = false;

    private bool _dashOnCooldown = false;

    private PlayerControls _playerControls;

    private Vector2 _movement;

    private Rigidbody2D _rb;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private TrailRenderer _trailRenderer;

    private bool _facingLeft = false;

    public bool FacingLeft => _facingLeft;

    private void Awake()
    {
        PlayerController.Instance = this;
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = gameObject.GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }

    private void Move()
    {
        float moveSpeed = _moveSpeed * (_isDashing ? _dashSpeedFactor : 1.0f);
        _rb.MovePosition(_rb.position + (_movement * (moveSpeed * Time.fixedDeltaTime)));
    }

    private void AdjustPlayerFacingDirection()
    {
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        _facingLeft = Input.mousePosition.x < playerScreenPoint.x;
        _spriteRenderer.flipX = _facingLeft;
    }

    private void Dash()
    {
        if (_dashOnCooldown)
            return;

        _isDashing = true;
        _dashOnCooldown = true;
        _trailRenderer.emitting = true;
        StartCoroutine(EndDashRoutine());
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(_dashTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        _dashOnCooldown = false;
    }
}
