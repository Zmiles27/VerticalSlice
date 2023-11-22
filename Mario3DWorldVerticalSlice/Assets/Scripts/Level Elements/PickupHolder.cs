using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHolder : MonoBehaviour
{
    public int coin = 0;
    public int score = 0;


    public void AddValues(int newCoins, int newScore)
    {
        coin += newCoins;
        score += newScore;
    }
}
