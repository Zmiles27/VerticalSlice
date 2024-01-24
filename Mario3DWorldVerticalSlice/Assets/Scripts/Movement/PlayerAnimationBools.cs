using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBools : MonoBehaviour
{
    // This is a wrapper script used for updating booleans with using unity events

    Animator animator; // The objects animator

    const string isJumpingName = "isJumping";
    const string isClimbingName = "isClimbing";
    const string isIdleName = "isIdle";
    const string isAirName = "isAir";

    
    // Set up
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();    
    }


    // Setting bools
    public void SetIsJumping(bool isJumping)
    {
        animator.SetBool(isJumpingName, isJumping);
    }

    public void SetIsClimbing(bool isClimbing)
    {
        animator.SetBool(isClimbingName, isClimbing);
    }

    public void SetIsIdle(bool isIdle)
    {
        animator.SetBool(isIdleName, isIdle);
    }

    public void SetIsAir(bool isAir)
    {
        animator.SetBool(isAirName, isAir);
    }

    // - Example
    //public void SetIsRunning(bool isRunning)
    //{
    //    animator.SetBool(isRunningName, isRunning);
    //}
}
