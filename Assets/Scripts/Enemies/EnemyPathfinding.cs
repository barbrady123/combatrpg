using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Knockback))]
public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;

    private Rigidbody2D _rb;

    private Knockback _knockback;

    private Vector2 _moveDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
    }

    private void FixedUpdate()
    {
        if (_knockback.GettingKnockedBack)
            return;

        _rb.MovePosition(_rb.position + (_moveDir * (_moveSpeed * Time.fixedDeltaTime)));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }
}
