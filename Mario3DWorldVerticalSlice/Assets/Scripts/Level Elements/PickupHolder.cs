using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHolder : MonoBehaviour
{
    // Be sure to add the "PickupManager" tag to the object this script is on!

    public int coin = 0; // Current number of collected coins
    public int score = 0; // Current number of score


    // Add score and coins
    public void AddValues(int newCoins, int newScore)
    {
        coin += newCoins;
        score += newScore;
    }
}
