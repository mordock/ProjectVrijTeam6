using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraMovementSpeed;

    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    private bool drag = false;
    public bool enclosureUISelected = false;

    private GameObject gameManager;

    private void Start()
    {
        ResetCamera = gameObject.transform.position;
        gameManager = GameObject.Find("GameManager");
    }

    private void LateUpdate()
    {
        if (!gameManager.GetComponent<Tutorial>().isPlayingFirstTutorial && !gameManager.GetComponent<Tutorial>().isPlayingEnclosureTutorial)
        {
            if (Input.GetMouseButton(0))
            {
                Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - gameObject.transform.position;
                if (drag == false && !enclosureUISelected)
                {
                    drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                gameObject.transform.position = Origin - Difference;
            }

            if (Input.GetKey(KeyCode.O))
            {
                gameObject.transform.position = ResetCamera;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate((Vector3.left + Vector3.back) * cameraMovementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate((Vector3.right + Vector3.forward) * cameraMovementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate((Vector3.forward + Vector3.left) * cameraMovementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate((Vector3.back + Vector3.right) * cameraMovementSpeed * Time.deltaTime);
            }

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        
    }

    public void TurnDragOff()
    {
        enclosureUISelected = true;
    }
}
