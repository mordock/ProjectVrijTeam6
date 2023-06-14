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

    public TextMeshProUGUI output;
    public Image outputIcon;

    public TextMeshProUGUI upgradeButtonText;

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

        OpenLevel(currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel + 1);
    }

    public void CloseUpgradePopup() {
        upgradePopup.SetActive(false);
        upgradeButton.onClick.RemoveAllListeners();
    }

    public void OpenLevel(int level) {
        if (currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel == 5) {
            level -= 1;
        }

        //change name
        enclosureName.text = currentEnclosure.GetComponent<EnclosureScript>().enclosureName;
        //Change Cost values
        if (level > 0) {
            moneyCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeCosts[level - 1].ToString();
            stoneCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeStoneCosts[level - 1].ToString();
            woodCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeWoodCosts[level - 1].ToString();
            iceCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeIceCosts[level - 1].ToString();
            leafCost.text = currentEnclosure.GetComponent<EnclosureScript>().upgradeLeafCosts[level - 1].ToString();
        } else {
            moneyCost.text = "N/A";
            stoneCost.text = "N/A";
            woodCost.text = "N/A";
            iceCost.text = "N/A";
            leafCost.text = "N/A";
        }

        //turn on/off starts
        for (int i = 0; i < starHolder.transform.childCount; i++) {
            if (i <= currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel) {
                foreach (Transform child in starHolder.transform.GetChild(i).transform) {
                    child.gameObject.SetActive(true);
                }
            } else {
                foreach (Transform child in starHolder.transform.GetChild(i).transform) {
                    child.gameObject.SetActive(false);
                }
            }
        }
        EnclosureScript enclosure = currentEnclosure.GetComponent<EnclosureScript>();
        //change output
        if (level <= 5 && level > 0) {
            output.text = "+ " + (enclosure.tierEarnings[level] - enclosure.tierEarnings[level - 1]).ToString();
        } else {
            output.text = "N/A";
        }

        outputIcon.sprite = currentEnclosure.GetComponent<MoralityEnclosure>().enclosureMaterial.GetComponent<BuildMaterial>().icon;

        if (enclosure.enclosureLevel < 5) {
            upgradeButtonText.text = "upgrade to level " + (enclosure.enclosureLevel + 1);
        }
        if (enclosure.enclosureLevel == 5) {
            upgradeButtonText.text = "MAXED";
        }
    }

    public void Upgrade() {
        OpenLevel(currentEnclosure.GetComponent<EnclosureScript>().enclosureLevel + 1);
    }
}
