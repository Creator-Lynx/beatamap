using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterTrigger : MonoBehaviour
{
    [SerializeField] private RoomController _room;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _room.OnPlayerEnter();
            Destroy(gameObject);
        }
    }
}
