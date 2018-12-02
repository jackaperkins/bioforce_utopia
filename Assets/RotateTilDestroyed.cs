using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTilDestroyed : MonoBehaviour {
    public Vector3 rotation;
    bool dead;

	// Use this for initialization
	void Start () {
		
	}

    public void Destroyed () {
        dead = true;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotation * Time.deltaTime);
        if(dead){
            rotation = Vector3.MoveTowards(rotation, Vector3.zero, 40 * Time.deltaTime);
        }
	}
}
