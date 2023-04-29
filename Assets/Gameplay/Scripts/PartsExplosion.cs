using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsExplosion : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _parts;

    public void Explode(Vector3 hitPoint, Vector3 hitDirection)
    {
        var sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.position = hitPoint;
        sph.transform.localScale = Vector3.one * 0.05f;

        foreach (var part in _parts) 
        {                       
            part.AddForceAtPosition(hitDirection, hitPoint, ForceMode.Impulse);
        }
    }
}
