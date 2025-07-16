using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
    [Header("Danh sách prefab vũ khí có thể random")]
    public GameObject[] gunPrefabs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        WeaponInventory inventory = other.GetComponent<WeaponInventory>();
        if (inventory == null || gunPrefabs.Length == 0) return;

       
        int randomIndex = Random.Range(0, gunPrefabs.Length);
        GameObject randomGun = gunPrefabs[randomIndex];

        
        if (inventory.WeaponCount < inventory.maxWeapons)
        {
            inventory.AddWeapon(randomGun);
        }
        else
        {
          
            WeaponReplaceUI ui = FindObjectOfType<WeaponReplaceUI>();
            if (ui != null)
            {
                ui.PromptReplaceWeapon(randomGun);
            }
        }

        
        Destroy(gameObject);
    }
}
