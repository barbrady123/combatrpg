using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _knockbackTime = 0.2f;

    public bool GettingKnockedBack { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockbackThrust)
    {
        this.GettingKnockedBack = true;
        var diff = (transform.position - damageSource.position).normalized * knockbackThrust * _rb.mass;
        _rb.AddForce(diff, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(_knockbackTime);
        this.GettingKnockedBack = false;
    }
}
