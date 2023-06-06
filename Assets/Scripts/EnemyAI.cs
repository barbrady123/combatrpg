using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPathfinding))]
public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming = 0
    }

    private State _state;

    private EnemyPathfinding _enemyPathfinding;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            _enemyPathfinding.MoveTo(GetRoamingPosition());
            yield return new WaitForSeconds(2);
        }
    }

    private Vector2 GetRoamingPosition() => new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
}
