using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPrefab; // Prefab gắn vào Player khi nhặt

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponInventory inventory = other.GetComponent<WeaponInventory>();
        if (inventory == null) return;

        if (inventory.WeaponCount < inventory.maxWeapons)
        {
            inventory.AddWeapon(gunPrefab);
        }
        else
        {
            // Gọi UI thay thế
            WeaponReplaceUI ui = FindObjectOfType<WeaponReplaceUI>();
            if (ui != null)
            {
                ui.PromptReplaceWeapon(gunPrefab);
            }
        }

        Destroy(gameObject); // Xóa khỏi scene (tuỳ bạn, có thể hoãn lại)
        }
    }
}
