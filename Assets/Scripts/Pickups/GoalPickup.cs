using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for pickups which end the level
/// </summary>
public class GoalPickup : Pickup
{
    public override void DoOnPickup(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.LevelCleared();
            }
        }
        base.DoOnPickup(collision);
    }
}
