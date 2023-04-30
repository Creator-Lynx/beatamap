using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KatanaController : WeaponController
{
    [SerializeField] private LayerMask _ignoreLayers;
    [SerializeField] private CharacterController _player;    
    private IDamagable _target = null;

    [Header("UI")]
    [SerializeField] private GameObject _targetMarker;

    protected override void OnUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out var hit, 1000f, ~_ignoreLayers))
        {
            var damagable = hit.collider.GetComponent<IDamagable>();
            if(damagable != null && Vector3.Distance(damagable.transform.position, transform.position) <= 5f)
            {
                _target = damagable;
            }
            else
            {
                _target = null;
            }
        }
        else
        {
            _target = null;
        }

        _targetMarker.SetActive(_target != null);
    }

    public override void Attack()
    {                
        if (_target != null)
        {
            var targetPos = _target.transform.position;
            var distanceToTarget = Vector3.Distance(targetPos, transform.position);
            if (distanceToTarget > 2f)
            {
                var dirToTarget = targetPos - transform.position;
                var destinationPoint = dirToTarget - dirToTarget.normalized;
                _player.Move(destinationPoint);
            }
        }

        SetAttackTrigger();
    }
}
