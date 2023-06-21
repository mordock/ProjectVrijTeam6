using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    void Update()
    {
        //Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if ((highlight.CompareTag("Selectable") || highlight.CompareTag("SelectableMain")) && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        //Selection
        if (Input.GetMouseButtonDown(0))
        {
            GameObject gamemanager = GameObject.Find("GameManager");
            if(!gamemanager.GetComponent<Tutorial>().isPlayingFirstTutorial || !gamemanager.GetComponent<Tutorial>().isPlayingEnclosureTutorial)
            {
                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                        selection.gameObject.GetComponentInParent<EnclosureScript>().currentlySelected = false;
                    }
                    //main building
                    if (highlight.CompareTag("SelectableMain")) {
                        gamemanager.GetComponent<AdminManager>().OpenAdminUI();
                    } else{
                        //enclosures
                        selection = raycastHit.transform;
                        selection.gameObject.GetComponent<Outline>().enabled = true;
                        selection.gameObject.GetComponentInParent<EnclosureScript>().currentlySelected = true;
 
                        gamemanager.GetComponent<EnclosureManager>().currentOpenEnclosure = selection.gameObject;
                        gamemanager.GetComponent<UiManager>().enclosureUiIsOpen = true;

                        highlight = null;
                    }
                }
            }
        }
    }

    public void Deselect()
    {
        selection.gameObject.GetComponent<Outline>().enabled = false;
        selection.gameObject.GetComponentInParent<EnclosureScript>().currentlySelected = false;
        selection = null;
    }
}
