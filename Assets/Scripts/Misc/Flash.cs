using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flash : MonoBehaviour
{
    [SerializeField]
    private Material _whiteFlashMat;

    [SerializeField]
    private float _restoreDefaultMatTime = 0.2f;

    private SpriteRenderer _spriteRenderer;

    private Material _defaultMat;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }

    public IEnumerator FlashRoutine(Action afterAction)
    {
        _spriteRenderer.material = _whiteFlashMat;
        yield return new WaitForSeconds(_restoreDefaultMatTime);
        _spriteRenderer.material = _defaultMat;
        afterAction();
    }
}
