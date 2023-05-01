using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageTrigger : MonoBehaviour
{
    private bool _isActive = false;
    private int _curDamage = 1;

    public void Activate(int damage)
    {
        _curDamage = damage;
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false; 
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (_isActive)
        {
            var damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.SetDamage(_curDamage, collision.contacts[0].point, -collision.contacts[0].normal);
            }
        }
    }
}
