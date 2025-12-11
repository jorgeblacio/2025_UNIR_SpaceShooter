using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Weapon[] availableWeapons;
    
    private void Start()
    {
        if (availableWeapons != null && availableWeapons.Length > 0)
        {
            SwitchWeapon(0);
        }
    }
    
    public void Fire()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Fire();
        }
    }
    
    public void SwitchWeapon(int index)
    {
        if (availableWeapons == null || index < 0 || index >= availableWeapons.Length)
            return;
        
        foreach (Weapon weapon in availableWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
        
        currentWeapon = availableWeapons[index];
        currentWeapon.gameObject.SetActive(true);
    }
}