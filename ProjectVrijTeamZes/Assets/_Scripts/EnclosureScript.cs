using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureScript : MonoBehaviour
{
    public GameObject[] enclosureTiers;
    public int[] upgradeCosts;
    public int[] tierEarnings;

    public int enclosureLevel;
    public GameObject cameraHolder;
    public bool currentlySelected;
    public GameObject enclosureUI;
    public string enclosureNaam;
    private float earningTimer;
    public int earningAmount;
    public float earningCooldown;

    private int currentCost;

    void Update()
    {
        for (int i = 0; i < enclosureTiers.Length; i++)
        {
            if (i == enclosureLevel)
            {
                enclosureTiers[i].SetActive(true);
            }
            else
            {
                enclosureTiers[i].SetActive(false);
            }
        }

        for (int z = 0; z < upgradeCosts.Length; z++)
        {
            if (z == enclosureLevel)
            {
                currentCost = upgradeCosts[z];
            }
        }

        for (int x = 0; x < tierEarnings.Length; x++)
        {
            if (x == enclosureLevel)
            {
                earningAmount = tierEarnings[x];
            }
        }

        //UI pop-up
        if (currentlySelected && !enclosureUI.activeSelf)
        {
            enclosureUI.SetActive(true);
        }

        if (!currentlySelected && enclosureUI.activeSelf)
        {
            enclosureUI.SetActive(false);
        }

        //Tijdelijke knop om de upgrades te testen tot we de UI met knoppen hebben. Druk op U om te upgraden.
        if (Input.GetKeyDown(KeyCode.U) && enclosureLevel < enclosureTiers.Length - 1)
        {
            AttemptUpgrade();
        }

        if (earningTimer == 0)
        {
            cameraHolder.GetComponent<PlayerInventory>().money += earningAmount;
            earningTimer = earningCooldown;
        }
    }

    private void FixedUpdate()
    {
        //Money earning timer
        if (earningTimer > 0)
        {
            earningTimer -= Time.fixedDeltaTime;
        }
    }

    public void AttemptUpgrade()
    {
        //Log if player doesn't have enough money
        if (cameraHolder.GetComponent<PlayerInventory>().money < currentCost)
        {
            Debug.Log("Too expensive");
        }

        //Upgrade if player has enough money
        if (cameraHolder.GetComponent<PlayerInventory>().money >= currentCost)
        {
            cameraHolder.GetComponent<PlayerInventory>().money -= currentCost;
            enclosureLevel++;
            earningTimer = earningCooldown;
        }
    }

}
