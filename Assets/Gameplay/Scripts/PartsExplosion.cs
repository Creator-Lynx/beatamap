using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsExplosion : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _parts;

    public void Explode(Vector3 hitPoint, Vector3 hitDirection)
    {
        foreach (var part in _parts) 
        {                       
            //part.AddForceAtPosition(hitDirection, hitPoint, ForceMode.Impulse);
            part.AddForce(hitDirection, ForceMode.Impulse);
        }
    }
}
