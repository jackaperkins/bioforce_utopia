using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {
    public Spline spline;
    int goalIndex;
    Vector3 goal;

        
	void Start () {
        goalIndex = 0;
        goal = spline.GetPoint(goalIndex);
        transform.position = goal;

        goalIndex = 1;
        goal = spline.GetPoint(goalIndex);
        transform.LookAt(goal);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(goal - transform.position), Time.deltaTime * 1.4f);

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position = goal;
            goalIndex += 1;
            goal = spline.GetPoint(goalIndex);
        }
        transform.position = Vector3.MoveTowards(transform.position, goal, 15 * Time.deltaTime);

        // transform.Translate(transform.forward * 10 * Time.deltaTime);// = Vector3.MoveTowards(transform.position, goal, 12f*Time.deltaTime);
        if ((goal - transform.position).magnitude < 0.3f)
        {
            goalIndex += 1;
            goal = spline.GetPoint(goalIndex);
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 15);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, goal);
    }
}
