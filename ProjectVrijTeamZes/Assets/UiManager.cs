using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UiManager : MonoBehaviour
{
    public GameObject cameraHolder;
    public GameObject enclosureUI;
    public TextMeshProUGUI moneyUI;
    [HideInInspector] public bool enclosureUiIsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        //CloseEnclosureUI();
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = cameraHolder.GetComponent<PlayerInventory>().money.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && !GetComponent<Tutorial>().isPlayingEnclosureTutorial)
        {
            if (GetComponent<UpgradePopup>().upgradePopupOpen)
            {
                GetComponent<UpgradePopup>().CloseUpgradePopup();
            }
            else
            if (enclosureUiIsOpen && !GetComponent<Tutorial>().isPlayingFirstTutorial && !GetComponent<Tutorial>().isPlayingEnclosureTutorial)
            {
                cameraHolder.GetComponent<OutlineSelection>().Deselect();
                GetComponent<EnclosureManager>().currentOpenEnclosure.transform.parent.gameObject.GetComponentInParent<EnclosureScript>().currentlySelected = false;
                CloseEnclosureUI();
            }
        }

    }

    public void CloseEnclosureUI() {
        if (!GetComponent<Tutorial>().isPlayingEnclosureTutorial)
        {
            GetComponent<EnclosureManager>().currentOpenEnclosure.transform.parent.gameObject.GetComponentInParent<EnclosureScript>().currentlySelected = false;
            enclosureUiIsOpen = false;
            if (GetComponent<EnclosureManager>().currentOpenEnclosure != null)
            {
                MoralityEnclosure enclosureScript = GetComponent<EnclosureManager>().currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>();
                enclosureScript.isCurrentEnclosure = false;
            }
            GetComponent<EnclosureManager>().currentOpenEnclosure = null;
            enclosureUI.SetActive(false);
            cameraHolder.GetComponent<OutlineSelection>().Deselect();
        }
    }

    public void OpenEnclosureUI(GameObject enclosure) {
        enclosureUI.SetActive(true);
        MoralityEnclosure enclosureScript = GetComponent<EnclosureManager>().currentOpenEnclosure.transform.parent.gameObject.GetComponent<MoralityEnclosure>();
        //set slider values to current enclosure ones
        enclosureUI.transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = enclosureScript.currentFoodValue;
        enclosureUI.transform.GetChild(1).GetChild(1).GetComponent<Slider>().value = enclosureScript.currentWorkSlider;
        enclosureUI.transform.GetChild(1).GetChild(2).GetComponent<Slider>().value = enclosureScript.currentToolSlider;
        enclosureScript.isCurrentEnclosure = true;
    }
}
