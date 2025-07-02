using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    public int doorID = 0;
    public bool isOpen = false;
    public UnityEvent openEvent = new UnityEvent();
    public UnityEvent closeEvent = new UnityEvent();
    public GameObject doorOpenAndCloseEffect;
    public GameObject doorLockedEffect;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            AttemptToOpen();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AttemptToOpen();
        }
    }

    public void AttemptToOpen()
    {
        if (CheckPlayerHasKey() && !isOpen)
        {
            Open();
        }
        else if (doorLockedEffect && !isOpen)
        {
            Instantiate(doorLockedEffect, transform.position, Quaternion.identity, null);
        }
    }

    public bool CheckPlayerHasKey()
    {
        return KeyRing.HasKey(this);
    }

    protected virtual void Open()
    {
        isOpen = true;
        openEvent.Invoke();
        if (doorOpenAndCloseEffect)
        {
            Instantiate(doorOpenAndCloseEffect, transform.position, Quaternion.identity, null);
        }
    }

    protected virtual void Close()
    {
        isOpen = false;
        closeEvent.Invoke();
        if (doorOpenAndCloseEffect)
        {
            Instantiate(doorOpenAndCloseEffect, transform.position, Quaternion.identity, null);
        }
    }
}
