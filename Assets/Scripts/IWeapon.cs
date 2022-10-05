public interface IWeapon
{
    public void PreShoot();
    public void Shoot();
    public float GetStartRotation();

    public void SetChargeMeter(ChargeMeter chargeMeter);
    public void UnEquip();
    public void Equip();
}
