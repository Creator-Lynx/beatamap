using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    public int HP = 5;
    public PartsExplosion DestroyPartsPrefab;

    public void SetDamage(int damageRate, Vector3 hitPoint, Vector3 hitDirection)
    {
        HP -= damageRate;
        if (HP <= 0)
        {
            var parts = Instantiate(DestroyPartsPrefab, transform.position, transform.rotation);
            parts.Explode(hitPoint, hitDirection);
            Destroy(gameObject); 
        }
    }
}
