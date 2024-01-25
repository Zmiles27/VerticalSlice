using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControllerManager : MonoBehaviour
{
    [SerializeField][Range(0, 4)] public int gamepad = 0; // Gamepad 4 should be the keyboard, for testing purposes

    Movement movement;
    Jumping jumping;
    Dive dive;
    Slash slash;
    Climbing climbing;



    // Setup
    private void Start()
    {
        movement = GetComponent<Movement>();
        jumping = GetComponent<Jumping>();
        dive = GetComponent<Dive>();
        slash = GetComponent<Slash>();
        climbing = GetComponent<Climbing>();

        SetGamepad(gamepad);
    }



    public void SetGamepad(int newGamepad)
    {
        gamepad = newGamepad;

        movement.gamepad = gamepad;
        jumping.gamepad = gamepad;
        dive.gamepad = gamepad;
        slash.gamepad = gamepad;
        climbing.gamepad = gamepad;
    }
}


