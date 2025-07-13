using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }
    
    void MoveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    
    // Handle collision with enemies and walls
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit an enemy (has Health component with teamId 1)
        Health enemyHealth = collision.GetComponent<Health>();
        if (enemyHealth != null && enemyHealth.teamId == 1)
        {
            // Deal 1 damage to the enemy
            enemyHealth.TakeDamage(1);
            // Destroy the bullet
            Destroy(gameObject);
            return;
        }
        
        // Check if we hit a wall (on layer 6 - Platforms/Environment)
        if (collision.gameObject.layer == 6)
        {
            // Destroy the bullet when hitting wall
            Destroy(gameObject);
            return;
        }
        
        // Check other possible wall layers
        if (collision.gameObject.layer == 7) // Environment layer
        {
            // Destroy the bullet when hitting wall
            Destroy(gameObject);
            return;
        }
    }
    
    // Also handle regular collisions (non-trigger)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we hit an enemy (has Health component with teamId 1)
        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        if (enemyHealth != null && enemyHealth.teamId == 1)
        {
            // Deal 1 damage to the enemy
            enemyHealth.TakeDamage(1);
            // Destroy the bullet
            Destroy(gameObject);
            return;
        }
        
        // Check if we hit a wall (on layer 6 - Platforms/Environment)
        if (collision.gameObject.layer == 6)
        {
            // Destroy the bullet when hitting wall
            Destroy(gameObject);
            return;
        }
        
        // Check other possible wall layers
        if (collision.gameObject.layer == 7) // Environment layer
        {
            // Destroy the bullet when hitting wall
            Destroy(gameObject);
            return;
        }
    }
}
