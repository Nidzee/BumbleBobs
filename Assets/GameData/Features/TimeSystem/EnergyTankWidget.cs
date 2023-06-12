using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnergyTankWidget : MonoBehaviour
{
    [SerializeField] BackwardTimer _timer;
    [SerializeField] UniversalButton _startButton;

    DateTime _startTime;
    DateTime _endTime;


    public void Start()
    {
        _startButton.OnClick.AddListener(StartTimer);
    }

    void StartTimer()
    {
        _startTime = DateTime.Now;
        TimeSpan timeSpan = EnergyConfig.Instance.GetEnergyTankReloadSpan();
        _endTime = _startTime.Add(timeSpan);
        
        _timer.StartTimer(_endTime);
        _timer.signalOnTimeUp.AddListener(() => {
            Debug.Log("TIMER FINISH");
        });
    }
}
