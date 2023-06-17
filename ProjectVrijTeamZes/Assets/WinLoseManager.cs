using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseManager : MonoBehaviour
{
    public int playDays;
    public TextMeshProUGUI maxDayText, currentDayText;

    [HideInInspector] public int currentDay;
    // Start is called before the first frame update
    void Start()
    {
        TickManager.DayTick += DayTick;
        UpdateDayUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDay > playDays) {
            Win();
        }
    }

    public void Win() {
        Time.timeScale = 0;
        //open end game popup
    }

    public void Lose() {
        Time.timeScale = 0;
        //open end game popup
    }

    private void DayTick(TickManager obj) {
        currentDay++;
        currentDayText.text = "Day: " + currentDay.ToString();
    }

    public void UpdateDayUI() {
        maxDayText.text = playDays.ToString();
        currentDayText.text = "Day: " + currentDay.ToString();
    }
}
