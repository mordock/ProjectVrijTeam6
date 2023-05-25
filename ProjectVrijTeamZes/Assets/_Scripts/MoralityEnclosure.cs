using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoralityEnclosure : MonoBehaviour
{
    //public Slider foodSlider, workSlider, toolSlider;

    public List<Animal> animals;

    //this is time in sec when money and materials are added/removed
    public float payoutTime;

    public int moneyPayoutAmount, materialPayoutAmount;

    public GameObject enclosureMaterial;

    private float currentFoodValue, currentWorkSlider, currentToolSlider;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= payoutTime) {
            currentTime = 0;
            //TODO add increase money method 
            enclosureMaterial.GetComponent<BuildMaterial>().IncreaseAmount(materialPayoutAmount);
        }

        //Debug.Log(Map(.5f, 0, 1, 1, 100));
    }

    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
