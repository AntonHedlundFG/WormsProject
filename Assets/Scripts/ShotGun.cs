using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotGun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _shotGunSlugGameObject;
    private WeaponAnimation _weaponAnimation;

    private float _startRotation = 90f;

    private int _slugCount = 50;
    private float _spreadDegrees = 5f;
    private float _slugForce = 5f;
    private float _fireDuration = 0.1f;
    private int _delayedFireCount = 10;

    private void Awake()
    {
        _weaponAnimation = GetComponent<WeaponAnimation>();
    }
    
    public void PreShoot()
    {

    }

    public void Shoot()
    {
        StartCoroutine("FireSlugs");
    }

    public float GetStartRotation()
    {
        return _startRotation;
    }

    public void SetChargeMeter(ChargeMeter chargeMeter)
    {
        
    }

    private IEnumerator FireSlugs()
    {
        for (int i = 0; i < _delayedFireCount; i++)
        {
            for (int j = 0; j < _slugCount / _delayedFireCount; j++)
            {
                FireSlug();
            }
            yield return new WaitForSeconds(_fireDuration / _delayedFireCount);
        }
    }

    private void FireSlug()
    {
        var slug = Instantiate(_shotGunSlugGameObject, transform.position, Quaternion.identity);
        slug.transform.eulerAngles = transform.eulerAngles + new Vector3(Random.Range(-_spreadDegrees, _spreadDegrees), Random.Range(-_spreadDegrees, _spreadDegrees), Random.Range(-_spreadDegrees, _spreadDegrees));
        slug.GetComponent<Rigidbody>().AddForce(slug.transform.up * _slugForce, ForceMode.Impulse);
    }

    public void UnEquip()
    {
        _weaponAnimation.PlayUnEquip();
        Destroy(gameObject, 1f);
    }

    public void Equip()
    {
        _weaponAnimation.PlayEquip();
    }
}
