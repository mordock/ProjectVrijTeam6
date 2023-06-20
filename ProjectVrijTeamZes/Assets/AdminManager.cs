using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdminManager : MonoBehaviour
{
    public List<int> ticketPrices;
    public GameObject adminUI; 

    public List<int> upgradePrices;

    public PlayerInventory playerInventory;
    public Image tooExpensiveImage;

    [Header("Top UI")]
    public TextMeshProUGUI levelUi;
    public Image progressBar;
    public TextMeshProUGUI progressText;

    [Header("Admin UI")]
    public TextMeshProUGUI priceText;
    public GameObject starList;
    public GameObject ticketPriceList;

    private int adminLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        tooExpensiveImage.gameObject.SetActive(false);
        CloseAdminUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTopUI();
        UpdateAdminUI();

        if(adminLevel >= 5) {
            GetComponent<WinLoseManager>().Win();
        }
    }

    public void OpenAdminUI() {
        adminUI.SetActive(true);
    }

    public void CloseAdminUI() {
        adminUI.SetActive(false);
    }

    public void SetTicketPrice(int ticketPosition) {
        GuestManager guestManager = GetComponent<GuestManager>();
        if (ticketPosition.Equals(0)) {
            guestManager.ticketPrice = ticketPrices[0];
        }else if (ticketPosition.Equals(1)) {
            guestManager.ticketPrice = ticketPrices[1];
        } else if (ticketPosition.Equals(2)) {
            guestManager.ticketPrice = ticketPrices[2];
        } else if (ticketPosition.Equals(3)) {
            guestManager.ticketPrice = ticketPrices[3];
        } else if (ticketPosition.Equals(4)) {
            guestManager.ticketPrice = ticketPrices[4];
        }
    }

    public void UpgradeAdmin() {
        int currentPrice = upgradePrices[adminLevel - 1];

        if(currentPrice > playerInventory.money) {
            //too expensive
            tooExpensiveImage.gameObject.SetActive(true);
        } else {
            adminLevel++;
            playerInventory.RemoveMoney(currentPrice);
            tooExpensiveImage.gameObject.SetActive(false);
        }
    }

    public void UpdateTopUI() {
        levelUi.text = adminLevel.ToString();

        int currentPrice = upgradePrices[adminLevel - 1];

        progressText.text = playerInventory.money + "/" + currentPrice;
        float fillAmount = (float)playerInventory.money / (float)currentPrice;
        if(fillAmount > 1) {
            fillAmount = 1;
        }
        progressBar.fillAmount = fillAmount;
    }

    public void UpdateAdminUI() {
        priceText.text = upgradePrices[adminLevel - 1].ToString();

        for(int i = 0; i < starList.transform.childCount; i++) {
            if((i + 1) <= adminLevel) {
                starList.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                starList.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < ticketPriceList.transform.childCount; i++) {
            ticketPriceList.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = playerInventory.money + "/" + ticketPrices[i].ToString();
        }
    }
}
