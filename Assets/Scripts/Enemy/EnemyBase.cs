using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    public float moveSpeed = 2f;

    public enum EnemyState { Walking, Dead, Idle }

    public EnemyState enemyState;

    protected virtual void Start()
    {
        Setup();
    }

    protected virtual void Update()
    {
        Vector3 movement = GetMovement();
        MoveEnemy(movement);
    }

    protected virtual void Setup()
    {

    }

    protected virtual Vector3 GetMovement()
    {
        return Vector3.zero;
    }

    protected virtual void MoveEnemy(Vector3 movement)
    {
        transform.position = transform.position + movement;
    }
}
