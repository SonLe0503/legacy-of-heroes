using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyBase-derived enemy class which walks in a direction until it hits a wall
/// </summary>
public class WalkingEnemy : EnemyBase
{
    public enum WalkDirections { Right, Left, None }
    
    public GroundCheck wallTestLeft;
    public GroundCheck wallTestRight;
    public GroundCheck leftEdge;
    public GroundCheck rightEdge;

    public WalkDirections walkDirection = WalkDirections.None;
    public bool willTurnAroundAtEdge = false;

    // The sprite renderer for this enemy
    private SpriteRenderer spriteRenderer = null;

    protected override void Setup()
    {
        base.Setup();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        DetermineWalkDirection();
        base.Update();
    }

    protected override Vector3 GetMovement()
    {
        // Determine the movement vector based on the direction that the enemy is currently moving in
        switch (walkDirection)
        {
            case WalkDirections.None:
                enemyState = EnemyState.Idle;
                return Vector3.zero;
            case WalkDirections.Left:
                enemyState = EnemyState.Walking;
                return Vector3.left * moveSpeed * Time.deltaTime;
            case WalkDirections.Right:
                enemyState = EnemyState.Walking;
                return Vector3.right * moveSpeed * Time.deltaTime;
            default:
                return base.GetMovement();
        }
    }

    private void DetermineWalkDirection()
    {
        if (TestWall() || GetIsNearEdge())
        {
            TurnAround();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = (walkDirection == WalkDirections.Right);
        }
    }

    private void TurnAround()
    {
        if (walkDirection == WalkDirections.Left)
        {
            walkDirection = WalkDirections.Right;
        }
        else if (walkDirection == WalkDirections.Right)
        {
            walkDirection = WalkDirections.Left;
        }
    }

    protected virtual bool TestWall()
    {
        switch (walkDirection)
        {
            case WalkDirections.Left:
                if (wallTestLeft != null)
                {
                    return wallTestLeft.CheckGrounded();
                }
                break;
            case WalkDirections.Right:
                if (wallTestRight != null)
                {
                    return wallTestRight.CheckGrounded();
                }
                break;
        }
        return false;
    }

    private bool GetIsNearEdge()
    {
        GroundCheck check = null;
        if (walkDirection == WalkDirections.Left)
        {
            check = leftEdge; 
        }
        else if (walkDirection == WalkDirections.Right)
        {
            check = rightEdge;
        }
        if (check != null && !check.CheckGrounded())
        {
            return willTurnAroundAtEdge;
        }
        return false;
    }
}
