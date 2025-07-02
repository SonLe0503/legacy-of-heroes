using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component on gameobjects with colliders which determines if there is
/// a collider overlapping them which is on a specific layer.
/// Used to check for ground.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayers = new LayerMask();
    public Collider2D groundCheckCollider = null;

    public GameObject landingEffect;

    // Whether or not the player was grounded last check
    [HideInInspector]
    public bool groundedLastCheck = false;

    private void Start()
    {
        // When this component starts up, ensure that the collider is not null, if possible
        GetCollider();
    }

    public void GetCollider()
    {
        if (groundCheckCollider == null)
        {
            groundCheckCollider = gameObject.GetComponent<Collider2D>();
        }
    }

    public bool CheckGrounded()
    {
        // Ensure the collider is assigned
        if (groundCheckCollider == null)
        {
            GetCollider();
        }

        // Find the colliders that overlap this one
        Collider2D[] overlaps = new Collider2D[5];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = groundLayers;
        groundCheckCollider.OverlapCollider(contactFilter, overlaps);

        // Check if one of the overlapping colliders is on the "ground" layer
        foreach (Collider2D overlapCollider in overlaps)
        {
            if (overlapCollider != null)
            {
                int match = contactFilter.layerMask.value & (int)Mathf.Pow(2, overlapCollider.gameObject.layer);
                if (match > 0)
                {
                    if (landingEffect && !groundedLastCheck)
                    {
                        Instantiate(landingEffect, transform.position, Quaternion.identity, null);
                    }
                    groundedLastCheck = true;
                    return true;
                }
            }
        }
        groundedLastCheck = false;
        return false;
    }
}
