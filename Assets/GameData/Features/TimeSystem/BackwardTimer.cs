using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EachSecond;

public class BackwardTimer : MonoBehaviour {
    
    public TMP_Text timerLabel = null;
    private DateTime endTime = DateTime.MinValue;
    private DateTime timeStart = DateTime.MinValue;
    

    public readonly SignalVoid signalOnTimeUp = new SignalVoid();
    public readonly SignalVoid signalOnTimerUpdate = new SignalVoid();
    public readonly SignalVoid signalAfterCustomTimeTillEnd = new SignalVoid();
    
    bool isTimeUpCalled = false;



    void OnEnable() {
        EachSecond.Instance.signalEachSecond.AddListener(UpdateTimer);
    }

    void OnDisable() {
        EachSecond.Instance.signalEachSecond.RemoveListener(UpdateTimer);
    }

    void UpdateTimer()
    {
        if (endTime.Equals(DateTime.MinValue))
        {
            return;
        }

        if (timerLabel == null) 
        {
            return;
        }

        if (isTimeUpCalled)
        {
            return;
        }


        TimeSpan timeLeft = (endTime - DateTime.Now);
        
        
        if (timeLeft.TotalMilliseconds > 0)
        {
            timerLabel.text = formatTime((long)timeLeft.TotalMilliseconds);
        } 
        else
        {
            isTimeUpCalled = true;
            signalOnTimeUp.Invoke();
        }


        signalOnTimerUpdate.Invoke();
    }





    public void StartTimer(DateTime end_time)
    {
        if (end_time == endTime) 
        {
            return;
        }

        timeStart = DateTime.Now;
        endTime = end_time;

        isTimeUpCalled = false;
        UpdateTimer();
    }

    public void stopTimer() {
        isTimeUpCalled = true;
        endTime = DateTime.MinValue;
    }





















    public static string formatTime(long totalMilliseconds) {
        if (totalMilliseconds < 0) {
            totalMilliseconds = 0;
        }

        long seconds = totalMilliseconds / 1000;

        long minutes = seconds / 60;
        seconds = seconds % 60;

        long hours = minutes / 60;
        minutes = minutes % 60;

        long days = hours / 24;
        hours = hours % 24;

        string res;

        if (days != 0) {
        
        if (hours != 0) {
            res = days + "d" + " " + hours + "h";
        } else {
            res = days + "d";
        } 

        } else if (hours != 0) {
        
        if (minutes != 0) {
            res = hours + "h" + " " + minutes + "m";
        } else {
            res = hours + "h";
        }

        } else if (minutes != 0) {
        
        if (seconds != 0) {
            res = minutes + "m" + " " + seconds + "s";
        } else {
            res = minutes + "m";
        }

        } else {
            res = seconds + "s";
        }

        return res;
    }
}