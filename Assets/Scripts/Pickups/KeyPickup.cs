using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Pickup
{
    public int keyID = 0;

    public override void DoOnPickup(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            KeyRing.AddKey(keyID);
        }
        base.DoOnPickup(collision);
    }
}
