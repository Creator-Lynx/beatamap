using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatticeController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public void Close()
    {
        _anim.SetTrigger("Close");
    }

    public void Open()
    {
        _anim.SetTrigger("Open");
    }
}
