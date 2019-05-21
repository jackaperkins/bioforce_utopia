using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTilDestroyed : MonoBehaviour, IShootable {
    public Vector3 rotation;
    Vector3 originalRotation;
    bool dead;
    public bool spinOnHit = false;

	// Use this for initialization
	void Start () {
        originalRotation = rotation;
	}

    public void Destroyed () {
        dead = true;


    }


    public void Shoot () {
        if (!dead && spinOnHit)
        {
            rotation = 2.3f * originalRotation;
        }

    }
	
	// Update is called once per frame
	void Update () {

        

        transform.Rotate(rotation * Time.deltaTime);
        if(dead){
            rotation = Vector3.MoveTowards(rotation, Vector3.zero, 250 * Time.deltaTime);
        } else {
            if (spinOnHit)
            {
                rotation = Vector3.MoveTowards(rotation, originalRotation, 300 * Time.deltaTime);
            }
        }
	}
}
