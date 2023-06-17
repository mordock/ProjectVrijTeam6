using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMaterial : MonoBehaviour
{
    public string materialName;
    public int materialAmount;
    public Sprite icon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseAmount(int amount) {
        materialAmount += amount;
    }

    public void DecreaseAmount(int amount) {
        materialAmount -= amount;
    }
}
