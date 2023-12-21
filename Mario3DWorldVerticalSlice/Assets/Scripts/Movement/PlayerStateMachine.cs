using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Movement scripts
    Movement movement;
    Jumping jumping;
    Dive dive;
    Climbing climbing;

    Mover mover;

    // States of the player
    public enum PlayerState
    {
        NORMAL,
        CLIMBING,
        INACTIVE
    }



    // Current state of the player
    public PlayerState CURRENTSTATE
    {
        set
        {
            currentState = value;

            SetNewState();
        }
        get
        {
            return currentState;
        }
    }
    [SerializeField] private PlayerState currentState;



    // Setup
    private void Start()
    {
        movement = GetComponent<Movement>();
        jumping = GetComponent<Jumping>();
        dive = GetComponent<Dive>();
        climbing = GetComponent<Climbing>();

        mover = GetComponent<Mover>();

        SetNewState();
    }



    // Called when setting a new state
    void SetNewState()
    {
        mover.Xvelocity = 0;
        mover.Yvelocity = 0;
        mover.Zvelocity = 0;

        switch (currentState)
        {
            case PlayerState.NORMAL:
                movement.enabled = true;
                jumping.enabled = true;
                dive.enabled = true;

                climbing.enabled = false;

                break;


            case PlayerState.CLIMBING:
                movement.enabled = false;
                jumping.enabled = false;
                dive.enabled = false;

                climbing.enabled = true;

                break;


            case PlayerState.INACTIVE:
                movement.enabled = false;
                jumping.enabled = false;
                dive.enabled = false;

                climbing.enabled = false;

                break;
        }
    }
}
