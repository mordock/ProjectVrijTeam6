using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseManager : MonoBehaviour
{
    public int playDays;
    public int debtLimit;
    public TextMeshProUGUI maxDayText, currentDayText;
    public GameObject loseScreen, winScreen;

    [HideInInspector] public int currentDay;
    // Start is called before the first frame update
    void Start()
    {
        TickManager.DayTick += DayTick;
        UpdateDayUI();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDay > playDays) {
            Lose();
        }
    }

    public void Win() {
        Time.timeScale = 0;
        //open end game popup
        winScreen.SetActive(true);
    }

    public void Lose() {
        Time.timeScale = 0;
        //open end game popup
        loseScreen.SetActive(true);
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
