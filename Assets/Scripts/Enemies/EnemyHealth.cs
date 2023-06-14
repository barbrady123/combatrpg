using UnityEngine;

[RequireComponent(typeof(Knockback))]
[RequireComponent(typeof(Flash))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 3;

    private int _currentHealth;

    private Knockback _knockback;

    private Flash _flash;

    [SerializeField]
    private GameObject _deathVFXPrefab;

    [SerializeField]
    private float _knockbackPower = 15f;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _flash = GetComponent<Flash>();
    }

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockback.GetKnockedBack(PlayerController.Instance.transform, _knockbackPower);
        StartCoroutine(_flash.FlashRoutine(DetectDeath));
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
