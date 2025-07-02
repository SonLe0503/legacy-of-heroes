using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChilder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MakeAChildOfAttachedTransform(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MakeAChildOfAttachedTransform(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DeChild(collision);
    }

    private void DeChild(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void MakeAChildOfAttachedTransform(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        } 
    }
}
