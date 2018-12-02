using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonMotion : MonoBehaviour {
    public float yDepth;
    public float speed = 2;

    Vector3 origin;
    Vector3 goal;
    bool down;

	void Start () {
        origin = transform.position;
        goal = transform.position + Vector3.up * yDepth;
        down = true;

        transform.position = Vector3.Lerp(origin, goal, Random.value);
	}
	
	// Update is called once per frame
	void Update () {
        if(down) {
            transform.position = Vector3.MoveTowards(transform.position, goal, speed*Time.deltaTime * 2.5f);
            if(Vector3.Distance(transform.position, goal) < 0.1f) {
                down = !down;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, origin, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, origin) < 0.1f)
            {
                down = !down;
            }
        }

	}
}
