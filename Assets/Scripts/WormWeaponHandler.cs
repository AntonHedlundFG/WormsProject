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

    private float _maxrotation = 110f;
    private float _minrotation = 10f;

    void Start()
    {
        Init();
    }
    void Init()
    {
        _wormHandler = GetComponentInParent<WormHandler>();
        _turnHandler = TurnHandler.Instance;
        _chargeMeter = GetComponentInChildren<ChargeMeter>();
    }

    public void Shoot()
    {
        if (!_hasShotThisTurn && _startedShootingThisTurn)
        {
            _equippedWeapon.Shoot();
            _hasShotThisTurn = true;
            _wormHandler.EndTurn();
        }
    }

    public void PreShoot()
    {
        if (!_startedShootingThisTurn)
        {
            _equippedWeapon.PreShoot();
            _startedShootingThisTurn = true;
        }
    }

    public void SwapToGrenadeLauncher()
    {
        if (!_startedShootingThisTurn)
            EquipWeapon(0);
    }
    public void SwapToShotGun()
    {
        if (!_startedShootingThisTurn)
            EquipWeapon(1);
    }

    
    public void EquipWeapon(int weaponID)
    {
        if (weaponID >= _weapons.Length || weaponID < 0)
            return;
        UnEquipWeapon();
        _equippedWeaponObject = Instantiate(_weapons[weaponID], _weaponHolder.transform.position, Quaternion.identity, _weaponHolder.transform);
        _equippedWeapon = _equippedWeaponObject.GetComponent<IWeapon>();
        _equippedWeapon.SetChargeMeter(_chargeMeter);
        RotateWeapon(_equippedWeaponObject.GetComponent<IWeapon>().GetStartRotation());
        _equippedWeapon.Equip();
    }

    public void UnEquipWeapon()
    {
        if (_equippedWeaponObject != null)
            _equippedWeapon.UnEquip();
    }

    public void RotateWeapon(float rotationDegrees)
    {
        if (_equippedWeaponObject == null)
            return;
        float currentRotation = _equippedWeaponObject.transform.localEulerAngles.x;
        float newRotation = Mathf.Clamp(currentRotation + rotationDegrees, _minrotation, _maxrotation);
        _equippedWeaponObject.transform.localEulerAngles = new Vector3(newRotation, 0, 0);
    }

    public void ResetShotStatus()
    {
        _hasShotThisTurn = false;
        _startedShootingThisTurn = false;
    }
}
