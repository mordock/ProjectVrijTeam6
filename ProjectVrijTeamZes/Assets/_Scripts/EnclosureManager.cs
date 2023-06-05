using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnclosureManager : MonoBehaviour
{
    public GameObject currentOpenEnclosure;
    public TextMeshProUGUI happinessText, healthText, materialText;
    public TextMeshProUGUI happinessPercentage, workPercentage, multiplierText;
    public TextMeshProUGUI materialOutPutText, expenditureText;
    public Image happinessBar, healthBar;
    public TextMeshProUGUI enclosureName, animalNames;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHappinessPercentageUi();
        UpdateHealthPercentageUi();

        //update material output
        if(currentOpenEnclosure != null){
            materialOutPutText.text = "Material Output: " + currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().materialCalculatedPayout.ToString("F2");
            expenditureText.text = "Expenditure: " + currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().moneyPayAmount.ToString("F2");

            //fill top bar with data
            enclosureName.text = currentOpenEnclosure.transform.parent.gameObject.GetComponent<EnclosureScript>().enclosureName;
            string names = "";
            foreach(Animal animal in currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().animals) {
                names += animal.animalName + ", ";
            }
            //remove last comma
            animalNames.text = names.Remove(names.Length - 2);
        }
    }

    public void UpdateFoodSlider(float value) {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().currentFoodValue = value;
        if (value < 0.4f) {
            float happiness = Map(value, 0, 0.4f, 0, 10);
            AddToHappinessChanges(happiness, 0);
            happinessPercentage.text = happiness.ToString("F2") + "%";
            SetUnderFed();
            PopupManager.EnableUnderFedPopup();
            PopupManager.DisableOverFedPopup();
            //under fed
            //0-10 happiness
            //minder health
        } else if(value > 0.9f) {
            float happiness = Map(value, 0.9f, 1.0f, 100, 60);
            AddToHappinessChanges(happiness, 0);
            happinessPercentage.text = happiness.ToString("F2") + "%";
            SetOverFed();
            PopupManager.EnableOverFedPopup();
            PopupManager.DisableUnderFedPopup();
            //over fed
            //100-60 happinesss
            //minder health
        } else {
            float happiness = Map(value, 0.4f, 0.9f, 10, 100);
            AddToHappinessChanges(happiness, 0);
            happinessPercentage.text = happiness.ToString("F2") + "%";
            SetRegularFood();
            PopupManager.DisableOverFedPopup();
            PopupManager.DisableUnderFedPopup();
            //between 0.4 and 0.9
            //10-100 happiness
        }
    }

    public void UpdateWorkSlider(float value) {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().currentWorkSlider = value;
        if (value > 0.8f) {
            float happiness = Map(value, 0.8f, 1f, -40, -80);
            AddToHappinessChanges(happiness, 1);
            SetOverWorked();
            PopupManager.EnableOverworkedPopup();
            //over work
            //-40- -80 happiness
            //minder health
        } else {
            float happiness = Map(value, 0, 0.8f, 0, -40);
            AddToHappinessChanges(happiness, 1);
            SetRegularWork();
            PopupManager.DisableOverworkedPopup();
            //regular work
            //0- -40 happiness
        }
        workPercentage.text = (value * 100).ToString("F2") + "%";
    }

    public void UpdateToolSlider(float value) {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().currentToolSlider = value;
        float multiplier = Map(value, 0, 1f, 1, 1.5f);
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().toolMultiplier = multiplier;
        multiplierText.text = multiplier.ToString("F2") + "X";
    }

    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    //add the actual values to animal happiness change
    private void AddToHappinessChanges(float happiness, int locataion) {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.happinessChanges[locataion] = (int)happiness;
        });
    }

    public void UpdateHappinessPercentageUi() {

        if (currentOpenEnclosure != null){
            MoralityEnclosure enclosure = currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>();

            happinessText.text = enclosure.GetAverageAnimalHappiness().ToString() + "%";
            happinessBar.fillAmount = enclosure.GetAverageAnimalHappiness() / 100;
        }
    }

    public void UpdateHealthPercentageUi() {
        if (currentOpenEnclosure != null) {
            MoralityEnclosure enclosure = currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>();
            healthText.text = enclosure.GetAverageAnimalHealth().ToString() + "%";
            healthBar.fillAmount = enclosure.GetAverageAnimalHealth() / 100;
        }
    }

    public void SetUnderFed() {
        currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.underFed = true;
            animal.overFed = false;
        });
    }

    public void SetOverFed() {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overFed = true;
            animal.underFed = false;
        });
    }

    public void SetOverWorked() {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overWorked = true;
        });
    }

    public void SetRegularFood() {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overFed = false;
            animal.underFed = false;
        });
    }

    public void SetRegularWork() {
        currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>().animals.ForEach(animal => {
            animal.overWorked = false;
        });
    }
}
