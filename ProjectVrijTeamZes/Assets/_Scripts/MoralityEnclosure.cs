using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoralityEnclosure : MonoBehaviour
{
    public List<Animal> animals;

    [HideInInspector] public float currentFoodValue, currentWorkSlider, currentToolSlider;
    [HideInInspector] public float toolMultiplier;
    //payout if food and work are both on 100%
    public int maxMaterialPayout;
    //maximum money cost, if food or tools are 100%
    public int maxFoodCost, maxToolCost;
    [HideInInspector] public int moneyPayAmount, materialPayoutAmount;
    [HideInInspector] public bool isCurrentEnclosure;
    public GameObject enclosureMaterial;
    // Start is called before the first frame update
    void Start() {
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
        toolMultiplier = 1;

        //register for tick event
        TickManager.DayTick += DayTick;
    }

    // Update is called once per frame
    void Update() {
        if(isCurrentEnclosure){
            //show and remove low happiness popup
            if (GetAverageAnimalHappiness() <= 50) {
                PopupManager.EnableLowHappinessPopup();
            } else {
                PopupManager.DisableLowHappinessPopup();
            }
            //show and remove low health popup
            if (GetAverageAnimalHealth() <= 30) {
                PopupManager.EnableLowHealthPopup();
            } else {
                PopupManager.DisableLowHealthPopup();
            }
        }
    }

    //method used to display the happiness on the enclosure UI
    public float GetAverageAnimalHappiness() {
        float totalHappiness = 0;
        foreach (Animal animal in animals) {
            totalHappiness += animal.happinessLevel;
        }
        return totalHappiness / animals.Count;
    }

    //method used to display the health on the enclosure UI
    public float GetAverageAnimalHealth() {
        float totalHealth = 0;
        foreach (Animal animal in animals) {
            totalHealth += animal.healthLevel;
        }
        return totalHealth / animals.Count;
    }

    //map a number within a range to a different range
    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    private void DayTick(TickManager tickManager) {
        Debug.Log("TICK " + gameObject.name);

        //remove money based on costs
        moneyPayAmount = (int)((maxFoodCost * currentFoodValue) + (maxToolCost * currentToolSlider));
        GetComponent<EnclosureScript>().cameraHolder.GetComponent<PlayerInventory>().RemoveMoney(moneyPayAmount);

        //increase materials based on food and work percentage
        float foodPercentage = Map(currentFoodValue, 0, 1, 0, 0.67f);
        float workPercentage = Map(currentWorkSlider, 0, 1, 0, 0.33f);
        materialPayoutAmount = (int)((workPercentage + foodPercentage) * maxMaterialPayout);
        Debug.Log(toolMultiplier);
        float multipliedAmount = toolMultiplier * materialPayoutAmount;
        materialPayoutAmount = (int)multipliedAmount;
        enclosureMaterial.GetComponent<BuildMaterial>().IncreaseAmount(materialPayoutAmount);
        
    }
}
