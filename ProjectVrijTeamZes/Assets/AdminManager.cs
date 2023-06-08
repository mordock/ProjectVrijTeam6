using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    public int veryLowTicket, lowTicket, mediumTicket, highTicket, veryHighTicket;
    public GameObject adminUI;

    private int adminLevel = 1;

    private GuestManager guestManager;
    // Start is called before the first frame update
    void Start()
    {
        CloseAdminUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAdminUI() {
        adminUI.SetActive(true);
    }

    public void CloseAdminUI() {
        adminUI.SetActive(false);
    }

    public void SetTicketPrice(int ticketPosition) {
        if (ticketPosition.Equals(0)) {

        }else if (ticketPosition.Equals(1)) {

        }else if (ticketPosition.Equals(2)) {

        }else if (ticketPosition.Equals(3)) {

        }else if (ticketPosition.Equals(4)) {

        }
    }

    public void UpgradeAdmin() {
        adminLevel++;
    }
}
