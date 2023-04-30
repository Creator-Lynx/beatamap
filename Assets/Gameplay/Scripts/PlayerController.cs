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
    [InterfaceField(typeof(IWeaponController))] 
    [SerializeField] private Object _cudgelObject;
    private IWeaponController _cudgel => _cudgelObject as IWeaponController;

    [InterfaceField(typeof(IWeaponController))]
    [SerializeField] private Object _katanaObject;
    private IWeaponController _katana => _katanaObject as IWeaponController;

    private bool _isShooting = false;
    [SerializeField] private GunController _gunController;

    private IWeaponController _currentWeapon;
 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
        SetWeapon(WeaponType.Katana);
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

    public void SetWeapon(WeaponType weaponType)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
        }

        switch(weaponType)
        {
            case WeaponType.Cudgel:
                _currentWeapon = _cudgel; 
                break;

            case WeaponType.Katana:
                _currentWeapon = _katana;
                break;
        }

        _currentWeapon.gameObject.SetActive(true);
    }

    private void PlayerAttack()
    {
        if(Input.GetButtonDown("Fire1") && !_isShooting)
        {
            _currentWeapon.Attack();
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            _isShooting = true;
            _currentWeapon.gameObject.SetActive(false);
            _gunController.gameObject.SetActive(true);
            _gunController.StartShooting();
        }

        if(_isShooting && _gunController.isFinished)
        {
            _isShooting = false;
            _gunController.gameObject.SetActive(false);
            _currentWeapon.gameObject.SetActive(true);
        }
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
