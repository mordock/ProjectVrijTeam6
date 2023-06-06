using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePopup : MonoBehaviour
{
    public GameObject upgradePopup;
    public TextMeshProUGUI enclosureName;

    [Header("Costs")]
    public TextMeshProUGUI moneyCost;
    public TextMeshProUGUI stoneCost;
    public TextMeshProUGUI woodCost;
    public TextMeshProUGUI iceCost;
    public TextMeshProUGUI leafCost;

    public GameObject starHolder;
    public Button upgradeButton;

    private GameObject currentEnclosure;
    // Start is called before the first frame update
    void Start() {
        CloseUpgradePopup();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OpenUpgradePopup() {
        upgradePopup.SetActive(true);

        currentEnclosure = GetComponent<EnclosureManager>().currentOpenEnclosure.transform.parent.gameObject;

        upgradeButton.onClick.AddListener(delegate { currentEnclosure.GetComponent<EnclosureScript>().AttemptUpgrade(); });

        OpenLevel(currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel);
    }

    public void CloseUpgradePopup() {
        upgradePopup.SetActive(false);
    }

    public void OpenLevel(int level) {
        Debug.Log("Called");
        //change name
        enclosureName.text = currentEnclosure.GetComponent<EnclosureScript>().enclosureName;
        //Change Cost values
        moneyCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeCosts[level].ToString();
        if (level > 0) {
            stoneCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeStoneCosts[level - 1].ToString();
            woodCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeWoodCosts[level - 1].ToString();
            iceCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeIceCosts[level - 1].ToString();
            leafCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeLeafCosts[level - 1].ToString();
        } else {
            stoneCost.text = "0";
            woodCost.text = "0";
            iceCost.text = "0";
            leafCost.text = "0";
        }
        //turn on/off starts
        for(int i = 0; i < starHolder.transform.childCount; i++) {
            if(i <= currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel) {
                foreach(Transform child in starHolder.transform.GetChild(i).transform) {
                    child.gameObject.SetActive(true);
                }
            } else {
                foreach (Transform child in starHolder.transform.GetChild(i).transform) {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public void Upgrade() {
        OpenLevel(currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel);
    }
}
