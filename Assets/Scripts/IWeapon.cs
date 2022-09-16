using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void PreShoot();
    public void Shoot();
    public float GetStartRotation();

    public void SetChargeMeter(ChargeMeter chargeMeter);
}
