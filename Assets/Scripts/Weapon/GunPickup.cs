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
            PlayerGunController playerGun = other.GetComponent<PlayerGunController>();
            if (playerGun != null)
            {
                playerGun.EquipGun(gunPrefab);
                Destroy(gameObject); // Xóa súng trong scene sau khi nhặt
            }
        }
    }
}
