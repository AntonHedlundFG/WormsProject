using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _grenadeGameObject;
    [SerializeField] private GameObject _chargeMeterObject;

    private GameObject _grenade;
    private ChargeMeter _chargeMeter;

    private float _startRotation = 45f;

    private float _minForce = 1f;
    private float _maxForce = 2.5f;

    [SerializeField] private float _curForce;
    private int _charging;
    private float _chargeTime = 1f;


    public void Start()
    {
        Init();
    }
    public void Update()
    {
        if (_charging != 0)
        {
            ChangeCharge();
            _chargeMeter.UpdateBar((_curForce - _minForce) / (_maxForce - _minForce) );
        }
        ShowCharge();
    }

    private void Init()
    {
        
    }

    public void SetChargeMeter(ChargeMeter chargeMeter)
    {
        _chargeMeter = chargeMeter;
    }

    public void Shoot()
    {
        Shoot(_curForce);
    }

    public void Shoot(float force)
    {
        _grenade = Instantiate(_grenadeGameObject, transform.position, Quaternion.identity);
        Rigidbody grenadeRigidbody = _grenade.GetComponent<Rigidbody>();
        grenadeRigidbody.AddForce(transform.up * force, ForceMode.Impulse);
        _charging = 0;
        _chargeMeter.SetActive(false);
    }

    public void PreShoot()
    {
        _curForce = _minForce;
        _charging = 1;
        _chargeMeter.SetActive(true);
    }

    public float GetStartRotation()
    {
        return _startRotation;
    }

    private void ChangeCharge()
    {
        _curForce += _charging * Time.deltaTime * (_maxForce - _minForce) / _chargeTime;
        if (_curForce > _maxForce)
        {
            _curForce = _maxForce;
            _charging = -1;
        }
        if (_curForce < _minForce)
        {
            _curForce = _minForce;
            _charging = 1;
        }
    }

    private void ShowCharge()
    {

    }
    
}
