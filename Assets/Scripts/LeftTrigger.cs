using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTrigger : MonoBehaviour
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
        cameraMovement.MoveLeft();
    }

    private void OnMouseExit()
    {
        cameraMovement.StopLeft();
    }
}
