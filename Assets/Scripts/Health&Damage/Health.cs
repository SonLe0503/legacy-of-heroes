using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int teamId = 0;

    public int defaultHealth = 1;
    public int maximumHealth = 1;
    public int currentHealth = 1;
    public float invincibilityTime = 3f;

    public bool useLives = false;
    public int currentLives = 3;
    public int maximumLives = 5;
    public float respawnWaitTime = 3f;

    void Start()
    {
        SetRespawnPoint(transform.position);
    }

    void Update()
    {
        InvincibilityCheck();
        RespawnCheck();
    }

    // The time to respawn at
    private float respawnTime;
    
    private void RespawnCheck()
    {
        if (respawnWaitTime != 0 && currentHealth <= 0 && currentLives > 0)
        {
            if (Time.time >= respawnTime)
            {
                Respawn();
            }
        }
    }

    // The specific game time when the health can be damaged again
    private float timeToBecomeDamagableAgain = 0;
    // Whether or not the health is invincible
    public bool isInvincible = false;

    private void InvincibilityCheck()
    {
        if (timeToBecomeDamagableAgain <= Time.time)
        {
            isInvincible = false;
        }
    }

    // The position that the health's gameobject will respawn at
    private Vector3 respawnPosition;

    public void SetRespawnPoint(Vector3 newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }

    void Respawn()
    {
        transform.position = respawnPosition;
        currentHealth = defaultHealth;
        GameManager.UpdateUIElements();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible || currentHealth <= 0)
        {
            return;
        }
        else
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation, null);
            }
            timeToBecomeDamagableAgain = Time.time + invincibilityTime;
            isInvincible = true;
            currentHealth -= damageAmount;
            CheckDeath();
        }
        GameManager.UpdateUIElements();
    }

    public void ReceiveHealing(int healingAmount)
    {
        currentHealth += healingAmount;
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
        CheckDeath();
        GameManager.UpdateUIElements();
    }

    public void AddLives(int bonusLives)
    {
        if (useLives)
        {
            currentLives += bonusLives;
            if (currentLives > maximumLives)
            {
                currentLives = maximumLives;
            }
            GameManager.UpdateUIElements();
        }
    }

    public GameObject deathEffect;
    public GameObject hitEffect;

    bool CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation, null);
        }

        if (useLives)
        {
            currentLives -= 1;
            if (currentLives > 0)
            {
                if (respawnWaitTime == 0)
                {
                    Respawn();
                }
                else
                {
                    respawnTime = Time.time + respawnWaitTime;
                } 
            }
            else
            {
                if (respawnWaitTime != 0)
                {
                    respawnTime = Time.time + respawnWaitTime;
                }
                else
                {
                    Destroy(this.gameObject);
                }
                GameOver();
            }
            
        }
        else
        {
            GameOver();
            Destroy(this.gameObject);
        }
        GameManager.UpdateUIElements();
    }

    public void GameOver()
    {
        if (GameManager.instance != null && gameObject.tag == "Player")
        {
            GameManager.instance.GameOver();
        }
    }
}
