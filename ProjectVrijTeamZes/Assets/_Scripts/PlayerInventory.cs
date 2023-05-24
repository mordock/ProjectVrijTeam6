using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int money;

    private void Update()
    {
        //Om te testen heb ik voor nu even cheatknopjes toegevoegd
        //Druk op J voor meer geld, K om te resetten naar 0, en L voor minder geld

        if (Input.GetKeyDown(KeyCode.J))
        {
            money--;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            money = 0;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            money++;
        }

    }

}
