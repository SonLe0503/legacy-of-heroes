using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject pickUpEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DoOnPickup(collision);
    }

    public virtual void DoOnPickup(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (pickUpEffect != null)
            {
                Instantiate(pickUpEffect, transform.position, Quaternion.identity, null);
            }
            Destroy(this.gameObject);
        }
    }
}
