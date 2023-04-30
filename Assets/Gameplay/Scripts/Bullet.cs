using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.SetDamage(5, transform.position, GetComponent<Rigidbody>().velocity.normalized);
        }
        Destroy(gameObject);
    }
}
