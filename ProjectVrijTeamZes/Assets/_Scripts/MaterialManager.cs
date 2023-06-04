using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialManager : MonoBehaviour
{
    //niet vast, change materials
    public GameObject wood, ice, stone, leaf;

    public TextMeshProUGUI woodUi, IceUi, StoneUi, LeafUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        woodUi.text = wood.GetComponent<BuildMaterial>().materialAmount.ToString();
        IceUi.text = ice.GetComponent<BuildMaterial>().materialAmount.ToString();
        StoneUi.text = stone.GetComponent<BuildMaterial>().materialAmount.ToString();
        LeafUi.text = leaf.GetComponent<BuildMaterial>().materialAmount.ToString();
    }
}
