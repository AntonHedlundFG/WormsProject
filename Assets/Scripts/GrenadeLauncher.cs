using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _grenadeGameObject;

    private GameObject _grenade;
    private float _startRotation = 45f;

    private float _minForce = 1f;
    private float _maxForce = 2.5f;

    public void Shoot()
    {
        Shoot(_maxForce);
    }

    public void Shoot(float force)
    {
        _grenade = Instantiate(_grenadeGameObject, transform.position, Quaternion.identity);
        Rigidbody grenadeRigidbody = _grenade.GetComponent<Rigidbody>();
        grenadeRigidbody.AddForce(transform.up * force, ForceMode.Impulse);
    }

    public float GetStartRotation()
    {
        return _startRotation;
    }
}
