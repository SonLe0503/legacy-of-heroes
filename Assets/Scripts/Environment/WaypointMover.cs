using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 1f;
    public float waitTime = 3f;

    private float timeToStartMovingAgain = 0f;
    // Whether or not the waypoint mover is stopped
    [HideInInspector]
    public bool stopped = false;

    private Vector3 previousTarget;
    private Vector3 currentTarget;
    private int currentTargetIndex;
    [HideInInspector]
    public Vector3 travelDirection;

    void Start()
    {
        InitializeInformation();
    }

    void Update()
    {
        ProcessMovementState();
    }

    void ProcessMovementState()
    {
        if (stopped)
        {
            StartCheck();
        }
        else
        {
            Travel();
        }
    }

    void StartCheck()
    {
        if (Time.time >= timeToStartMovingAgain)
        {
            stopped = false;
            previousTarget = currentTarget;
            currentTargetIndex += 1;
            if (currentTargetIndex >= waypoints.Count)
            {
                currentTargetIndex = 0;
            }
            currentTarget = waypoints[currentTargetIndex].position;
            CalculateTravelInformation();
        }
    }

    void InitializeInformation()
    {
        BeginWait();
        previousTarget = this.transform.position;
        currentTargetIndex = 0;
        if (waypoints.Count > 0)
        {
            currentTarget = waypoints[0].position;
        }
        else
        {
            waypoints.Add(this.transform);
            currentTarget = previousTarget;
        }

        CalculateTravelInformation();
    }

    void CalculateTravelInformation()
    {
        travelDirection = (currentTarget - previousTarget).normalized;
    }

    void Travel()
    {
        transform.Translate(travelDirection * moveSpeed * Time.deltaTime);
        bool overX = false;
        bool overY = false;
        bool overZ = false;

        Vector3 directionFromCurrentPositionToTarget = currentTarget - transform.position;

        if ((directionFromCurrentPositionToTarget.x <= 0.0001 && directionFromCurrentPositionToTarget.x >= -0.0001) || Mathf.Sign(directionFromCurrentPositionToTarget.x) != Mathf.Sign(travelDirection.x))
        {
            overX = true;
            transform.position = new Vector3(currentTarget.x, transform.position.y, transform.position.z);
        }
        if ((directionFromCurrentPositionToTarget.y <= 0.0001 && directionFromCurrentPositionToTarget.y >= -0.0001) || Mathf.Sign(directionFromCurrentPositionToTarget.y) != Mathf.Sign(travelDirection.y))
        {
            overY = true;
            transform.position = new Vector3(transform.position.x, currentTarget.y, transform.position.z);
        }
        if ((directionFromCurrentPositionToTarget.z <= 0.0001 && directionFromCurrentPositionToTarget.z >= -0.0001) || Mathf.Sign(directionFromCurrentPositionToTarget.z) != Mathf.Sign(travelDirection.z))
        {
            overZ = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, currentTarget.z);
        }

        // If we are over the x, y, and z of our target we need to stop
        if (overX && overY && overZ)
        {
            BeginWait();
        }
    }

    void BeginWait()
    {
        stopped = true;
        timeToStartMovingAgain = Time.time + waitTime;
    }
}
