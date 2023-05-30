using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureScript : MonoBehaviour
{
    public GameObject[] enclosureTiers;
    public int[] upgradeCosts;
    public int[] upgradeWoodCosts;
    public int[] upgradeLeafCosts;
    public int[] upgradeStoneCosts;
    public int[] upgradeIceCosts;
    public int[] tierEarnings;

    public int enclosureLevel;
    public GameObject cameraHolder;
    public bool currentlySelected;
    public GameObject enclosureUI;
    public string enclosureName;
    private float earningTimer;
    public int earningAmount;
    public float earningCooldown;

    private int currentCost;
    private int currentWoodCost;
    private int currentLeafCost;
    private int currentStoneCost;
    private int currentIceCost;
    private GameObject gameManager;

    private void Start() {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        for (int i = 0; i < enclosureTiers.Length; i++)
        {
                enclosureTiers[i].SetActive(false);
                enclosureTiers[enclosureLevel].SetActive(true);
        }

        for (int z = 0; z < upgradeCosts.Length; z++)
        {
            if (z == enclosureLevel)
            {
                currentCost = upgradeCosts[z];
                currentWoodCost = upgradeCosts[z];
                currentLeafCost = upgradeCosts[z];
                currentStoneCost = upgradeCosts[z];
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
            gameManager.GetComponent<UiManager>().OpenEnclosureUI();
        }

        //Tijdelijke knop om de upgrades te testen tot we de UI met knoppen hebben. Druk op U om te upgraden.
        if (Input.GetKeyDown(KeyCode.U) && enclosureLevel < enclosureTiers.Length - 1)
        {
            AttemptUpgrade();
        }

        if (earningTimer <= 0)
        {
            cameraHolder.GetComponent<PlayerInventory>().AddMoney(earningAmount);
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
            if (cameraHolder.GetComponent<PlayerInventory>().wood >= currentWoodCost
                && cameraHolder.GetComponent<PlayerInventory>().leaf >= currentLeafCost
                && cameraHolder.GetComponent<PlayerInventory>().stone >= currentStoneCost
                && cameraHolder.GetComponent<PlayerInventory>().ice >= currentIceCost)
            {
                cameraHolder.GetComponent<PlayerInventory>().money -= currentCost;
                cameraHolder.GetComponent<PlayerInventory>().wood -= currentWoodCost;
                cameraHolder.GetComponent<PlayerInventory>().leaf -= currentLeafCost;
                cameraHolder.GetComponent<PlayerInventory>().stone -= currentStoneCost;
                cameraHolder.GetComponent<PlayerInventory>().ice -= currentIceCost;
                enclosureLevel++;
                earningTimer = earningCooldown;
                gameManager.GetComponent<GuestManager>().AddChance(3);
            }
            else
            {
                Debug.Log("Not enough materials");
            }

        }
    }

}
