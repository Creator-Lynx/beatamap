using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistsController : WeaponController
{
    public override void Attack()
    {
        SetAttackTrigger();
    }

    public override void SetPlayerHP(int hp) { }
}
