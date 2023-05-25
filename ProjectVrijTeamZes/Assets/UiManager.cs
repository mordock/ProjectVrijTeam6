using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject enclosureUI;

    public bool enclosureUiIsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        CloseEnclosureUI();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enclosureUiIsOpen);
    }

    public void CloseEnclosureUI() {
        enclosureUI.SetActive(false);
        gameObject.GetComponent<UiManager>().enclosureUiIsOpen = false;
    }

    public void OpenEnclosureUI() {
        enclosureUI.SetActive(true);
    }
}
