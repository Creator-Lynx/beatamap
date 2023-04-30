using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_POTOM_UDALU : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) DoDestroy();
    }
    [SerializeField]
    float force = 5f, radius = 5f, upwardModifier = 1f;
    [SerializeField]
    GameObject completePart, destroyedParts;
    void DoDestroy()
    {
        completePart.SetActive(false);
        destroyedParts.SetActive(true);
        Rigidbody[] parts = destroyedParts.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody part in parts)
        {
            part.AddExplosionForce(force, completePart.transform.position, radius, upwardModifier, ForceMode.Impulse);
        }
    }
}
