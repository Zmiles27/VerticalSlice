using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // gebruikt Text mesh pro
using System.Globalization;
using System;

public class Timer : MonoBehaviour
{
    [Header("component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")] // limits the timer to preference 
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")] //set the amount of decimals
    public bool HasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>(); 

    // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00 ");
    }

    // Update is called once per frame
    void Update()
    { //tell our current time to increase or decrease based on the flag we set
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime; // if countdown is false (countDown ?) currentTime -= will run but if its true currentTime += will count 

        //send a check to see if we have a limit enabeld to see if we are below or above the limit
        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit))) // if hasLimit is checked we want to see if were decreasing or going below the limit and if were increasing then we go above our limit so basically we have a limit    // for counting down and (&&) our current time is less then or equal (<=) to our time limit or (||) were not !counting down were counting up and (&&) our current time is greateror equal (>=) than our timerLimit 
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false; // stops the timer from running
        }

        SetTimerText();
    }

    private void SetTimerText() 
    {
        timerText.text = HasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
}

public enum TimerFormats 
{
    Whole,
    TenthDecimal,
    HundrethsDecimal, 
}
