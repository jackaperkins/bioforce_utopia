using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBoss : MonoBehaviour, ITriggerable
{
    bool triggered;
    public Vector3 edgeTriggerPosition;
    public Vector3 maximumPosition;

    float moveSpeed = 3;

    Vector3 goalPosition;
    bool dead;

    public int health = 10;


    bool introOver;
    bool upDown = true;

    // Start is called before the first frame update
    void Start()
    {

      
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
            Invoke("EndGame", 3);
            dead = true;
        }
    }

    void EndGame ()
    {
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
        }
    }
}
