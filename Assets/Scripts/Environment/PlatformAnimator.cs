using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointMover))]
[RequireComponent(typeof(Animator))]
public class PlatformAnimator : MonoBehaviour
{
    WaypointMover mover = null;
    Animator animator = null;

    private void Awake()
    {
        // Get the mover and animator from the game object this script is attached to
        mover = GetComponent<WaypointMover>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Set the isMoving bool of the animator according to the waypoint mover's state
        if (mover != null && animator != null)
        {
            animator.SetBool("isMoving", !mover.stopped);
        }
    }
}
