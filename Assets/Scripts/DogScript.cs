//Note to future generations: Do not ever do this

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class DogScript : MonoBehaviour
{
    public float localScale;
    public bool faceRight = true;
    public float distanceToTarget;

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    public Rigidbody2D rb;

    public Transform movePoint;

    private Vector3 initialLocation;
    private Vector3 roamingPosition;

    //public Transform groundCheckPoint;
    //public float groundCheckRadius = 0.2f;
    public bool isGrounded = true;

    public int currentNavPoint;
    public int targetNavPoint;
    public int navShift;

    public HungerBar hungerBar;
    public HappinessBar happyBar;

    public float treatScanRadius = 7f;
    public bool seesTreat1 = false;
    public bool seesTreat2 = false;
    public bool seesTreat3 = false;
    public bool seesTreat4 = false;
    public bool seesTreat5 = false;

    public int treatSeen = 0;
    public LayerMask treatLayer1;
    public LayerMask treatLayer2;
    public LayerMask treatLayer3;
    public LayerMask treatLayer4;
    public LayerMask treatLayer5;

    public float vaseScanRadius;
    public bool seesVase1 = false;
    public bool seesVase2 = false;
    public bool seesVase3 = false;
    public bool seesVase4 = false;

    public int vaseSeen = 0;
    public LayerMask vaseLayer1;
    public LayerMask vaseLayer2;
    public LayerMask vaseLayer3;
    public LayerMask vaseLayer4;

    public bool vase1IsSmashed = false;
    public bool vase2IsSmashed = false;
    public bool vase3IsSmashed = false;
    public bool vase4IsSmashed = false;

    public int vaseAggroNum;

    public GameObject treatObject;
    public GameObject vaseObject;

    public bool fallDelay = false;

    private float startTime;
    private float timer;

    private float tickTimer;
    private float tickStartTime;

    public float hungerMeter;
    public float hungerMax = 45;
    public float hungerDecreasePerTurn = 1;
    public int hungerState = 1;
    public float hungerStateMarker1 = 30;
    public float hungerStateMarker2 = 15;
    public float hungerStateMarker3 = 5;
    public float hungerMultiplier;

    public float jumpHeight = 4f;

    //public float travelDistance;

    public float happyMeter;
    public float happyDecrease;
    public float happyDecreaseMultiplier = 1;
    public int happyMin = 60;
    public int happyMax = 100;


    public float distanceRangeMin = 0.5f;
    public float distanceRangeMax = 1.5f;
    public float moveSpeed = 4f;
    public float timeBetweenMovementChange = 1.5f;

    public float distanceBetweenPoints;
    public TreatScript treat;
    public VaseScript vase;

    public Animator anim;
    public bool animUpdated = false;
    public int animNumChange = 1;
    public int animNumCurrent = 0;


    public Animator iconAnim;
    public bool iconAnimUpdated = false;
    public int iconAnimNumChange = 5;
    public int iconAnimNumCurrent = 0;

    public float relativeX;
    public float relativeY;

    public Transform groundCheck;

    public GameController gameController;

    public Transform[] navPoint = new Transform[52];
    
    /*
    public Transform navPoint1;
    public Transform navPoint2;
    public Transform navPoint3;
    public Transform navPoint4;
    public Transform navPoint5;
    public Transform navPoint6;
    public Transform navPoint7;
    public Transform navPoint8;
    public Transform navPoint9;
    public Transform navPoint10;
    public Transform navPoint11;
    public Transform navPoint12;
    public Transform navPoint13;
    public Transform navPoint14;
    public Transform navPoint15;
    public Transform navPoint16;
    public Transform navPoint17;
    public Transform navPoint18;
    public Transform navPoint19;
    public Transform navPoint20;
    public Transform navPoint21;
    public Transform navPoint22;
    public Transform navPoint23;
    public Transform navPoint24;
    public Transform navPoint25;
    public Transform navPoint26;
    public Transform navPoint27;
    public Transform navPoint28;
    public Transform navPoint29;
    public Transform navPoint30;
    public Transform navPoint31;
    public Transform navPoint32;
    public Transform navPoint33;
    public Transform navPoint34;
    public Transform navPoint35;
    public Transform navPoint36;
    public Transform navPoint37;
    public Transform navPoint38;
    public Transform navPoint39;
    public Transform navPoint40;
    public Transform navPoint41;
    public Transform navPoint42;
    public Transform navPoint43;
    public Transform navPoint44;
    public Transform navPoint45;
    public Transform navPoint46;
    public Transform navPoint47;
    public Transform navPoint48;
    public Transform navPoint49;
    public Transform navPoint50;
    public Transform navPoint51;
    public Transform navPoint52;
    */


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialLocation = transform.position;
        //travelDistance = Random.Range(distanceRangeMin, distanceRangeMax);
        //roamingPosition = GetRoamingPosition();

        startTime = Time.time;
        tickStartTime = Time.time;
        isGrounded = true;

        hungerMeter = hungerMax;
        //happyMeter = (int)Random.Range(happyMin, happyMax);
        happyMeter = 100f;

        iconAnim.SetBool("happyOn", true);

        anim = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        animUpdated = false;
        iconAnimUpdated = false;

        tickTimer = Time.time - tickStartTime;
        if(tickTimer >= 1f)
        {
            OnTick();
            tickStartTime = Time.time;
        }


        timer = Time.time - startTime;

        if(timer >= timeBetweenMovementChange)
        {
            //travelDistance = Random.Range(distanceRangeMin, distanceRangeMax);

            //initialLocation = transform.position;
            //roamingPosition = GetRoamingPosition();

            currentNavPoint = targetNavPoint;

            navShift = Random.Range(-3, 4);
            targetNavPoint = targetNavPoint + navShift;

            if (targetNavPoint < 1)
            {
                targetNavPoint = 1;
            }
            else if (targetNavPoint > 52)
            {
                targetNavPoint = 52;
            }

            distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

            if(Mathf.Abs(distanceBetweenPoints) > 4)
            {
                navShift = Random.Range(4, 10);
                targetNavPoint = targetNavPoint + navShift;

                if (targetNavPoint < 1)
                {
                    targetNavPoint = 1;
                }
                else if (targetNavPoint > 52)
                {
                    targetNavPoint = 52;
                }

                distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                if (Mathf.Abs(distanceBetweenPoints) > 4)
                {
                    navShift = Random.Range(-9, -4);
                    targetNavPoint = targetNavPoint + navShift;

                    if (targetNavPoint < 1)
                    {
                        targetNavPoint = 1;
                    }
                    else if (targetNavPoint > 52)
                    {
                        targetNavPoint = 52;
                    }

                    distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                    if (Mathf.Abs(distanceBetweenPoints) > 4)
                    {
                        navShift = Random.Range(10, 15);
                        targetNavPoint = targetNavPoint + navShift;

                        if (targetNavPoint < 1)
                        {
                            targetNavPoint = 1;
                        }
                        else if (targetNavPoint > 52)
                        {
                            targetNavPoint = 52;
                        }

                        distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                        if (Mathf.Abs(distanceBetweenPoints) > 4)
                        {
                            navShift = Random.Range(-14, -19);
                            targetNavPoint = targetNavPoint + navShift;

                            if (targetNavPoint < 1)
                            {
                                targetNavPoint = 1;
                            }
                            else if (targetNavPoint > 52)
                            {
                                targetNavPoint = 52;
                            }

                            distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                            if (Mathf.Abs(distanceBetweenPoints) > 4)
                            {
                                navShift = Random.Range(15, 20);
                                targetNavPoint = targetNavPoint + navShift;

                                if (targetNavPoint < 1)
                                {
                                    targetNavPoint = 1;
                                }
                                else if (targetNavPoint > 52)
                                {
                                    targetNavPoint = 52;
                                }

                                distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                                if (Mathf.Abs(distanceBetweenPoints) > 4)
                                {
                                    navShift = Random.Range(-19, -14);
                                    targetNavPoint = targetNavPoint + navShift;

                                    if (targetNavPoint < 1)
                                    {
                                        targetNavPoint = 1;
                                    }
                                    else if (targetNavPoint > 52)
                                    {
                                        targetNavPoint = 52;
                                    }

                                    distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                                    if (Mathf.Abs(distanceBetweenPoints) > 4)
                                    {
                                        navShift = Random.Range(20, 50);
                                        targetNavPoint = targetNavPoint + navShift;

                                        if (targetNavPoint < 1)
                                        {
                                            targetNavPoint = 1;
                                        }
                                        else if (targetNavPoint > 52)
                                        {
                                            targetNavPoint = 52;
                                        }
                                        distanceBetweenPoints = transform.position.x - navPoint[targetNavPoint].position.x;

                                        if (Mathf.Abs(distanceBetweenPoints) > 4)
                                        {
                                            navShift = Random.Range(-50, -20);
                                            targetNavPoint = targetNavPoint + navShift;

                                            if (targetNavPoint < 1)
                                            {
                                                targetNavPoint = 1;
                                            }
                                            else if (targetNavPoint > 52)
                                            {
                                                targetNavPoint = 52;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            if(isGrounded == true)
            {
                if(targetNavPoint == 8 || targetNavPoint == 9 || targetNavPoint == 10 || targetNavPoint == 11 || targetNavPoint == 12 ||
                    targetNavPoint == 25 || targetNavPoint == 26 || targetNavPoint == 27 || targetNavPoint == 28 || targetNavPoint == 29 ||
                    targetNavPoint == 42 || targetNavPoint == 43 || targetNavPoint == 44 || targetNavPoint == 45 || targetNavPoint == 46 || targetNavPoint == 47)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    isGrounded = false;
                }
            }
            else
            {
                if (targetNavPoint != 8 && targetNavPoint != 9 && targetNavPoint != 10 && targetNavPoint != 11 && targetNavPoint != 12 &&
                    targetNavPoint != 25 && targetNavPoint != 26 && targetNavPoint != 27 && targetNavPoint != 28 && targetNavPoint != 29 &&
                    targetNavPoint != 42 && targetNavPoint != 43 && targetNavPoint != 44 && targetNavPoint != 45 && targetNavPoint != 46 && targetNavPoint != 47)
                {
                    isGrounded = true;
                }
            }

            startTime = Time.time;
            if (fallDelay == true)
            {
                fallDelay = false;
            }

            if(navShift > 0)
            {
                faceRight = true;
            }
            else if(navShift < 0)
            {
                faceRight = false;
            }
        }

        if (navShift > 0)
        {
            faceRight = true;
        }
        else if (navShift < 0)
        {
            faceRight = false;
        }


        if (isBeingHeld == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

            rb.velocity = new Vector2(0, 0);

            animNumChange = 3;
            animUpdated = true;
        }

        
        else
        {
            if(treatSeen == 0) //If we start the update without seeing a treat, check all layers for nearby treats
            {
                seesTreat1 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer1);
                if(seesTreat1 == true)
                {
                    treatSeen = 1;
                    FindObjectOfType<AudioManager>().Play("bark");
                }
                else
                {
                    seesTreat2 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer2);
                    if (seesTreat2 == true)
                    {
                        treatSeen = 2;
                        FindObjectOfType<AudioManager>().Play("bark");
                    }
                    else
                    {
                        seesTreat3 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer3);
                        if (seesTreat3 == true)
                        {
                            treatSeen = 3;
                            FindObjectOfType<AudioManager>().Play("bark");
                        }
                        else
                        {
                            seesTreat4 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer4);
                            if (seesTreat4 == true)
                            {
                                treatSeen = 4;
                                FindObjectOfType<AudioManager>().Play("bark");
                            }
                            else
                            {
                                seesTreat5 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer5);
                                if (seesTreat5 == true)
                                {
                                    treatSeen = 5;
                                    FindObjectOfType<AudioManager>().Play("bark");
                                }
                            }
                        }
                    }
                }
            }
            else //On the frame after seeing a treat, check to see if the treat is still in range. If so, move towards it. If not, set value to 0 and move normally.
            {
                if(treatSeen == 1)
                {
                    seesTreat1 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer1);
                    if (seesTreat1 == true)
                    {
                        treatObject = GameObject.FindWithTag("Treat1");
                        transform.position = Vector3.MoveTowards(transform.position, treatObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 1;
                            iconAnimUpdated = true;
                        }

                        if(treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();

                    }
                    else
                    {
                        treatSeen = 0;
                    } 
                }
                else if(treatSeen == 2)
                {
                    seesTreat2 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer2);
                    if (seesTreat2 == true)
                    {
                        treatObject = GameObject.FindWithTag("Treat2");
                        transform.position = Vector3.MoveTowards(transform.position, treatObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 1;
                            iconAnimUpdated = true;
                        }

                        if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        treatSeen = 0;
                    }
                }
                else if (treatSeen == 3)
                {
                    seesTreat3 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer3);
                    if (seesTreat3 == true)
                    {
                        treatObject = GameObject.FindWithTag("Treat3");
                        transform.position = Vector3.MoveTowards(transform.position, treatObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 1;
                            iconAnimUpdated = true;
                        }

                        if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        treatSeen = 0;
                    }
                }
                else if (treatSeen == 4)
                {
                    seesTreat4 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer4);
                    if (seesTreat4 == true)
                    {
                        treatObject = GameObject.FindWithTag("Treat4");
                        transform.position = Vector3.MoveTowards(transform.position, treatObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 1;
                            iconAnimUpdated = true;
                        }

                        if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        treatSeen = 0;
                    }
                }
                else if (treatSeen == 5)
                {
                    seesTreat5 = Physics2D.OverlapCircle(transform.position, treatScanRadius, treatLayer5);
                    if (seesTreat5 == true)
                    {
                        treatObject = GameObject.FindWithTag("Treat5");
                        transform.position = Vector3.MoveTowards(transform.position, treatObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 1;
                            iconAnimUpdated = true;
                        }

                        if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (treatObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        treatSeen = 0;
                    }
                }
            }

            if(vaseSeen != 0 && treatSeen == 0)
            {
                if(vaseSeen == 1)
                {
                    if (gameController.vase1Destroyed == false)
                    {
                        vaseObject = GameObject.FindWithTag("Vase1");
                        transform.position = Vector3.MoveTowards(transform.position, vaseObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 2;
                            iconAnimUpdated = true;
                        }

                        if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        vaseSeen = 0;
                    }

                }
                else if(vaseSeen == 2)
                {

                    if (gameController.vase2Destroyed == false)
                    {
                        vaseObject = GameObject.FindWithTag("Vase2");
                        transform.position = Vector3.MoveTowards(transform.position, vaseObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 2;
                            iconAnimUpdated = true;
                        }

                        if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        vaseSeen = 0;
                    }

                }
                else if (vaseSeen == 3)
                {
                    if(gameController.vase3Destroyed == false)
                    {
                        vaseObject = GameObject.FindWithTag("Vase3");
                        transform.position = Vector3.MoveTowards(transform.position, vaseObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 2;
                            iconAnimUpdated = true;
                        }

                        if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();
                    }
                    else
                    {
                        vaseSeen = 0;
                    }

                }
                else if (vaseSeen == 4)
                {
                    if(gameController.vase4Destroyed == false)
                    {
                        vaseObject = GameObject.FindWithTag("Vase4");
                        transform.position = Vector3.MoveTowards(transform.position, vaseObject.transform.position, moveSpeed * Time.deltaTime);

                        if (iconAnimUpdated == false)
                        {
                            iconAnimNumChange = 2;
                            iconAnimUpdated = true;
                        }

                        if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = true;
                        }
                        else if (vaseObject.transform.position.x - transform.position.x > 0)
                        {
                            faceRight = false;
                        }

                        DecideFlip();

                    }
                    else
                    {
                        vaseSeen = 0;
                    }

                }
            }

            if(fallDelay == false && treatSeen == 0 && vaseSeen == 0)
            {
                if(targetNavPoint == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[1].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[2].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[3].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 4)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[4].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[5].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 6)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[6].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 7)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[7].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 8)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[8].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 9)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[9].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 10)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[10].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 11)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[11].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 12)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[12].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 13)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[13].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 14)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[14].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 15)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[15].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 16)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[16].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 17)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[17].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 18)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[18].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 19)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[19].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 20)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[20].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 21)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[21].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 22)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[22].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 23)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[23].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 24)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[24].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 25)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[25].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 26)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[26].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 27)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[27].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 28)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[28].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 29)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[29].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 30)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[30].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 31)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[31].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 32)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[32].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 33)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[33].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 34)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[34].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 35)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[35].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 36)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[36].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 37)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[37].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 38)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[38].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 39)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[39].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 40)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[40].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 41)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[41].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 42)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[42].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 43)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[43].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 44)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[44].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 45)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[45].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 46)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[46].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 47)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[47].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 48)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[48].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 49)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[49].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 50)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[50].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 51)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[51].position, moveSpeed * Time.deltaTime);
                }
                else if (targetNavPoint == 52)
                {
                    transform.position = Vector3.MoveTowards(transform.position, navPoint[52].position, moveSpeed * Time.deltaTime);
                }

                DecideFlip();
            }

            if(happyMeter <= 60)
            {
                if (iconAnimUpdated == false)
                {
                    iconAnimNumChange = 3;
                    iconAnimUpdated = true;
                }
            }

            if(hungerMeter <= 25)
            {
                if (iconAnimUpdated == false)
                {
                    iconAnimNumChange = 4;
                    iconAnimUpdated = true;
                }
            }

            if (iconAnimUpdated == false)
            {
                iconAnimNumChange = 5;
                iconAnimUpdated = true;
            }


            if(iconAnimNumChange != iconAnimNumCurrent)
            {
                iconAnimNumCurrent = iconAnimNumChange;
                AnimReset();

                if(iconAnimNumCurrent == 1)
                {
                    iconAnim.SetBool("treatOn", true);
                }
                else if (iconAnimNumCurrent == 2)
                {
                    iconAnim.SetBool("vaseOn", true);
                }
                else if (iconAnimNumCurrent == 3)
                {
                    iconAnim.SetBool("upsetOn", true);
                }
                else if (iconAnimNumCurrent == 4)
                {
                    iconAnim.SetBool("hungryOn", true);
                }
                else if (iconAnimNumCurrent == 5)
                {
                    iconAnim.SetBool("happyOn", true);
                }
            }
        }

        if (animUpdated == false)
        {

            relativeX = Mathf.Abs(transform.position.x - navPoint[targetNavPoint].position.x);
            relativeY = Mathf.Abs(transform.position.y - navPoint[targetNavPoint].position.y);


            if (relativeX > 0.25f) //If he's moving
            {
                if(fallDelay == true) //If he just got picked up
                {
                    animNumChange = 1;
                }
                else
                {
                    if (relativeY > 0.5f) //If he's moving vertically
                    {
                        if(isGrounded == false)
                        {
                            animNumChange = 4;
                        }
                        else //Jumping off a ledge
                        {
                            animNumChange = 1;
                        }
                    }
                    else //Moving, but not falling
                    {
                        animNumChange = 2;
                    }
                }
            }
            else
            {
                animNumChange = 1;
            }

        }

        //Update Dog Animations

        if(animNumChange != animNumCurrent)
        {
            animNumCurrent = animNumChange;
            MainAnimReset();

            if(animNumCurrent == 1)
            {
                
            }
            else if(animNumCurrent == 2)
            {
                anim.SetBool("isWalk", true);
            }
            else if (animNumCurrent == 3)
            {
                anim.SetBool("isGrab", true);
            }
            else if (animNumCurrent == 4)
            {
                anim.SetBool("isJump", true);
            }
            else if (animNumCurrent == 5)
            {
                //anim.SetBool("isFall", true);
            }


        }



    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<AudioManager>().Play("press");

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;

            //rb.gravityScale = 0;            
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;

        //rb.gravityScale = 1;

        initialLocation = transform.position;
        //roamingPosition = GetRoamingPosition();

        fallDelay = true;
        startTime = Time.time;

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<AudioManager>().Play("pat");
            HappyIncreasePat();
            happyBar.SetFill((int)happyMeter);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Treat1")
        {
            treat = collision.gameObject.GetComponent<TreatScript>();
            treat.Delete();
            HappyIncreaseFeed();
            FindObjectOfType<AudioManager>().Play("treat");

            if (hungerMeter + 30 < 45)
            {
                hungerMeter = hungerMeter + 30;
            }
            else
            {
                hungerMeter = 45;
            }

            hungerBar.SetFill((int)hungerMeter);
        }
        else if (collision.gameObject.tag == "Treat2")
        {
            treat = collision.gameObject.GetComponent<TreatScript>();
            treat.Delete();
            HappyIncreaseFeed();
            FindObjectOfType<AudioManager>().Play("treat");

            if (hungerMeter + 30 < 45)
            {
                hungerMeter = hungerMeter + 30;
            }
            else
            {
                hungerMeter = 45;
            }
            hungerBar.SetFill((int)hungerMeter);
        }
        else if (collision.gameObject.tag == "Treat3")
        {
            treat = collision.gameObject.GetComponent<TreatScript>();
            treat.Delete();
            HappyIncreaseFeed();
            FindObjectOfType<AudioManager>().Play("treat");

            if (hungerMeter + 30 < 45)
            {
                hungerMeter = hungerMeter + 30;
            }
            else
            {
                hungerMeter = 45;
            }
            hungerBar.SetFill((int)hungerMeter);
        }
        else if (collision.gameObject.tag == "Treat4")
        {
            treat = collision.gameObject.GetComponent<TreatScript>();
            treat.Delete();
            HappyIncreaseFeed();
            FindObjectOfType<AudioManager>().Play("treat");

            if (hungerMeter + 30 < 45)
            {
                hungerMeter = hungerMeter + 30;
            }
            else
            {
                hungerMeter = 45;
            }
            hungerBar.SetFill((int)hungerMeter);
        }
        else if (collision.gameObject.tag == "Treat5")
        {
            treat = collision.gameObject.GetComponent<TreatScript>();
            treat.Delete();
            HappyIncreaseFeed();
            FindObjectOfType<AudioManager>().Play("treat");

            if (hungerMeter + 30 < 45)
            {
                hungerMeter = hungerMeter + 30;
            }
            else
            {
                hungerMeter = 45;
            }
            hungerBar.SetFill((int)hungerMeter);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vase1" && vaseSeen == 1)
        {
            vase = collision.gameObject.GetComponent<VaseScript>();
            vase.Delete();
            vase1IsSmashed = true;
            vaseSeen = 0;
        }
        else if (collision.gameObject.tag == "Vase2" && vaseSeen == 2)
        {
            vase = collision.gameObject.GetComponent<VaseScript>();
            vase.Delete();
            vase2IsSmashed = true;
            vaseSeen = 0;
        }
        else if (collision.gameObject.tag == "Vase3" && vaseSeen == 3)
        {
            vase = collision.gameObject.GetComponent<VaseScript>();
            vase.Delete();
            vase3IsSmashed = true;
            vaseSeen = 0;
        }
        else if (collision.gameObject.tag == "Vase4" && vaseSeen == 4)
        {
            vase = collision.gameObject.GetComponent<VaseScript>();
            vase.Delete();
            vase4IsSmashed = true;
            vaseSeen = 0;
        }
    }


    public void OnTick()
    {
        //Hunger Calculations
        if (hungerMeter - hungerDecreasePerTurn > 0)
        {
            hungerMeter = hungerMeter - hungerDecreasePerTurn;
        }
        else
        {
            hungerMeter = 0;
        }

        if(hungerMeter >= hungerStateMarker1) //Greater than/Equal to 30
        {
            hungerState = 1;
            hungerMultiplier = 0.5f;
        }
        else if (hungerMeter < hungerStateMarker1 && hungerMeter >= hungerStateMarker2) //Less than 30 AND Greater than 15
        {
            hungerState = 2;
            hungerMultiplier = 1f;
        }
        else if (hungerMeter < hungerStateMarker2 && hungerMeter >= hungerStateMarker3) //Less than 15 AND Greater than/Equal to 5
        {
            hungerState = 3;
            hungerMultiplier = 2f;
        }
        else
        {
            hungerState = 4;
            hungerMultiplier = 4f;
        }
        hungerBar.SetFill((int)hungerMeter);


        //Happy Calculations

        happyDecrease = happyDecreaseMultiplier * hungerMultiplier;

        if(happyMeter - happyDecrease > 0)
        {
            happyMeter = happyMeter - happyDecrease;
        }
        else
        {
            happyMeter = 0;
        }
        happyBar.SetFill((int)happyMeter);


        //Vase Chase

        if(vaseSeen == 0) //No Vase Selected
        {
            if(happyMeter <= 60) //If Happy Meter is less than 60
            {
                if(happyMeter <= 60 && happyMeter > 40) //Generate Range
                {
                    vaseScanRadius = 7f;
                }
                else if(happyMeter <= 40 && happyMeter > 20)
                {
                    vaseScanRadius = 20f;
                }
                else if(happyMeter <= 20 && happyMeter > 10)
                {
                    vaseScanRadius = 40f;
                }
                else
                {
                    vaseScanRadius = 100f;
                }

                //Generate Aggro Num
                vaseAggroNum = Random.Range(3, 60);
                print(vaseAggroNum);

                if (vaseAggroNum > happyMeter)
                {
                    seesVase1 = Physics2D.OverlapCircle(transform.position, vaseScanRadius, vaseLayer1);
                    if (seesVase1 == true && gameController.vase1Destroyed == false)
                    {
                        vaseSeen = 1;
                        FindObjectOfType<AudioManager>().Play("bark");
                        print("vase1 targeted");
                    }
                    else
                    {
                        seesVase2 = Physics2D.OverlapCircle(transform.position, vaseScanRadius, vaseLayer2);
                        if (seesVase2 == true && gameController.vase2Destroyed == false)
                        {
                            vaseSeen = 2;
                            FindObjectOfType<AudioManager>().Play("bark");
                            print("vase2 targeted");
                        }
                        else
                        {
                            seesVase3 = Physics2D.OverlapCircle(transform.position, vaseScanRadius, vaseLayer3);
                            if (seesVase3 == true && gameController.vase3Destroyed == false)
                            {
                                vaseSeen = 3;
                                FindObjectOfType<AudioManager>().Play("bark");
                                print("vase3 targeted");
                            }
                            else
                            {
                                seesVase4 = Physics2D.OverlapCircle(transform.position, vaseScanRadius, vaseLayer4);
                                if (seesVase4 == true && gameController.vase4Destroyed == false)
                                {
                                    vaseSeen = 4;
                                    FindObjectOfType<AudioManager>().Play("bark");
                                    print("vase4 targeted");
                                }
                            }
                        }
                    }
                }                
            }            
        }
        else //Vase Selected and in Chase
        {
            if(vaseAggroNum < happyMeter)
            {
                vaseSeen = 0;
            }
        }
        

    }

    public void HappyIncreasePat()
    {
        if(happyMeter + 0.5f <= 100)
        {
            happyMeter = happyMeter + 0.5f;
        }
        else
        {
            happyMeter = 100;
        }
    }

    public void HappyIncreaseFeed()
    {
        if (happyMeter + 10 <= 100)
        {
            happyMeter = happyMeter + 10;
        }
        else
        {
            happyMeter = 100;
        }
    }

    public void AnimReset()
    {
        iconAnim.SetBool("happyOn", false);
        iconAnim.SetBool("hungryOn", false);
        iconAnim.SetBool("upsetOn", false);
        iconAnim.SetBool("treatOn", false);
        iconAnim.SetBool("vaseOn", false);
    }

    public void MainAnimReset()
    {
        anim.SetBool("isJump", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isGrab", false);
        //anim.SetBool("isFall", false);
    }

    public void DecideFlip()
    {
        if(faceRight == true)
        {
            FaceRight();
        }
        else
        {
            FaceLeft();
        }
    }

    public void FaceLeft()
    {
        transform.localScale = new Vector2(-localScale, localScale);
    }

    public void FaceRight()
    {
        transform.localScale = new Vector2(localScale, localScale);
    }

}
