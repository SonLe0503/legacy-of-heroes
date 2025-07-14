using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isEquipped = false;
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 24;
    public int currentAmmo;
    private GunType gunType;
    private bool canShoot = true;
    // Start is called before the first frame update
    [SerializeField] private float doubleShotSpacing = 0.3f;
    public Sprite gunIcon;

    public enum GunType
    {
        Single,     // Gun tag - 1 shot then reload
        Double,     // Gun2 tag - 2 shots then reload  
        Magazine    // Gun3 tag - 24 shots then reload
    }
    void Start()
    {
        currentAmmo = maxAmmo;
        DetermineGunType();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEquipped) return;
        RotateGun();
        Shot();
        Reload();
    }
    void DetermineGunType()
    {
        string tag = gameObject.tag;
        switch (tag)
        {
            case "Gun":
                gunType = GunType.Single;
                maxAmmo = 1;
                currentAmmo = 1;
                break;
            case "Gun2":
                gunType = GunType.Double;
                maxAmmo = 2;
                currentAmmo = 2;
                break;
            case "Gun3":
                gunType = GunType.Magazine;
                maxAmmo = 24;
                currentAmmo = 24;
                break;
            default:
                gunType = GunType.Magazine; // Default fallback
                break;
        }
    }
    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
    void Shot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot && canShoot)
        {
            nextShot = Time.time + shotDelay;
            
            switch (gunType)
            {
                case GunType.Single:
                    // Single shot - 1 bullet
                    Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
                    currentAmmo--;
                    canShoot = false; // Disable shooting until reload
                    break;
                    
                case GunType.Double:
                    // Double shot - 2 bullets with spacing
                    if (currentAmmo >= 2)
                    {
                        // Tính toán vị trí 2 viên đạn dựa trên hướng súng
                        Vector3 rightDirection = firePos.right;
                        Vector3 leftPos = firePos.position + rightDirection * doubleShotSpacing;
                        Vector3 rightPos = firePos.position - rightDirection * doubleShotSpacing;
                        
                        // Tạo 2 viên đạn với khoảng cách
                        Instantiate(bulletPrefabs, leftPos, firePos.rotation);
                        Instantiate(bulletPrefabs, rightPos, firePos.rotation);
                        
                        currentAmmo -= 2;
                        canShoot = false; // Disable shooting until reload
                    }
                    break;
                    
                case GunType.Magazine:
                    // Magazine - single bullet, can shoot multiple times
                    Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
                    currentAmmo--;
                    break;
            }
        }
    }
    void Reload()
    {
        if(Input.GetMouseButtonDown(1))
        {
            switch (gunType)
            {
                case GunType.Single:
                case GunType.Double:
                    // Reload to full capacity
                    currentAmmo = maxAmmo;
                    canShoot = true; // Re-enable shooting
                    break;
                    
                case GunType.Magazine:
                    // Only reload if not full
                    if (currentAmmo < maxAmmo)
                    {
                        currentAmmo = maxAmmo;
                    }
                    break;
            }
        }
    }
}
