using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject lowHappinessPopup, overworkedPopup, underFedPopup, overFedPopup, lowHealthPopup;
    public static GameObject lowHappinessPopupS, overworkedPopupS, underFedPopupS, overFedPopupS, lowHealthPopupS;
    // Start is called before the first frame update
    void Start()
    {
        lowHappinessPopupS = lowHappinessPopup;
        overworkedPopupS = overworkedPopup;
        underFedPopupS = underFedPopup;
        overFedPopupS = overFedPopup;
        lowHealthPopupS = lowHealthPopup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void EnableLowHappinessPopup() {
        lowHappinessPopupS.SetActive(true);
    }
    public static void EnableOverworkedPopup() {
        overworkedPopupS.SetActive(true);
    }
    public static void EnableUnderFedPopup() {
        underFedPopupS.SetActive(true);
    }
    public static void EnableOverFedPopup() {
        overFedPopupS.SetActive(true);
    }
    public static void EnableLowHealthPopup() {
        lowHealthPopupS.SetActive(true);
    }

    public static void DisableLowHappinessPopup() {
        lowHappinessPopupS.SetActive(false);
    }
    public static void DisableOverworkedPopup() {
        overworkedPopupS.SetActive(false);
    }
    public static void DisableUnderFedPopup() {
        underFedPopupS.SetActive(false);
    }
    public static void DisableOverFedPopup() {
        overFedPopupS.SetActive(false);
    }
    public static void DisableLowHealthPopup() {
        lowHealthPopupS.SetActive(false);
    }
}
