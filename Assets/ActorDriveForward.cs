using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDriveForward : MonoBehaviour, ITriggerable {

    bool triggered;
    public float speed = 10;
    public float wait = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!triggered) return;

        if(wait>=0){
            wait -= Time.deltaTime;
        } else {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}

    public void Trigger(){
        triggered = true;
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + Vector3.up, Vector3.up + transform.position + transform.forward * 150);
    }
}
