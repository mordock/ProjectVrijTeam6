using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickManager : MonoBehaviour
{
    public static event Action<TickManager> DayTick = delegate { };

    public GameObject clockPointerPivot;

    public float tickTimeInSec;

    private float currentTime;

    [HideInInspector] public bool timePaused;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update() {
        if (!timePaused)
        currentTime += Time.deltaTime;
        if (currentTime >= tickTimeInSec) {
            currentTime = 0;

            DayTick(this);
        }

        float percentage = currentTime / tickTimeInSec;
        float rotation = Map(percentage, 0, 1, 270, 90);

        clockPointerPivot.transform.eulerAngles = new Vector3(180, 0, rotation);
    }

    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
