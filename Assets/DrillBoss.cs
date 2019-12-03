using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBoss : MonoBehaviour, ITriggerable,IDestructable
{
    bool triggered;
    public Vector3 edgeTriggerPosition;
    public Vector3 maximumPosition;
    public Transform parentRotator;
    Vector3 parentOriginRotation;
    public GameObject blockadeUIPrefab;

    float moveSpeed = 3;

    Vector3 goalPosition;
    bool dead;

    public int health = 10;

    SimpleRotate rotator;


    bool introOver;
    bool upDown = true;

    public bool IsDestroyed ()
    {
        return dead;
    }

    // Start is called before the first frame update
    void Start()
    {

        rotator = GetComponent<SimpleRotate>();

        parentOriginRotation = parentRotator.localEulerAngles;
    }

    public void GetShot ()
    {
        Debug.Log("DINKG!!");
        if (dead)
        {
            return;
        }
        health -= 1;
        if (health < 1)
        {
            StartCoroutine("EndGame");
            dead = true;
        }
    }

    IEnumerator EndGame ()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(blockadeUIPrefab, UIRoot.root);
        yield return new WaitForSeconds(4f);
        Transition.instance.ChangeScene("end");
    }

    public void Trigger ()
    {
        triggered = true;
        StartCoroutine(IntroAnimation());
    }



    IEnumerator IntroAnimation ()
    {
        // slowly down

        while (true)
        {
        
            // shoot back up
            goalPosition = maximumPosition;
            moveSpeed = 15f;
            yield return new WaitUntil(Arrived);


            // go down
            goalPosition = edgeTriggerPosition;
            moveSpeed = 20f;
            yield return new WaitUntil(Arrived);
            // wait for death to break
        }
    }

    public bool Arrived ()
    {
        return Vector3.Distance(transform.localPosition, goalPosition) < 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if (!dead)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPosition, Time.deltaTime * moveSpeed);
            }
            else
            {
                rotator.rotation *= 0.95f;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, edgeTriggerPosition, Time.deltaTime * 5);

                parentRotator.localEulerAngles = Vector3.MoveTowards(parentRotator.localEulerAngles, parentOriginRotation + new Vector3(-10, 0, 0), Time.deltaTime*3);
            }

        }
    }
}
