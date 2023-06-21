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
    public string scientificName;
    private float earningTimer;
    public int earningAmount;
    public float earningCooldown;
    public GameObject currentAnimalIcon;

    private int currentCost;
    private int currentWoodCost;
    private int currentLeafCost;
    private int currentStoneCost;
    private int currentIceCost;
    private GameObject gameManager;

    private void Start() {
        gameManager = GameObject.Find("GameManager");
    }

    void Update() {
        for (int z = 0; z < upgradeCosts.Length; z++) {
            if (z == enclosureLevel) {
                if (z < upgradeCosts.Length) {
                    currentCost = upgradeCosts[z];
                    currentWoodCost = upgradeWoodCosts[z];
                    currentLeafCost = upgradeLeafCosts[z];
                    currentStoneCost = upgradeStoneCosts[z];
                    currentIceCost = upgradeIceCosts[z];
                }
            }
        }

        for (int x = 0; x < tierEarnings.Length; x++) {
            if (x == enclosureLevel) {
                earningAmount = tierEarnings[x];
            }
        }

        if (currentlySelected && !gameManager.GetComponent<Tutorial>().enclosureTutorialDone)
        {
            gameManager.GetComponent<Tutorial>().enclosureTutorialDone = true;
            gameManager.GetComponent<Tutorial>().PlayEnclosureTutorial();
        }

        //UI pop-up
        if (currentlySelected && !enclosureUI.activeSelf) {
            gameManager.GetComponent<UiManager>().OpenEnclosureUI(gameObject);
        }

        if (earningTimer <= 0) {
            cameraHolder.GetComponent<PlayerInventory>().AddMoney(earningAmount);
            earningTimer = earningCooldown;
        }
    }

    private void FixedUpdate() {
        //Money earning timer
        if (earningTimer > 0) {
            earningTimer -= Time.fixedDeltaTime;
        }
    }

    public void AttemptUpgrade() {
        MaterialManager materialManager = gameManager.GetComponent<MaterialManager>();
        //Upgrade if player has enough materials
        if (materialManager.wood.GetComponent<BuildMaterial>().materialAmount >= currentWoodCost
            && materialManager.leaf.GetComponent<BuildMaterial>().materialAmount >= currentLeafCost
            && materialManager.stone.GetComponent<BuildMaterial>().materialAmount >= currentStoneCost
            && materialManager.ice.GetComponent<BuildMaterial>().materialAmount >= currentIceCost) {
            cameraHolder.GetComponent<PlayerInventory>().money -= currentCost;
            materialManager.wood.GetComponent<BuildMaterial>().materialAmount -= currentWoodCost;
            materialManager.leaf.GetComponent<BuildMaterial>().materialAmount -= currentLeafCost;
            materialManager.stone.GetComponent<BuildMaterial>().materialAmount -= currentStoneCost;
            materialManager.ice.GetComponent<BuildMaterial>().materialAmount -= currentIceCost;
            enclosureLevel++;
            earningTimer = earningCooldown;
            gameManager.GetComponent<GuestManager>().AddChance(3);
            gameManager.GetComponent<UpgradePopup>().Upgrade();

            for (int i = 0; i < enclosureTiers.Length; i++) {
                enclosureTiers[i].SetActive(false);
                enclosureTiers[enclosureLevel].SetActive(true);
            }
        } else {
            Debug.Log("Not enough materials");
        }

    }

}
