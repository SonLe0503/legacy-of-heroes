using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Health associatedHealth;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Feet")
        {
            associatedHealth.TakeDamage(damage);
            BouncePlayer();
        }
    }

    private void BouncePlayer()
    {
        PlayerController playerController = GameManager.instance.player.GetComponentInChildren<PlayerController>();
        if (playerController != null)
        {
            playerController.Bounce();
        }
    }
}
