using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class intended to work with grid layout groups to create an image based health bar
/// </summary>
public class HealthDisplay : UIelement
{
    public GameObject healthDisplayImage = null;
    public GameObject numberDisplay = null;
    public int maximumNumberToDisplay = 3;

    public override void UpdateUI()
    {
        if (GameManager.instance != null && GameManager.instance.player != null)
        {
            Health playerHealth = GameManager.instance.player.GetComponent<Health>();
            if (playerHealth != null)
            {
                SetChildImageNumber(playerHealth.currentHealth);
            }
        }
    }

    private void SetChildImageNumber(int number)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if (healthDisplayImage != null)
        {
            if (maximumNumberToDisplay >= number)
            {
                for (int i = 0; i < number; i++)
                {
                    Instantiate(healthDisplayImage, transform);
                }
            }
            else
            {
                Instantiate(healthDisplayImage, transform);
                GameObject createdNumberDisp = Instantiate(numberDisplay, transform);
                createdNumberDisp.GetComponent<TMP_Text>().SetText(number.ToString());
            }
        }
    }
}
