using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1f;

    private PlayerControls _playerControls;

    private Vector2 _movement;

    private Rigidbody2D _rb;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _rb.MovePosition(_rb.position + (_movement * (_moveSpeed * Time.fixedDeltaTime)));
    }

    private void AdjustPlayerFacingDirection()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        _spriteRenderer.flipX = Input.mousePosition.x < playerScreenPoint.x;
    }
}
