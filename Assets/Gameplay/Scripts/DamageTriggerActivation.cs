using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerActivation : MonoBehaviour
{
    public PlayerController playerController;

    private void ActivateTrigger(int isStrong)
    {
        playerController.CurrentWeapon.ActivateTrigger(isStrong != 0);
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
