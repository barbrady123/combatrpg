using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack()
    {
        // fire our sword animation
        _animator.SetTrigger("Attack");
    }
}
