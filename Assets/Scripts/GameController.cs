using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image vase1;
    public Image vase2;
    public Image vase3;
    public Image vase4;

    public bool vase1Destroyed = false;
    public bool vase2Destroyed = false;
    public bool vase3Destroyed = false;
    public bool vase4Destroyed = false;

    public bool gameOver = false;

    public int numOfVases = 4;

    public float timeRemaining;
    public float startTime;

    public GameObject resetScreen;

    public LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VaseDestroy(int vaseNum)
    {
        if(vaseNum == 1)
        {
            Destroy(vase1);
            FindObjectOfType<AudioManager>().Play("glass");
            numOfVases--;
            vase1Destroyed = true;
            if(numOfVases == 0)
            {
                gameOver = true;
                print("Game Over!");
                resetScreen.SetActive(true);
            }
        }
        else if (vaseNum == 2)
        {
            Destroy(vase2);
            FindObjectOfType<AudioManager>().Play("glass");
            numOfVases--;
            vase2Destroyed = true;
            if (numOfVases == 0)
            {
                gameOver = true;
                print("Game Over!");
                resetScreen.SetActive(true);
            }
        }
        else if (vaseNum == 3)
        {
            Destroy(vase3);
            FindObjectOfType<AudioManager>().Play("glass");
            numOfVases--;
            vase3Destroyed = true;
            if (numOfVases == 0)
            {
                gameOver = true;
                print("Game Over!");
                resetScreen.SetActive(true);
            }
        }
        else if (vaseNum == 4)
        {
            Destroy(vase4);
            FindObjectOfType<AudioManager>().Play("glass");
            numOfVases--;
            vase4Destroyed = true;
            if (numOfVases == 0)
            {
                gameOver = true;
                print("Game Over!");
                resetScreen.SetActive(true);
            }
        }
    }
}
