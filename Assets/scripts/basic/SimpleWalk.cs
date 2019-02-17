using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalk : MonoBehaviour {
    Vector3 dir;
    public float speed = 3;
    public float fallSpeed = 3f;
    public float noiseMagnitude = 20f;
    float maxFallSpeed;
    float currentFallSpeed;
    public bool flying = false;

    // Use this for initialization
    void Start() {
        if (flying) { 
            dir = Camera.main.transform.forward * -1;
            dir += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1f)) * noiseMagnitude * 0.3f;
        } else {
            dir = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1,1f));
        }
        dir.Normalize();
        dir.y = 0;
        maxFallSpeed = fallSpeed * 2f;
	}




	
	// Update is called once per frame
	void Update () {
        dir += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1f)) * noiseMagnitude * Time.deltaTime;
        dir.Normalize();


        //Debug.DrawRay(transform.position + Vector3.up*2, dir* 3, Color.green, .1f);

        
        Vector3 newPos = transform.position + dir * Time.deltaTime * speed;
        newPos += Vector3.up * 5;

        //Debug.DrawRay(newPos, Vector3.down*10, Color.red, .1f);
        RaycastHit hit;
        if(Physics.Raycast(newPos, Vector3.down, out hit, 100)){
            float heightDiff = hit.point.y - transform.position.y;
            if(heightDiff > 0){
                // going up a ramp, see if it's 2:1 of our horizontal speed
                if (heightDiff < speed * 2 * Time.deltaTime) {
                    transform.position = hit.point;
                   
                }
                currentFallSpeed = 0;
            } else {
                // fall down
                currentFallSpeed = Mathf.Max(maxFallSpeed * -1, currentFallSpeed - Time.deltaTime * 4);
                if(flying){
                    currentFallSpeed = 0;
                }
                transform.position = new Vector3(hit.point.x, transform.position.y +speed*Time.deltaTime*currentFallSpeed, hit.point.z);

            }
        }
	}
}
