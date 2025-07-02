using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which handles enemy state translation to Animations
/// </summary>
public class EnemyAnimator : MonoBehaviour
{
    public EnemyBase enemyComponent = null;
    public Animator enemyAnimator = null;

    public string IdleAnimatorParameter = "isIdle";
    public string MovingAnimatorParameter = "isWalking";
    public string DeadAnimatorParameter = "isDead";

    private void Start()
    {
        SetAnimatorState();
    }

    private void Update()
    {
        SetAnimatorState();
    }
    private void SetAnimatorState()
    {
        if (enemyComponent != null && enemyAnimator != null)
        {
            // Handle idle state
            if (enemyComponent.enemyState == EnemyBase.EnemyState.Idle)
            {
                enemyAnimator.SetBool(IdleAnimatorParameter, true);
            }
            else
            {
                enemyAnimator.SetBool(IdleAnimatorParameter, false);
            }

            // Handle moving state
            if (enemyComponent.enemyState == EnemyBase.EnemyState.Walking)
            {
                enemyAnimator.SetBool(MovingAnimatorParameter, true);
            }
            else
            {
                enemyAnimator.SetBool(MovingAnimatorParameter, false);
            }

            // Handle dead state
            if (enemyComponent.enemyState == EnemyBase.EnemyState.Dead)
            {
                enemyAnimator.SetBool(DeadAnimatorParameter, true);
            }
            else
            {
                enemyAnimator.SetBool(DeadAnimatorParameter, false);
            }
        }
    }
}
