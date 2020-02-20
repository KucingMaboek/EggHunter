public interface IWeaponControl
{
    void TriggerOnHold();
    void TriggerOnRelease();
    void Fire();
    void Reload();
    void WeaponCooldown();
    void AddAmmo(int ammo);
    int GetStockAmmo();
    void ShowAmmoOnUI();
    void CheckForRootParent();
    void InitializeSP();
}
