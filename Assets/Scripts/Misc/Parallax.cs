using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float _parallaxOffset = -0.15f;

    private Vector2 _startPos;

    private Vector2 Travel => (Vector2)Camera.main.transform.position - _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = _startPos + (this.Travel * _parallaxOffset);
    }
}
