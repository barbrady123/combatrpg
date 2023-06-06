using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;

    private Rigidbody2D _rb;

    private Vector2 _moveDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (_moveDir * (_moveSpeed * Time.fixedDeltaTime)));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }
}
