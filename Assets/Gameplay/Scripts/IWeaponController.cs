using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    public GameObject gameObject { get; }

    public abstract void Attack();
}
