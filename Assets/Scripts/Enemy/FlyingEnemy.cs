using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for "Flying Enemies" or waypoint mover objects acting as enemies
/// </summary>
[RequireComponent(typeof(WaypointMover))]
public class FlyingEnemy : EnemyBase
{
    public WaypointMover waypointMover = null;

    private SpriteRenderer spriteRenderer = null;

    protected override void Setup()
    {
        base.Setup();
        waypointMover = GetComponent<WaypointMover>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        CheckFlipSprite();
        SetStateInformation();
    }
    private void CheckFlipSprite()
    {
        if (waypointMover != null && spriteRenderer != null)
        {
            spriteRenderer.flipX = (Vector3.Dot(waypointMover.travelDirection, Vector3.right) < 0);
        }
    }

    protected virtual void SetStateInformation()
    {
        if (waypointMover != null)
        {
            if (waypointMover.stopped)
            {
                enemyState = EnemyState.Idle;
            }
            else
            {
                enemyState = EnemyState.Walking;
            }
        }
        else
        {
            enemyState = EnemyState.Idle;
        }
    }
}
