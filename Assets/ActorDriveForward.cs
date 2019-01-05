using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDriveForward : MonoBehaviour, ITriggerable {
    public float deleteDistance = 100f;
    bool triggered;
    public float speed = 10;
    public float wait = 2;

    Vector3 originPos;
	// Use this for initialization
	void Start () {
        originPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!triggered) return;

        if(wait>=0){
            wait -= Time.deltaTime;
        } else {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Vector3.Distance(originPos, transform.position) > deleteDistance){
            gameObject.SetActive(false);
        }
	}

    public void Trigger(){
        triggered = true;
    }

    void OnDrawGizmosSelected () {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.up + transform.position + transform.forward * deleteDistance, 5);
        Gizmos.DrawLine(transform.position + Vector3.up, Vector3.up + transform.position + transform.forward * 150);
    }
}
