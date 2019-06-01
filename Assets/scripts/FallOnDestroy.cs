using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnDestroy : MonoBehaviour {
    public Transform goal;
    public float topSpeed = 50;
    public float acceleration = 25;
    float speed;

    Vector3 goalPosition;
    Quaternion goalRotation;

    bool triggered;

	// Use this for initialization
	void Start () {

	}

    void Destroyed() {
        triggered = true;
        goalPosition = goal.position;
        goalRotation = goal.rotation;
    }

    // Update is called once per frame
    void Update () {
		if (triggered) {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, topSpeed);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, speed * 7 * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos() {
        if(goal != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, goal.position);
        }
    }
}
