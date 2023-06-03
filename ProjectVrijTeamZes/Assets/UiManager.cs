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
    public Button upgradeButton;
    [HideInInspector] public bool enclosureUiIsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        CloseEnclosureUI();
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = cameraHolder.GetComponent<PlayerInventory>().money.ToString();
    }

    public void CloseEnclosureUI() {
        enclosureUI.SetActive(false);
        enclosureUiIsOpen = false;
        if(GetComponent<EnclosureManager>().currentOpenEnclosure != null){
            MoralityEnclosure enclosureScript = GetComponent<EnclosureManager>().currentOpenEnclosure.GetComponent<MoralityEnclosure>();
            enclosureScript.isCurrentEnclosure = false;
        }

        GetComponent<EnclosureManager>().currentOpenEnclosure = null;
    }

    public void OpenEnclosureUI(GameObject enclosure) {
        enclosureUI.SetActive(true);

        MoralityEnclosure enclosureScript = GetComponent<EnclosureManager>().currentOpenEnclosure.GetComponent<MoralityEnclosure>();
        //set slider values to current enclosure ones
        enclosureUI.transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = enclosureScript.currentFoodValue;
        enclosureUI.transform.GetChild(1).GetChild(1).GetComponent<Slider>().value = enclosureScript.currentWorkSlider;
        enclosureUI.transform.GetChild(1).GetChild(2).GetComponent<Slider>().value = enclosureScript.currentToolSlider;
        enclosureScript.isCurrentEnclosure = true;
        
        upgradeButton.onClick.AddListener(delegate { enclosure.GetComponent<EnclosureScript>().AttemptUpgrade(); });
    }

    public void CloseAdminUI()
    {

    }

    public void OpenAdminUI()
    {

    }


}
