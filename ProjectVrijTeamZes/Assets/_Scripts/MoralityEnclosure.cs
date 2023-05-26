using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoralityEnclosure : MonoBehaviour
{
    public List<Animal> animals;

    //this is time in sec when money and materials are added/removed
    public float maxMoneyTickTime;
    [HideInInspector]
    public float currentFoodValue, currentWorkSlider, currentToolSlider;
    public float toolMultiplier;
    //payout if food and work are both on 100%
    public int maxMaterialPayout;
    public int maxFoodCost, maxToolCost;
    [HideInInspector] public int moneyPayAmount, materialPayoutAmount;

    public GameObject enclosureMaterial;

    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        //set inital food slider value
        currentFoodValue = 0.7f;
        float foodHappiness = Map(currentFoodValue, 0.4f, 0.9f, 10, 100);
        animals.ForEach(animal => {
            animal.happinessChanges[0] = (int)foodHappiness;
        });

        //set intial work slider value
        currentWorkSlider = 0.25f;
        float workHappiness = Map(currentWorkSlider, 0, 0.8f, 0, -40);
        animals.ForEach(animal => {
            animal.happinessChanges[1] = (int)workHappiness;
        });

        //set inital animal happiness
        animals.ForEach(animal => {
            animal.UpdateHappinessAndHealth();
        });

        currentToolSlider = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= maxMoneyTickTime) {
            currentTime = 0;
            //remove money based on costs
            moneyPayAmount = (int)((maxFoodCost * currentFoodValue) + (maxToolCost * currentToolSlider));
            GetComponent<EnclosureScript>().cameraHolder.GetComponent<PlayerInventory>().RemoveMoney(moneyPayAmount);

            //increase materials based on food and work percentage
            float foodPercentage = Map(currentFoodValue, 0, 1, 0, 0.67f);
            float workPercentage = Map(currentWorkSlider, 0, 1, 0, 0.33f);
            materialPayoutAmount = (int)((workPercentage + foodPercentage) * maxMaterialPayout);
            float multipliedAmount = toolMultiplier * materialPayoutAmount;
            Debug.Log(multipliedAmount);
            materialPayoutAmount = (int)multipliedAmount;
            enclosureMaterial.GetComponent<BuildMaterial>().IncreaseAmount(materialPayoutAmount);
        }

        if(GetAverageAnimalHappiness() <= 50) {
            PopupManager.EnableLowHappinessPopup();
        } else {
            PopupManager.DisableLowHappinessPopup();
        }

        if(GetAverageAnimalHealth() <= 30) {
            PopupManager.EnableLowHealthPopup();
        } else {
            PopupManager.DisableLowHealthPopup();
        }
    }

    //method used to display the happiness on the enclosure UI
    public float GetAverageAnimalHappiness() {
        float totalHappiness = 0;
        foreach(Animal animal in animals) {
            totalHappiness += animal.happinessLevel;
        }
        return totalHappiness / animals.Count;
    }

    public float GetAverageAnimalHealth() {
        float totalHealth = 0;
        foreach (Animal animal in animals) {
            totalHealth += animal.healthLevel;
        }
        return totalHealth / animals.Count;
    }

    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
