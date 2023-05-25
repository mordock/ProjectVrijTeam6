using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureManager : MonoBehaviour
{
    public GameObject currentOpenEnclosure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFoodSlider(float value) {        
        if(value < 0.4f) {
            Debug.Log(currentOpenEnclosure);
            float happiness = Map(value, 0, 0.4f, 0, 10);
            List<Animal> animals = currentOpenEnclosure.GetComponent<MoralityEnclosure>().animals;
            foreach(Animal animal in animals) {
                animal.happinessLevel = (int)happiness;
            }
            //underfed
            //0-10 happiness
            //minder health
        } else if(value > 0.9f) {
            float happiness = Map(value, 0.9f, 1.0f, 100, 60);
            //overfed
            //100-60 happinesss
            //minder health
        } else {
            float happiness = Map(value, 0.4f, 0.9f, 10, 100);
            //between 0.4 and 0.9
            //10-100 happiness

        }
    }

    public void UpdateWorkSlider(float value) {
        Debug.Log(value);
    }

    public void UpdateToolSlider(float value) {
        Debug.Log(value);
    }

    float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
