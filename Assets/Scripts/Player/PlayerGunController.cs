using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    public Transform gunHoldPoint; // Gắn FirePos hoặc GunHoldPoint ở đây
    private GameObject currentGun;
    

    public void EquipGun(GameObject gunPrefab)
    {
        if (currentGun != null)
            Destroy(currentGun);

        currentGun = Instantiate(gunPrefab, gunHoldPoint.position, gunHoldPoint.rotation, gunHoldPoint);
        Gun gunScript = currentGun.GetComponent<Gun>();
        if (gunScript != null)
        {
            gunScript.isEquipped = true;
        }
    }
}
