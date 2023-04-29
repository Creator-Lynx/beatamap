using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{    
    [SerializeField] private Animator _anim;
    [SerializeField] private MeleeDamageTrigger _damageTrigger;

    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }

    private void ActivateTrigger()
    {
        _damageTrigger.Activate();
    }

    private void DeactivateTrigger()
    {
        _damageTrigger.Deactivate();
    }
}
