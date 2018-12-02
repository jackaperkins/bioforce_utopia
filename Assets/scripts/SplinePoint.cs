using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinePoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1f);
    }


    private void OnDrawGizmosSelected()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 50)) {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, hit.point);
            Gizmos.DrawSphere(hit.point, 0.4f);


            for (float y = hit.point.y+ 0.05f; y < transform.position.y; y += 5) {
                Vector3 left = new Vector3(transform.position.x - 3, y, transform.position.z);
                Vector3 right = new Vector3(transform.position.x + 3, y, transform.position.z);
                Gizmos.DrawLine(left, right);
            }
        }
    }
}
