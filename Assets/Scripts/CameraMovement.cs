using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float leftLimit;
    public float rightLimit;

    public bool moveLeft = false;
    public bool moveRight = false;

    public float xPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = this.gameObject.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveLeft == true)
        {
            if(xPos - 0.2f >= leftLimit)
            {
                xPos = xPos - 0.2f;
                this.gameObject.transform.localPosition = new Vector3(xPos, 0, -10);
            }
        }

        if(moveRight == true)
        {
            if(xPos + 0.2 <= rightLimit)
            {
                xPos = xPos + 0.2f;
                this.gameObject.transform.localPosition = new Vector3(xPos, 0, -10);
            }
        }
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void StopLeft()
    {
        moveLeft = false;
    }

    public void StopRight()
    {
        moveRight = false;
    }

}
