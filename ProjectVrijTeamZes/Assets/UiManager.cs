using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public GameObject cameraHolder;
    public GameObject enclosureUI;
    public TextMeshProUGUI moneyUI;
    public bool enclosureUiIsOpen = false;
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

    public void OpenEnclosureUI() {
        enclosureUI.SetActive(true);

        MoralityEnclosure enclosureScript = GetComponent<EnclosureManager>().currentOpenEnclosure.GetComponent<MoralityEnclosure>();
        //set slider values to current enclosure ones
        enclosureUI.transform.GetChild(1).GetChild(0).GetComponent<Slider>().value = enclosureScript.currentFoodValue;
        enclosureUI.transform.GetChild(1).GetChild(1).GetComponent<Slider>().value = enclosureScript.currentWorkSlider;
        enclosureUI.transform.GetChild(1).GetChild(2).GetComponent<Slider>().value = enclosureScript.currentToolSlider;
        enclosureScript.isCurrentEnclosure = true;
    }
}
