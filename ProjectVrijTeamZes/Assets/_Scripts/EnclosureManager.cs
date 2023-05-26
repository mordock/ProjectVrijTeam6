using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnclosureManager : MonoBehaviour
{
    public GameObject currentOpenEnclosure;
    public TextMeshProUGUI happinessText, healthText, materialText;
    public TextMeshProUGUI foodHappinessGainText, workHappinessLoseText, multiplierText;
    public TextMeshProUGUI overUnderFedText, overWorkedText;
    public TextMeshProUGUI materialOutPutText, expenditureText;

    // Start is called before the first frame update
    void Start()
    {
        overUnderFedText.text = "";
        overWorkedText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHappinessText();
        UpdateHealthText();

        //update material output
        if(currentOpenEnclosure != null){
            materialOutPutText.text = "Material Output: " + currentOpenEnclosure.GetComponent<MoralityEnclosure>().materialPayoutAmount.ToString("F2");
            expenditureText.text = "Expenditure: " + currentOpenEnclosure.GetComponent<MoralityEnclosure>().moneyPayAmount.ToString("F2");
        }
    }

    public void UpdateFoodSlider(float value) {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().currentFoodValue = value;
        if (value < 0.4f) {
            float happiness = Map(value, 0, 0.4f, 0, 10);
            AddToHappinessChanges(happiness, 0);
            foodHappinessGainText.text = "+Happiness: " + happiness.ToString("F2");
            SetUnderFed();
            overUnderFedText.text = "UNDERFED!";
            //under fed
            //0-10 happiness
            //minder health
        } else if(value > 0.9f) {
            float happiness = Map(value, 0.9f, 1.0f, 100, 60);
            AddToHappinessChanges(happiness, 0);
            foodHappinessGainText.text = "+Happiness: " + happiness.ToString("F2");
            SetOverFed();
            overUnderFedText.text = "OVERFED!";
            //over fed
            //100-60 happinesss
            //minder health
        } else {
            float happiness = Map(value, 0.4f, 0.9f, 10, 100);
            AddToHappinessChanges(happiness, 0);
            foodHappinessGainText.text = "+Happiness: " + happiness.ToString("F2");
            SetRegularFood();
            overUnderFedText.text = "";
            //between 0.4 and 0.9
            //10-100 happiness
        }
    }

    public void UpdateWorkSlider(float value) {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().currentWorkSlider = value;
        if (value < 0.8f) {
            float happiness = Map(value, 0, 0.8f, 0, -40);
            AddToHappinessChanges(happiness, 1);
            workHappinessLoseText.text = "-Happiness: " + happiness.ToString("F2");
            SetRegularWork();
            //regular work
            //0- -40 happiness
        } else {
            float happiness = Map(value, 0.8f, 1f, -40, -80);
            AddToHappinessChanges(happiness, 1);
            workHappinessLoseText.text = "-Happiness: " + happiness.ToString("F2");
            SetOverWorked();
            overWorkedText.text = "OVERWORKED!";
            //over work
            //-40- -80 happiness
            //minder health
        }
    }

    public void UpdateToolSlider(float value) {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().currentToolSlider = value;
        float multiplier = Map(value, 0, 1f, 1, 1.5f);
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().toolMultiplier = multiplier;
        multiplierText.text = "Material multiplier: " + multiplier.ToString("F2");
    }

    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    private void AddToHappinessChanges(float happiness, int locataion) {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.happinessChanges[locataion] = (int)happiness;
        });
    }

    public void UpdateHappinessText() {
        if(currentOpenEnclosure != null){
            MoralityEnclosure enclosure = currentOpenEnclosure.GetComponent<MoralityEnclosure>();
            happinessText.text = "Happiness: " + enclosure.GetAverageAnimalHappiness().ToString();
        }
    }

    public void UpdateHealthText() {
        if (currentOpenEnclosure != null) {
            MoralityEnclosure enclosure = currentOpenEnclosure.GetComponent<MoralityEnclosure>();
            healthText.text = "Health: " + enclosure.GetAverageAnimalHealth().ToString();
        }
    }

    public void SetUnderFed() {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.underFed = true;
            animal.overFed = false;
        });
    }

    public void SetOverFed() {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overFed = true;
            animal.underFed = false;
        });
    }

    public void SetOverWorked() {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overWorked = true;
        });
    }

    public void SetRegularFood() {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overFed = false;
            animal.underFed = false;
        });
    }

    public void SetRegularWork() {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overWorked = false;
        });
    }
}
