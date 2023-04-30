using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    [SerializeField] private string _weaponName;
    [SerializeField] private int _attacksCount = 2;

    [HideInInspector] public bool IsWalking = false;
    [HideInInspector] public bool IsStrongAttack = false;

    [SerializeField] private GameObject _objToActive;
    [SerializeField] private Animator _animator;
    [SerializeField] private MeleeDamageTrigger _damageTrigger;

    public void ActivateWeapon()
    {
        _objToActive?.SetActive(true);
        var triggerName = $"{_weaponName}_Get";
        _animator.SetTrigger(triggerName);
    }

    public void DeactivateWeapon()
    {
        _objToActive?.SetActive(false);
    }

    public abstract void Attack();

    protected void SetAttackTrigger()
    {
        var attackNum = Random.Range(0, _attacksCount);
        var triggerName = $"{_weaponName}_Attack{attackNum}";
        _animator.SetTrigger(triggerName);
    }

    private void Update()
    {
        _animator.SetBool("Walk", IsWalking);
        _animator.SetBool($"{_weaponName}_AttackStrong", IsStrongAttack);
        OnUpdate();
    }

    protected virtual void OnUpdate() { }

    private void ActivateTrigger()
    {
        _damageTrigger.Activate();
    }

    private void DeactivateTrigger()
    {
        _damageTrigger.Deactivate();
    }
}
