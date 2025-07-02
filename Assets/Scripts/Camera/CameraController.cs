using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [HideInInspector] private Camera playerCamera = null;

    public Transform target = null;

    public enum CameraStyles
    {
        Locked,
        Overhead,
        DistanceFollow,
        OffsetFollow,
        BetweenTargetAndMouse
    }

    public CameraStyles cameraMovementStyle = CameraStyles.Locked;

    public float maxDistanceFromTarget = 5.0f;
    public Vector2 cameraOffset = Vector2.zero;
    public float cameraZCoordinate = -10.0f;
    public float mouseTracking = 0.5f;
    public InputAction lookAction;

    void OnEnable()
    {
        lookAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
    }

    void Start()
    {
        InitilalSetup();
    }
    void InitilalSetup()
    {
        playerCamera = GetComponent<Camera>();
    }
    void Update()
    {
        SetCameraPosition();
    }
    private void SetCameraPosition()
    {
        if (target != null)
        {
            Vector3 targetPosition = GetTargetPosition();
            Vector3 mousePosition = GetPlayerMousePosition();
            Vector3 desiredCameraPosition = ComputeCameraPosition(targetPosition, mousePosition);

            transform.position = desiredCameraPosition;
        }      
    }

    public Vector3 GetTargetPosition()
    {
        if (target != null)
        {
            return target.position;
        }
        return transform.position;
    }

    public Vector3 GetPlayerMousePosition()
    {
        return playerCamera.ScreenToWorldPoint(lookAction.ReadValue<Vector2>());
    }

    public Vector3 ComputeCameraPosition(Vector3 targetPosition, Vector3 mousePosition)
    {
        Vector3 result = Vector3.zero;
        switch (cameraMovementStyle)
        {
            case CameraStyles.Locked:
                result = transform.position;
                break;
            case CameraStyles.Overhead:
                result = targetPosition;
                break;
            case CameraStyles.DistanceFollow:
                result = transform.position;
                if ((targetPosition - result).magnitude > maxDistanceFromTarget)
                {
                    result = targetPosition + (result - targetPosition).normalized * maxDistanceFromTarget;
                }
                break;
            case CameraStyles.OffsetFollow:
                result = targetPosition + (Vector3)cameraOffset;
                break;
            case CameraStyles.BetweenTargetAndMouse:
                Vector3 desiredPosition = Vector3.Lerp(targetPosition, mousePosition, mouseTracking);
                Vector3 difference = desiredPosition - targetPosition;
                difference = Vector3.ClampMagnitude(difference, maxDistanceFromTarget);
                result = targetPosition + difference;
                break;
        }
        result.z = cameraZCoordinate;
        return result;
    }
}
