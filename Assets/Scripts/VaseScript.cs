using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseScript : MonoBehaviour
{
    public bool isVase1;
    public bool isVase2;
    public bool isVase3;
    public bool isVase4;

    public GameController gameController;

    public Animator anim;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        anim = GetComponent<Animator>();
    }

    public void Delete()
    {
        if (isVase1)
        {
            print("Vase1 destroyed");
            gameController.VaseDestroy(1);
        }
        else if (isVase2)
        {
            print("Vase2 destroyed");
            gameController.VaseDestroy(2);
        }
        else if (isVase3)
        {
            print("Vase3 destroyed");
            gameController.VaseDestroy(3);
        }
        else if (isVase4)
        {
            print("Vase4 destroyed");
            gameController.VaseDestroy(4);
        }

        anim.SetBool("isBroken", true);
    }
}
