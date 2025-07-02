using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class which handles player movement
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public GroundCheck groundCheck = null;
    public SpriteRenderer spriteRenderer = null;
    public Health playerHealth;

    // The rigidbody used to move the player (necessary for this component, so not made public)
    private Rigidbody2D playerRigidbody = null;

    #region Getters (primarily from other components)
    #region Directional facing
    /// <summary>
    /// Enum to help determine which direction the player is facing.
    /// </summary>
    public enum PlayerDirection
    {
        Right,
        Left
    }

    // Which way the player is facing right now
    public PlayerDirection facing
    {
        get
        {
            if (moveAction.ReadValue<Vector2>().x > 0)
            {
                return PlayerDirection.Right;
            }
            else if (moveAction.ReadValue<Vector2>().x < 0)
            {
                return PlayerDirection.Left;
            }
            else
            {
                if (spriteRenderer != null && spriteRenderer.flipX == true)
                    return PlayerDirection.Left;
                return PlayerDirection.Right;
            }
        }
    }
    #endregion

    // Whether this player is grounded false if no ground check component assigned
    public bool grounded
    {
        get
        {
            if (groundCheck != null)
            {
                return groundCheck.CheckGrounded();
            }
            else
            {
                return false;
            }
        }
    }

    #endregion

    public float movementSpeed = 4.0f;

    public float jumpPower = 10.0f;
    public int allowedJumps = 1;
    public float jumpDuration = 0.1f;
    public GameObject jumpEffect = null;
    public List<string> passThroughLayers = new List<string>();

    public InputAction moveAction;
    public InputAction jumpAction;

    // The number of times this player has jumped since being grounded
    private int timesJumped = 0;
    // Whether the player is in the middle of a jump right now
    private bool jumping = false;

    #region Player State Variables

    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Fall,
        Dead
    }

    public PlayerState state = PlayerState.Idle;
    #endregion

    #region Functions
    #region GameObject Functions

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Start()
    {
        SetupRigidbody();
    }

    private void LateUpdate()
    {
        ProcessInput();
        HandleSpriteDirection();
        DetermineState();
    }
    #endregion

    #region Input Handling and Movement Functions
    private void ProcessInput()
    {
        HandleMovementInput();
        HandleJumpInput();
    }

    private void HandleMovementInput()
    {
        Vector2 movementForce = Vector2.zero;
        if (Mathf.Abs(moveAction.ReadValue<Vector2>().x) > 0 && state != PlayerState.Dead)
        {
            movementForce = transform.right * movementSpeed * moveAction.ReadValue<Vector2>().x;
        }
        MovePlayer(movementForce);
    }

    private void MovePlayer(Vector2 movementForce)
    {
        if (grounded && !jumping)
        {
            float horizontalVelocity = movementForce.x;
            float verticalVelocity = 0;
            playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        else
        {
            float horizontalVelocity = movementForce.x;
            float verticalVelocity = playerRigidbody.velocity.y;
            playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        if (playerRigidbody.velocity.y > 0)
        {
            foreach (string layerName in passThroughLayers)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName), true);
            } 
        }
        else
        {
            foreach (string layerName in passThroughLayers)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName), false);
            }
        }
    }

    private void HandleJumpInput()
    {
        if (jumpAction.triggered)
        {
            StartCoroutine("Jump", 1.0f);
        }
    }

    private IEnumerator Jump(float powerMultiplier = 1.0f)
    {
        if (timesJumped < allowedJumps && state != PlayerState.Dead)
        {
            jumping = true;
            float time = 0;
            SpawnJumpEffect();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(transform.up * jumpPower * powerMultiplier, ForceMode2D.Impulse);
            timesJumped++;
            while (time < jumpDuration)
            {
                yield return null;
                time += Time.deltaTime;
            }
            jumping = false;
        }
    }

    private void SpawnJumpEffect()
    {
        if (jumpEffect != null)
        {
            Instantiate(jumpEffect, transform.position, transform.rotation, null);
        }
    }

    public void Bounce()
    {
        timesJumped = 0;
        if (jumpAction.ReadValue<float>() >= 1)
        {
            StartCoroutine("Jump", 1.5f);
        }
        else
        {
            StartCoroutine("Jump", 1.0f);
        }
    }

    private void HandleSpriteDirection()
    {
        if (spriteRenderer != null)
        {
            if (facing == PlayerDirection.Left)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    #endregion

    #region State Functions

    private PlayerState GetState()
    {
        return state;
    }

    private void SetState(PlayerState newState)
    {
        state = newState;
    }

    private void DetermineState()
    {
        if (playerHealth.currentHealth <= 0)
        {
            SetState(PlayerState.Dead);
        }
        else if (grounded)
        {
            if (playerRigidbody.velocity.magnitude > 0)
            {
                SetState(PlayerState.Walk);
            }
            else
            {
                SetState(PlayerState.Idle);
            }
            if (!jumping)
            {
                timesJumped = 0;
            }
        }
        else
        {
            if (jumping)
            {
                SetState(PlayerState.Jump);
            }
            else
            {
                SetState(PlayerState.Fall);
            }
        }
    }
    #endregion

    private void SetupRigidbody()
    {
        if (playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
    }
    #endregion
}
