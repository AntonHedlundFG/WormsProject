using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Shoot();
    public void Shoot(float force);
    public float GetStartRotation();
}
