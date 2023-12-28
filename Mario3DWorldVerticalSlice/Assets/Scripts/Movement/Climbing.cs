using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    Climable currentClimable;


    public void SetClimbingCenter(Climable climable)
    {
        currentClimable = climable;
    }
}
