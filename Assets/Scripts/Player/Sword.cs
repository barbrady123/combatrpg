using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Animator _animator;

    private PlayerController _playerController;

    private ActiveWeapon _activeWeapon;

    [SerializeField]
    private GameObject _slashAnimPrefab;

    [SerializeField]
    private Transform _slashAnimSpawnPoint;

    [SerializeField]
    private Transform _weaponCollider;

    [SerializeField]
    private float _swordAttackCD = 0.5f;

    private GameObject _slashAnimation;

    private bool _attackButtonDown = false;

    private bool _isAttacking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    public void SwingUpFlipAnim()
    {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180f, 0f, 0f);
        _slashAnimation.GetComponent<SpriteRenderer>().flipX = _playerController.FacingLeft;
    }

    public void SwingDownFlipAnim()
    {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _slashAnimation.GetComponent<SpriteRenderer>().flipX = _playerController.FacingLeft;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void Attack()
    {
        if (!_attackButtonDown)
            return;

        if (_isAttacking)
            return;

        _isAttacking = true;

        // fire our sword animation
        _animator.SetTrigger("Attack");
        _weaponCollider.gameObject.SetActive(true);

        _slashAnimation = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
        _slashAnimation.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }

    public void DoneAttackingAnim()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset()
    {
        var mousePos = Input.mousePosition;
        var playerScreenInput = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        _activeWeapon.transform.rotation = Quaternion.Euler(0f, (mousePos.x < playerScreenInput.x) ? -180f : 0f, angle);
        _weaponCollider.transform.rotation = Quaternion.Euler(0f, (mousePos.x < playerScreenInput.x) ? -180f : 0f, 0f);
    }

    // This is a convoluted way to setup this timer...
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(_swordAttackCD);
        _isAttacking = false;
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }
}
