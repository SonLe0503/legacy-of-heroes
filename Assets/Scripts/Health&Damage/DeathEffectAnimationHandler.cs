using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathEffectAnimationHandler : MonoBehaviour
{
    void Start()
    {
        SetIsDead();
    }

    private void SetIsDead()
    {
        GetComponent<Animator>().SetTrigger("isDead");
    }
}
