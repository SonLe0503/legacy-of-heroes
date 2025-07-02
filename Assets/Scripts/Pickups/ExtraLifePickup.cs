using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePickup : Pickup
{
    [Header("Extra Life Settings")]
    [Tooltip("How many Lives to give")]
    public int extraLives = 1;

    public override void DoOnPickup(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.AddLives(extraLives);
        }
        base.DoOnPickup(collision);
    }
}
