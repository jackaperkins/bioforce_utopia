using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveForwardAndDie : MonoBehaviour, IShootable, ITriggerable, IDestructable
{
    // 
    float speed;
    public float maxSpeed;
    public float acceleration;
    public float hitReverseSpeed;
    public Transform parentObject;

    public int lifePoints =3;

    bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
    }

    public void Trigger()
    {
        triggered = true;
    }


    public bool IsDestroyed ()
    {
        return lifePoints <= 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered) return;

        if (lifePoints > 0)
        {
            speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
            parentObject.position += parentObject.forward * speed * Time.deltaTime;
        }
    }


    public void Shoot()
    {
        speed = hitReverseSpeed;
        lifePoints -= 1;

    }

    void OnDrawGizmos()
    {
        if(parentObject == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(parentObject.position, parentObject.position + parentObject.forward * 40);
    }
}