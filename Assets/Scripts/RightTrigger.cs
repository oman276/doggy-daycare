using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTrigger : MonoBehaviour
{
    public bool isActive = false;
    public CameraMovement cameraMovement;

    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
    }

    void Update()
    {

    }

    private void OnMouseEnter()
    {
        cameraMovement.MoveRight();
    }

    private void OnMouseExit()
    {
        cameraMovement.StopRight();
    }
}
