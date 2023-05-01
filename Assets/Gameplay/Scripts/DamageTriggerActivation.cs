using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerActivation : MonoBehaviour
{
    public PlayerController playerController;

    private void ActivateTrigger()
    {
        playerController.CurrentWeapon.ActivateTrigger();
    }

    private void DeactivateTrigger()
    {
        playerController.CurrentWeapon.DeactivateTrigger();
    }

    private void MakeDash()
    {
        ((KatanaController)playerController.CurrentWeapon).Dash();
    }
}
