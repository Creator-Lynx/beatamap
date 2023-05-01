using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KatanaController : WeaponController
{
    [SerializeField] private LayerMask _ignoreLayers;
    [SerializeField] private CharacterController _player;    
    private IDamagable _target = null;
    private Coroutine _dash = null;

    [Header("UI")]
    [SerializeField] private GameObject _targetMarker;

    protected override void OnUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out var hit, 1000f, ~_ignoreLayers))
        {
            var damagable = hit.collider.GetComponent<IDamagable>();
            if(damagable != null && Vector3.Distance(damagable.transform.position, transform.position) <= 7f)
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
        SetAttackTrigger();              
    }

    public void Dash()
    {
        if(_dash != null)
        {
            StopCoroutine(_dash);
        }

        StartCoroutine(DashMovement());
    }

    private IEnumerator DashMovement()
    {  
        if (_target != null)
        {
            var selectedTarget = _target;            
            while (true)
            {
                var targetPos = selectedTarget.transform.position;
                var distanceToTarget = Vector3.Distance(targetPos, _player.transform.position);

                if(distanceToTarget <= 1.5f) { break; }

                var dirToTarget = targetPos - _player.transform.position;
                dirToTarget.Normalize();

                _player.Move(dirToTarget * 15f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        _dash = null;
    }
}
