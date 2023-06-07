using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 3;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
