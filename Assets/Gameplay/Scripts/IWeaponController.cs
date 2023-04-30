using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    GameObject gameObject { get; }
    void Attack();
}
