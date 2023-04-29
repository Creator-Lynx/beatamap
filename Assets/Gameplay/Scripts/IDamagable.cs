using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void SetDamage(int damageRate, Vector3 hitPoint, Vector3 hitDirection);
}
