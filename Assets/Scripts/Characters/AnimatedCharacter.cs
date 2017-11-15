using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void SetMovement(float moveZ)
    {
        _animator.SetFloat("Input Z", moveZ);
    }

    public void Trigger( AnimationTrigger trigger)
    {
        _animator.SetTrigger(trigger.ToString() + " Trigger");
    }

    public void Die()
    {
        _animator.SetBool("Dead", true);
        Trigger(AnimationTrigger.Die);
    }

}

public enum AnimationTrigger
{
    Attack,
    Special1,
    Hit,
    Die,
    Victory,
}


