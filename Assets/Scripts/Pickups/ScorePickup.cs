using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{
    public int scoreAmount = 1;

    public override void DoOnPickup(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            GameManager.AddScore(scoreAmount);
        }
        base.DoOnPickup(collision);
    }
}
