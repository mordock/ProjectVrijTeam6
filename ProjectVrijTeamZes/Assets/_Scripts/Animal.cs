using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalName;

    //1-100
    public int healthLevel;

    //every tick all numbers in this list will be added together to make total happiness
    [HideInInspector] public List<int> happinessChanges = new List<int>();

    public bool underFed, overFed, overWorked;
    //1-100
    public int happinessLevel;

    private float happinessTickTime = 5f;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        //add 2 empty spots in the list for happiness changes
        happinessChanges.Add(0);
        happinessChanges.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        //movement logic here
        //TODO

        //add happiness/health tick every x seconds
        currentTime += Time.deltaTime;
        if (currentTime >= happinessTickTime) {
            currentTime = 0;

            UpdateHappinessAndHealth();
        }

        //animal dies
        if(healthLevel <= 0) {
            Debug.Log("Yo " + animalName + " fcking died");
        }

        if(happinessLevel <= 0) {
            happinessLevel = 0;
        }
    }

    public void UpdateHappinessAndHealth() {
        //reset happiness
        happinessLevel = 0;

        //total happiness is done by adding all the changes to happiness(work and food for now)
        foreach (int change in happinessChanges) {
            happinessLevel += change;
        }

        //animal is happy, health go up
        if (happinessLevel > 90) {
            healthLevel++;
        }

        //animal is unhappy, health go down
        if (happinessLevel < 50) {
            healthLevel--;
        }
        //addition health damage when abused
        if(underFed) {
            healthLevel--;
        }
        if (overFed) {
            healthLevel--;
        }
        if (overWorked) {
            healthLevel--;
        }
    }
}
