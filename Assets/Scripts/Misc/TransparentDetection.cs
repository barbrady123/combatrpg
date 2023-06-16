using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _transparencyAmount = 0.8f;

    [SerializeField]
    private float _fadeTime = 0.4f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tileMap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tileMap = GetComponent<Tilemap>();

        if ((_spriteRenderer == null) && (_tileMap == null))
            throw new Exception("Component requires SpriteRenderer or Tilemap");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController == null)
            return;

        StartCoroutine(FadeRoutine(_transparencyAmount));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController == null)
            return;

        StartCoroutine(FadeRoutine(1.0f));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        bool isSpriteRenderer = (_spriteRenderer != null);

        float elapsedTime = 0f;
        float startAlpha = isSpriteRenderer ? _spriteRenderer.color.a : _tileMap.color.a;

        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _fadeTime);

            if (isSpriteRenderer)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newAlpha);
            }
            else
            {
                _tileMap.color = new Color(_tileMap.color.r, _tileMap.color.g, _tileMap.color.b, newAlpha);
            }

            yield return null;
        }
    }
}
