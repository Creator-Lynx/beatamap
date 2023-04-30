using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<string> _inventory = new List<string>();

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

    [Header("Attack")]
    [SerializeField] private WeaponController _fists;  
    [SerializeField] private WeaponController _cudgel;
    [SerializeField] private WeaponController _katana;

    private bool _isShooting = false;
    [SerializeField] private GunController _gunController;

    [field: SerializeField] public WeaponController CurrentWeapon { get; private set; }
 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
        //SetWeapon(WeaponType.Fists);
        //SetWeapon(WeaponType.Katana);
        SetWeapon(WeaponType.Cudgel);
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

        if (_controller.isGrounded)
        {
            _velocityY = 0f;
        }
        _velocityY += _gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * moveDir.y + transform.right * moveDir.x) * _moveSpeed + _velocityY * Vector3.up;        
        _controller.Move(velocity * Time.deltaTime);

        CurrentWeapon.IsWalking = moveDir.magnitude > 0;        
    }

    private void PlayerLook()
    {
        float mouseY = -Input.GetAxisRaw("Mouse Y");
        float mouseX = Input.GetAxisRaw("Mouse X");

        x += mouseY * LookSpeed.x * _viewingSens;
        x = Mathf.Clamp(x, -90, 90);
        y += mouseX * LookSpeed.y * _viewingSens;

        PlayerCamera.localRotation = Quaternion.Euler(x, 0, 0);
        //PlayerCamera.localPosition = new Vector3(0, 1.8f, 0);
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    public void SetWeapon(WeaponType weaponType)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.DeactivateWeapon();
        }

        switch(weaponType)
        {
            case WeaponType.Fists:
                CurrentWeapon = _fists;
                break;

            case WeaponType.Cudgel:
                CurrentWeapon = _cudgel; 
                break;

            case WeaponType.Katana:
                CurrentWeapon = _katana;
                break;
        }

        CurrentWeapon.ActivateWeapon();
    }

    private void PlayerAttack()
    {
        if(Input.GetButtonDown("Fire1") && !_isShooting)
        {
            CurrentWeapon.Attack();
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            CurrentWeapon.StrongAttackPrepare();
        }

        if(Input.GetMouseButtonUp(1))
        {
            CurrentWeapon.StrongAttackRelease();
        }

        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    _isShooting = true;
        //    CurrentWeapon.gameObject.SetActive(false);
        //    _gunController.gameObject.SetActive(true);
        //    _gunController.StartShooting();
        //}

        //if(_isShooting && _gunController.isFinished)
        //{
        //    _isShooting = false;
        //    _gunController.gameObject.SetActive(false);
        //    CurrentWeapon.gameObject.SetActive(true);
        //}
    }

    public void AddToInventory(string packageID)
    {
        _inventory.Add(packageID);
    }

    public bool IsInventoryContains(string packageID)
    {
        return _inventory.Contains(packageID);
    }
}
