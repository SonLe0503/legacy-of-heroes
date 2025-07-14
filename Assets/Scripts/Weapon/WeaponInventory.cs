using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public int WeaponCount => weaponSlots.Count;
public int CurrentWeaponIndex => currentWeaponIndex;
public GameObject GetWeapon(int index) => weaponSlots[index];
    [SerializeField] private Transform gunHoldPoint;
    private List<GameObject> weaponSlots = new List<GameObject>();
    private int currentWeaponIndex = -1;

    public int maxWeapons = 2;

    public void AddWeapon(GameObject gunPrefab)
    {
        if (weaponSlots.Count >= maxWeapons)
        {
            Debug.Log("Đã đầy kho vũ khí!");
            return;
        }

        GameObject newGun = Instantiate(gunPrefab, gunHoldPoint.position, gunHoldPoint.rotation, gunHoldPoint);
        newGun.GetComponent<Gun>().isEquipped = false;
        newGun.SetActive(false); // Ẩn súng cho đến khi được chọn
        weaponSlots.Add(newGun);

        // Nếu đây là súng đầu tiên, chọn luôn
        if (weaponSlots.Count == 1)
        {
            SwitchWeapon(0);
        }
    }
    public void ReplaceWeapon(int index, GameObject newGunPrefab)
{
    if (index < 0 || index >= weaponSlots.Count) return;

    Destroy(weaponSlots[index]);
    weaponSlots[index] = Instantiate(newGunPrefab, gunHoldPoint.position, gunHoldPoint.rotation, gunHoldPoint);
    weaponSlots[index].SetActive(false);
    weaponSlots[index].GetComponent<Gun>().isEquipped = false;

    // Nếu slot vừa bị thay là slot đang dùng → tự động chọn lại slot 0
    if (index == currentWeaponIndex)
    {
        SwitchWeapon(0);
    }
}

    void Update()
    {
        // Nhấn phím 1 hoặc 2 để chuyển vũ khí
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weaponSlots.Count >= 1)
                SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponSlots.Count >= 2)
                SwitchWeapon(1);
        }
    }

    void SwitchWeapon(int index)
    {
        if (index == currentWeaponIndex) return;

        // Tắt súng hiện tại
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weaponSlots.Count)
        {
            weaponSlots[currentWeaponIndex].SetActive(false);
            weaponSlots[currentWeaponIndex].GetComponent<Gun>().isEquipped = false;
        }

        // Bật súng mới
        currentWeaponIndex = index;
        weaponSlots[currentWeaponIndex].SetActive(true);
        weaponSlots[currentWeaponIndex].GetComponent<Gun>().isEquipped = true;
    }
}
