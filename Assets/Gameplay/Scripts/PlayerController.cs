using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _gravity = -13.0f;
    private float _velocityY = 0f;
    private CharacterController _controller;

    [Header("Look")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Vector2 LookSpeed = new Vector2(2f, 2f);
    [SerializeField] private float _viewingSens = 10;
    private float x = 0;
    private float y = 0;    

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
        PlayerLook();
        PlayerAttack();
    }

    private void PlayerMovement()
    {
        var moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();        

        Vector3 velocity = (transform.forward * moveDir.y + transform.right * moveDir.x) * _moveSpeed;
        velocity.y = 0;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerLook()
    {
        float mouseY = -Input.GetAxisRaw("Mouse Y");
        float mouseX = Input.GetAxisRaw("Mouse X");

        x += mouseY * LookSpeed.x * _viewingSens;
        x = Mathf.Clamp(x, -90, 90);
        y += mouseX * LookSpeed.y * _viewingSens;

        PlayerCamera.localRotation = Quaternion.Euler(x, 0, 0);
        PlayerCamera.localPosition = new Vector3(0, 1.8f, 0);
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    private void PlayerAttack()
    {
        
    }
}
