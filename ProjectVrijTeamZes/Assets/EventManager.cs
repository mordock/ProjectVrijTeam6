using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public List<Event> events;

    public int chanceForEvent;
    public int minimumAmountBetweenEvents;

    public GameObject eventUI;
    public TextMeshProUGUI eventName, eventText;
    public GameObject option1, option2, continueButton;

    private int currentDaysPast;
    private bool eventIsOpen = false;
    // Start is called before the first frame update
    void Start() {
        //register for tick event
        TickManager.DayTick += DayTick;

        eventUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    private void DayTick(TickManager obj) {
        if (!eventIsOpen) {
            currentDaysPast++;

            //check if enough minimum days have past
            if (currentDaysPast > minimumAmountBetweenEvents) {
                int random = UnityEngine.Random.Range(1, 100);
                if (random < chanceForEvent) {
                    //fire new event
                    ShowEvent();
                }
                currentDaysPast = 0;
            }
        }
    }

    public void ShowEvent()
    {
        eventIsOpen = true;
        eventUI.SetActive(true);
        option1.SetActive(true);
        option2.SetActive(true);
        continueButton.SetActive(false);


        int random = UnityEngine.Random.Range(0, events.Count);
        Event currentEvent = events[random];

        //update event values
        eventName.text = currentEvent.eventName;
        eventText.text = currentEvent.eventText;

        option1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentEvent.option1.optionText;
        option2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentEvent.option2.optionText;

        //add onclick to option 1 
        if (currentEvent.option1.negativeGain == Event.NegativeGain.Yes)
            {
                option1.GetComponent<Button>().onClick.AddListener(delegate { DecreaseMaterialButton(currentEvent.option1.amount, currentEvent.option1.material); });
                option1.GetComponent<Button>().onClick.AddListener(delegate { ResetEvent(); });
            }
            else if (currentEvent.option1.positiveGain == Event.PositiveGain.Yes)
            {
                option1.GetComponent<Button>().onClick.AddListener(delegate { IncreaseMaterialButton(currentEvent.option1.amount, currentEvent.option1.material); });
                option1.GetComponent<Button>().onClick.AddListener(delegate { ResetEvent(); });
            } 
            else if (currentEvent.option1.postiveDays == Event.PostiveDays.Yes)
            {
                option1.GetComponent<Button>().onClick.AddListener(delegate { IncreaseDays(currentEvent.option1.dayAmount); });
                option1.GetComponent<Button>().onClick.AddListener(delegate { ResetEvent(); });
            }

            //add onclick to option 2 
            if (currentEvent.option2.negativeGain == Event.NegativeGain.Yes)
            {
                option2.GetComponent<Button>().onClick.AddListener(delegate { DecreaseMaterialButton(currentEvent.option2.amount, currentEvent.option2.material); });
                option2.GetComponent<Button>().onClick.AddListener(delegate { Option2Code(currentEvent); });
            }
            else if (currentEvent.option2.positiveGain == Event.PositiveGain.Yes)
            {
                option2.GetComponent<Button>().onClick.AddListener(delegate { IncreaseMaterialButton(currentEvent.option2.amount, currentEvent.option2.material); });
                option2.GetComponent<Button>().onClick.AddListener(delegate { Option2Code(currentEvent); });
            }
            else if (currentEvent.option2.postiveDays == Event.PostiveDays.Yes)
            {
                option2.GetComponent<Button>().onClick.AddListener(delegate { IncreaseDays(currentEvent.option2.dayAmount); });
                option2.GetComponent<Button>().onClick.AddListener(delegate { Option2Code(currentEvent); });
            }
        }

    private void Option2Code(Event currentEvent)
    {
        eventText.text = currentEvent.eventTextNo;
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        continueButton.SetActive(true);
        Debug.Log("nmfadssdniadjnauidniuasmd");
    }

    public void IncreaseMaterialButton(int value, BuildMaterial material) {
        if (material.name != "Money") {
            material.materialAmount += value;
        } else {
            GetComponent<UiManager>().cameraHolder.GetComponent<PlayerInventory>().money += value;
        }
    }

    public void DecreaseMaterialButton(int value, BuildMaterial material) {
        if (material.name != "Money") {
            material.materialAmount -= value;
        } else {
            GetComponent<UiManager>().cameraHolder.GetComponent<PlayerInventory>().money -= value;
        }
    }

    public void IncreaseDays(int value) {
        GetComponent<WinLoseManager>().currentDay += value;
        GetComponent<WinLoseManager>().UpdateDayUI();
    }


    public void ResetEvent() {
        eventIsOpen = false;
        eventUI.SetActive(false);
        option2.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
