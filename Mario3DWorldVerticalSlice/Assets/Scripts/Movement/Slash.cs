using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slash : MonoBehaviour
{
    [SerializeField] GroundCheck groundCheck; // Groundcheck, used to check ground and if grounded the player is able to slash

    [SerializeField] float slashTime = 1; // Time of a slash
    WaitForSeconds timer;
    private bool isSlashing = false;

    [SerializeField] AttackCollider slashCollider; // Collider of the slash hitbox
    [SerializeField] UnityEvent onSlash; // Invoked when slashing
    [SerializeField] UnityEvent onSlashEnd; // Invoked when slashing is ended

    public int gamepad = 0;



    // Set up
    private void Start()
    {
        timer = new WaitForSeconds(slashTime);
    }


    // processing
    private void Update()
    {
        if (groundCheck.isGrounded == true && Input.GetButtonDown("Dive" + gamepad.ToString()) && isSlashing == false)
        {
            StartCoroutine(SlashLoop());
        }
    }


    // Slashing corouting
    IEnumerator SlashLoop()
    {
        slashCollider.isActive = true;

        isSlashing = true;
        onSlash.Invoke();

        yield return timer;

        slashCollider.isActive = false;

        isSlashing = false;
        onSlashEnd.Invoke();

        yield break;
    }
}
