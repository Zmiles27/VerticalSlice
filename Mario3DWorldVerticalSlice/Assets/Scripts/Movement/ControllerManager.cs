using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControllerManager : MonoBehaviour
{
    [SerializeField][Range(0, 3)] public int gamepad = 0;

    Movement movement;
    Jumping jumping;



    // Setup
    private void Start()
    {
        movement = GetComponent<Movement>();
        jumping = GetComponent<Jumping>();

        SetGamepad(gamepad);
    }



    public void SetGamepad(int newGamepad)
    {
        gamepad = newGamepad;

        movement.gamepad = gamepad;
        jumping.gamepad = gamepad;
    }
}


