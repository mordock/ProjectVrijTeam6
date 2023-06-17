using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public GameObject cameraHolder;
    public GameObject floatingText;
    public GameObject floatingTextPos;
    public GameObject guestPrefab;
    public GameObject startingWalkPoint;

    public float guestTimer;
    public int guestTimerInSeconds;
    public int baseGuestChance = 10;
    private float currentGuestChance;
    public int ticketPrice;

    private void Start()
    {
        cameraHolder = GameObject.Find("CameraHolder");
    }

    void FixedUpdate()
    {
        if (currentGuestChance < 10)
            currentGuestChance = baseGuestChance;

        if (guestTimer >= 0)
        {
            guestTimer -= Time.fixedDeltaTime;
        }

        if (guestTimer <= 0)
        {
            if (Random.Range(0.0f, 100.0f) <= currentGuestChance)
            {
                SummonGuest();
            }
            guestTimer = guestTimerInSeconds;
        }
    }

    void SummonGuest()
    {
        Debug.Log("GUEST");
        cameraHolder.GetComponent<PlayerInventory>().AddMoney(ticketPrice);
        var go = Instantiate(floatingText, floatingTextPos.transform.position, floatingTextPos.transform.rotation, floatingTextPos.transform);
        go.GetComponent<TextMesh>().text = "+$" + ticketPrice.ToString();

        GameObject guest = Instantiate(guestPrefab);
        guest.transform.position = startingWalkPoint.transform.position;
        guest.GetComponent<Guest>().previousPoint = startingWalkPoint;
    }

    //
    public void AddChance(int addedValue)
    {
        currentGuestChance += addedValue;
    }

    public void RemoveChance(int removeValue)
    {
        currentGuestChance -= removeValue;
    }
}
