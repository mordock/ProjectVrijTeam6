using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalName;

    //1-100
    public int healthLevel;

    //every tick all numbers in this list will be added together to make total happiness
    [HideInInspector] public List<int> happinessChanges = new List<int>();

    public bool underFed, overFed, overWorked;
    //1-100
    public int happinessLevel;

    private Vector3 randomDirection;
    public Vector3 currentPos;
    public Vector3 targetPos;
    public float elapsedTime;
    public float timeBeforeMovement = 2f;
    private bool againstWall;

    private Animator animalAnimator;


    void Start()
    {
        //add 2 empty spots in the list for happiness changes
        happinessChanges.Add(0);
        happinessChanges.Add(0);

        //register for tick event
        TickManager.DayTick += DayTick;

        //Animalmovement
        animalAnimator = gameObject.GetComponentInChildren<Animator>();
        currentPos = transform.position;
        targetPos = transform.position;
        AnimalMovement();
    }

    void Update()
    {
        timeBeforeMovement -= Time.deltaTime;

        //movement logic here
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / 1.3f;

        transform.position = Vector3.Lerp(currentPos, targetPos, percentageComplete);

        if (elapsedTime > 1.3f)
            animalAnimator.SetBool("Walking", false);

        if (timeBeforeMovement < 0)
        {
            AnimalMovement();
        }

        //animal dies
        if (healthLevel <= 0) {
            Debug.Log("Yo " + animalName + " fcking died");
        }

        if(happinessLevel <= 0) {
            happinessLevel = 0;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("yap");
            againstWall = true;
            animalAnimator.SetBool("Walking", true);
            currentPos = transform.position;
            randomDirection = new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), 0, UnityEngine.Random.Range(1.0f, 3.0f));
            targetPos = currentPos - randomDirection;
            elapsedTime = 0f;
            timeBeforeMovement = UnityEngine.Random.Range(1.5f, 10f);
        }

    }

    //Call to assign new target position that the animal will move to
    private void AnimalMovement()
    {
        animalAnimator.SetBool("Walking", true);
        currentPos = transform.position;
        randomDirection = new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), 0, UnityEngine.Random.Range(-3.0f, 3.0f));
        targetPos = currentPos - randomDirection;
        elapsedTime = 0f;
        timeBeforeMovement = UnityEngine.Random.Range(1.5f, 10f);
    }

    public void UpdateHappinessAndHealth() {
        //reset happiness
        happinessLevel = 0;

        //total happiness is done by adding all the changes to happiness(work and food for now)
        foreach (int change in happinessChanges) {
            happinessLevel += change;
        }

        //animal is happy, health go up
        if (happinessLevel > 90) {
            healthLevel++;
        }

        //animal is unhappy, health go down
        if (happinessLevel < 50) {
            healthLevel--;
        }
        //addition health damage when abused
        if(underFed) {
            healthLevel--;
        }
        if (overFed) {
            healthLevel--;
        }
        if (overWorked) {
            healthLevel--;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PosXWall"))
        {
            Debug.Log("posX" + gameObject);
            againstWall = true;
            animalAnimator.SetBool("Walking", true);
            currentPos = transform.position;
            randomDirection = new Vector3(UnityEngine.Random.Range(1.0f, 3.0f), 0, UnityEngine.Random.Range(-3.0f, 3.0f));
            targetPos = currentPos - randomDirection;
            elapsedTime = 0f;
            timeBeforeMovement = UnityEngine.Random.Range(1.5f, 10f);
        }

        if (other.CompareTag("NegXWall"))
        {
            Debug.Log("negX" + gameObject);
            againstWall = true;
            animalAnimator.SetBool("Walking", true);
            currentPos = transform.position;
            randomDirection = new Vector3(UnityEngine.Random.Range(-1.0f, -3.0f), 0, UnityEngine.Random.Range(-3.0f, 3.0f));
            targetPos = currentPos - randomDirection;
            elapsedTime = 0f;
            timeBeforeMovement = UnityEngine.Random.Range(1.5f, 10f);
        }

        if (other.CompareTag("PosZWall"))
        {
            Debug.Log("posZ" + gameObject);
            againstWall = true;
            animalAnimator.SetBool("Walking", true);
            currentPos = transform.position;
            randomDirection = new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), 0, UnityEngine.Random.Range(1.0f, 3.0f));
            targetPos = currentPos - randomDirection;
            elapsedTime = 0f;
            timeBeforeMovement = UnityEngine.Random.Range(1.5f, 10f);
        }

        if (other.CompareTag("NegZWall"))
        {
            Debug.Log("negZ" + gameObject);
            againstWall = true;
            animalAnimator.SetBool("Walking", true);
            currentPos = transform.position;
            randomDirection = new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), 0, UnityEngine.Random.Range(-1.0f, -3.0f));
            targetPos = currentPos - randomDirection;
            elapsedTime = 0f;
            timeBeforeMovement = UnityEngine.Random.Range(1.5f, 10f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        againstWall = false;
    }

    private void DayTick(TickManager obj) {
        UpdateHappinessAndHealth();
    }
}
