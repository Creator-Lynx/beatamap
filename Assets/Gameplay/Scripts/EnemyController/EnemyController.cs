using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyState _currentState = EnemyState.Idle;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    private PlayerController _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(_currentState == EnemyState.Idle && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Awake");
        }

        if(_currentState == EnemyState.Walk)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);

            if(_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _animator.SetTrigger("Attack");
            }
        }
        else
        {
            _agent.isStopped = true;
        }
    }

    public void SetDamage(int damageRate, Vector3 hitPoint, Vector3 hitDirection)
    {
        _animator.SetTrigger("Stunned");
    }

    private void SetState(EnemyState state)
    {
        _currentState = state;
    }
}
