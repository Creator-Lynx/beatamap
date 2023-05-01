using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    [SerializeField] private string _weaponName;
    [SerializeField] private int _attacksCount = 2;

    [HideInInspector] public bool IsWalking = false;    

    [SerializeField] private GameObject _objToActive;
    [SerializeField] private Animator _animator;
    [SerializeField] private MeleeDamageTrigger[] _damageTriggers;

    [SerializeField] protected int _currentDamage = 1;
    [SerializeField] protected int _currentStrongDamage = 1;

    private bool _isActive = false;
    private int _curCombo = 0;

    public void ActivateWeapon(int curPlayerHP)
    {
        _isActive = true;
        if ( _objToActive) { _objToActive.SetActive(true); }
        var triggerName = $"{_weaponName}_Get";
        _animator.SetTrigger(triggerName);
        SetPlayerHP(curPlayerHP);
    }

    public void DeactivateWeapon()
    {
        _isActive = false;
        if (_objToActive) { _objToActive.SetActive(false); }
    }

    public abstract void Attack();

    protected void SetAttackTrigger()
    {        
        var triggerName = $"{_weaponName}_Attack{_curCombo}";
        _animator.SetTrigger(triggerName);
        _curCombo = Repeat(++_curCombo, 0, _attacksCount - 1);
    }

    private int Repeat(int val, int min, int max)
    {
        if (val > max) return min;
        else if (val < min) return max;
        else return val;
    }

    public void StrongAttackPrepare()
    {
        var triggerName = $"{_weaponName}_AttackStrong_Prepare";
        _animator.SetTrigger(triggerName);
    }

    public void StrongAttackRelease()
    {
        var triggerName = $"{_weaponName}_AttackStrong_Release";
        _animator.SetTrigger(triggerName);
    }

    private void Update()
    {
        if (_isActive)
        {
            _animator.SetBool("Walk", IsWalking);            
            OnUpdate();
        }
    }

    protected virtual void OnUpdate() { }

    public void ActivateTrigger(bool isStrong)
    {
        if (_damageTriggers.Length > 0) 
        {
            foreach (var trigger in _damageTriggers)
            {
                var dmg = isStrong ? _currentStrongDamage : _currentDamage;
                trigger.Activate(dmg);
            }
        }
    }

    public void DeactivateTrigger()
    {
        if (_damageTriggers.Length > 0)
        {
            foreach (var trigger in _damageTriggers)
            {
                trigger.Deactivate();
            }
        }
    }

    public abstract void SetPlayerHP(int hp);
    //TODO: сюда прилетает хп игрока, тут можно проводить вычисления урона и тд    
}
