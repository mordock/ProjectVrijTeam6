using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickManager : MonoBehaviour
{
    public static event Action<TickManager> DayTick = delegate { };

    public List<GameObject> tickCircles;

    public float tickTimeInSec;

    private float currentTime;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;
        if (currentTime >= tickTimeInSec) {
            currentTime = 0;

            DayTick(this);
        }

        foreach(GameObject circle in tickCircles) {
            circle.GetComponent<Image>().fillAmount = currentTime / tickTimeInSec;
        }
    }
}
