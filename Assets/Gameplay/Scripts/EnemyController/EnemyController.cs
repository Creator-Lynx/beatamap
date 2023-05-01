using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] private int HP = 3;
    [SerializeField] private PartsExplosion DestroyPartsPrefab;

    [SerializeField] private int _damage = 1;
    [SerializeField] private LayerMask _ignoreLayers;

    private EnemyState _currentState = EnemyState.Idle;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    private PlayerController _player;

    public Action<EnemyController> OnDeath;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
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

    public void AwakeEnemy()
    {
        _animator.SetTrigger("Awake");
    }

    public void SetDamage(int damageRate, Vector3 hitPoint, Vector3 hitDirection)
    {
        HP -= damageRate;
        if (HP <= 0)
        {
            OnDeath?.Invoke(this);
            var parts = Instantiate(DestroyPartsPrefab, transform.position, transform.rotation);
            parts.Explode(hitPoint, hitDirection);
            Destroy(gameObject);
        }
        else
        {
            _animator.SetTrigger("Stunned");
        }
    }

    private void SetState(EnemyState state)
    {
        _currentState = state;
    }

    private void DamagePlayer()
    {        
        var rayOrigin = transform.position + Vector3.up;
        if(Physics.SphereCast(rayOrigin, 0.5f, transform.forward, out var hit, 1.5f, ~_ignoreLayers))
        {            
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<PlayerController>().SetDamage(_damage);
            }
        }
    }
}
