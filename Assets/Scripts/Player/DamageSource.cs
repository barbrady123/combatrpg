using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField]
    private int _damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.gameObject.GetComponent<EnemyHealth>();
        if (health == null)
            return;

        health.TakeDamage(_damageAmount);
    }
}
