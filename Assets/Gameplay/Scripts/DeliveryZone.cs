using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private List<string> _requiredPackages = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var player = other.GetComponent<PlayerController>();
            var isInventoryMatch = true;
            foreach(var package in _requiredPackages)
            {
                isInventoryMatch &= player.IsInventoryContains(package);
                if(!isInventoryMatch)
                {
                    return;
                }
            }
            //TODO: zone completed event
            Destroy(gameObject);
        }
    }
}
