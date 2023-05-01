using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CudgelController : WeaponController
{
    public override void Attack()
    {
        SetAttackTrigger();
    }

    public override void SetPlayerHP(int hp)
    {
        //TODO: скейл параметров булавы
    }
}
