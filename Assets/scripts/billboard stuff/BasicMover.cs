using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour {
    public Transform goalPoint;
    public float speed;
    Vector3 origin;
	// Use this for initialization
	void Start () {
        origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        if(Vector3.Magnitude(transform.position - goalPoint.position) < 0.4f) {

            transform.position = origin;
        }
	}
}
