using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] weaponSlots; // Kéo 2 Image UI vào đây
    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Color unselectedColor = new Color(1, 1, 1, 0.5f);

    private WeaponInventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<WeaponInventory>();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (i < inventory.WeaponCount)
            {
                var gun = inventory.GetWeapon(i).GetComponent<Gun>();
                weaponSlots[i].sprite = gun.gunIcon;
                weaponSlots[i].color = (i == inventory.CurrentWeaponIndex) ? selectedColor : unselectedColor;
                
            }
            else
            {
                weaponSlots[i].sprite = emptySlotSprite;
                weaponSlots[i].color = unselectedColor;
            }
        }
    }
}
