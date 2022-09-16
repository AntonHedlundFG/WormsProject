using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private GameObject[] _weapons;

    private GameObject _equippedWeaponObject;
    private IWeapon _equippedWeapon;
    private WormHandler _wormHandler;
    private TurnHandler _turnHandler;


    private bool _hasShotThisTurn = false;
    private float _endTurnDelay = 5f;


    void Start()
    {
        Init();
    }
    void Update()
    {
        if (_wormHandler.IsActive() && _equippedWeaponObject != null && Input.GetKeyDown(KeyCode.Return) && !_hasShotThisTurn)
        {
            _equippedWeapon.Shoot();
            _hasShotThisTurn = true;
            _turnHandler.NextActiveWorm(_endTurnDelay);
        }
    }

    void Init()
    {
        _wormHandler = GetComponentInParent<WormHandler>();
        _turnHandler = TurnHandler.Instance;
    }
    public void EquipWeapon(int weaponID)
    {
        UnEquipWeapon();
        _equippedWeaponObject = Instantiate(_weapons[weaponID], _weaponHolder.transform.position, Quaternion.identity, _weaponHolder.transform);
        _equippedWeapon = _equippedWeaponObject.GetComponent<IWeapon>();
        RotateWeapon(_equippedWeaponObject.GetComponent<IWeapon>().GetStartRotation());

    }

    public void UnEquipWeapon()
    {
        Destroy(_equippedWeaponObject);
    }

    public void RotateWeapon(float rotationDegrees)
    {
        _equippedWeaponObject.transform.Rotate(transform.right, rotationDegrees);
    }

    public void ResetShotStatus()
    {
        _hasShotThisTurn = false;
    }
}
