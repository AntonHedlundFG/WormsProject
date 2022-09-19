using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WormWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private GameObject[] _weapons;

    private GameObject _equippedWeaponObject;
    private IWeapon _equippedWeapon;
    private WormHandler _wormHandler;
    private TurnHandler _turnHandler;
    private ChargeMeter _chargeMeter;

    private bool _startedShootingThisTurn = false;
    private bool _hasShotThisTurn = false;

    private float _endTurnDelay = 5f;
    private float _rotationSpeed = 50f;

    private float _maxrotation = 110f;
    private float _minrotation = 10f;

    void Start()
    {
        Init();
    }
    void Update()
    {
        if (_wormHandler.IsActive() && _equippedWeaponObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_hasShotThisTurn)
            {
                _equippedWeapon.PreShoot();
                _startedShootingThisTurn = true;
            }

            if (Input.GetKeyUp(KeyCode.Space) && !_hasShotThisTurn)
            {
                _equippedWeapon.Shoot();
                _hasShotThisTurn = true;
                _turnHandler.NextActiveWorm(_endTurnDelay);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                RotateWeapon(_rotationSpeed * Time.deltaTime);
            } else if (Input.GetKey(KeyCode.DownArrow))
            {
                RotateWeapon(-_rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && !_startedShootingThisTurn)
            {
                EquipWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !_startedShootingThisTurn)
            {
                EquipWeapon(1);
            }
        }

        

        
    }

    void Init()
    {
        _wormHandler = GetComponentInParent<WormHandler>();
        _turnHandler = TurnHandler.Instance;
        _chargeMeter = GetComponentInChildren<ChargeMeter>();
    }
    public void EquipWeapon(int weaponID)
    {
        if (weaponID >= _weapons.Length)
        {
            Debug.Log("Testing?");
            return;
        }
        UnEquipWeapon();
        _equippedWeaponObject = Instantiate(_weapons[weaponID], _weaponHolder.transform.position, Quaternion.identity, _weaponHolder.transform);
        _equippedWeapon = _equippedWeaponObject.GetComponent<IWeapon>();
        _equippedWeapon.SetChargeMeter(_chargeMeter);
        RotateWeapon(_equippedWeaponObject.GetComponent<IWeapon>().GetStartRotation());
        
    }

    public void UnEquipWeapon()
    {
        Destroy(_equippedWeaponObject);
    }

    public void RotateWeapon(float rotationDegrees)
    {
        float currentRotation = _equippedWeaponObject.transform.localEulerAngles.x;
        float newRotation = Mathf.Clamp(currentRotation + rotationDegrees, _minrotation, _maxrotation);
        _equippedWeaponObject.transform.localEulerAngles = new Vector3(newRotation, 0, 0);
    }

    public void ResetShotStatus()
    {
        _hasShotThisTurn = false;
        _startedShootingThisTurn = false;
    }

    public ChargeMeter GetChargeMeter()
    {
        return _chargeMeter;
    }
}
