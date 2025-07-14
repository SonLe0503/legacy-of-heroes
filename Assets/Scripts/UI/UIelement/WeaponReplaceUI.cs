using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReplaceUI : MonoBehaviour
{
   [SerializeField] private GameObject panel;
    [SerializeField] private Button slot1Button;
    [SerializeField] private Button slot2Button;

    private GameObject newWeaponPrefab;
    private WeaponInventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<WeaponInventory>();

        slot1Button.onClick.AddListener(() => ReplaceWeapon(0));
        slot2Button.onClick.AddListener(() => ReplaceWeapon(1));

        panel.SetActive(false);
    }

    public void PromptReplaceWeapon(GameObject incomingWeapon)
    {
        newWeaponPrefab = incomingWeapon;
        panel.SetActive(true);
    }

    void ReplaceWeapon(int indexToReplace)
    {
        inventory.ReplaceWeapon(indexToReplace, newWeaponPrefab);
        panel.SetActive(false);
    }
}
