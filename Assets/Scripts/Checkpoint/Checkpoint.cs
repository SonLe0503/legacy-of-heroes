using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles setting the checkpoint for the player to respawn at
/// </summary>
public class Checkpoint : MonoBehaviour
{
    public Transform respawnLocation;
    public Animator checkpointAnimator = null;
    public string animatorActiveParameter = "isActive";
    public GameObject checkpointActivationEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.SetRespawnPoint(respawnLocation.position);

            // Reset the last checkpoint if it exists
            if (CheckpointTracker.currentCheckpoint != null)
            {
                CheckpointTracker.currentCheckpoint.checkpointAnimator.SetBool(animatorActiveParameter, false);
            }

            if (CheckpointTracker.currentCheckpoint != this && checkpointActivationEffect != null)
            {
                Instantiate(checkpointActivationEffect, transform.position, Quaternion.identity, null);
            }

            // Set current checkpoint to this and set up its animation
            CheckpointTracker.currentCheckpoint = this;
            checkpointAnimator.SetBool(animatorActiveParameter, true);
        }
    }
}
