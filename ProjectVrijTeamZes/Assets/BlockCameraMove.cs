using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockCameraMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject cameraHolder;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        cameraHolder.GetComponent<CameraMove>().enclosureUISelected = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        cameraHolder.GetComponent<CameraMove>().enclosureUISelected = false;
    }

}
