using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    [field: SerializeField] public string PackageID { get; private set; }

    private void Update()
    {
        transform.Rotate(Vector3.up * 60f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().AddToInventory(PackageID);
            Destroy(gameObject);
        }
    }
}
