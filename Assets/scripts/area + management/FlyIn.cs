using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour, ITriggerable {
    public float timer = 1f;
    bool triggered;

    public Transform warpStartTransform;
     Vector3 startWarp;

    public Transform warpEndTransform;
     Vector3 endWarp;

    Vector3 originScale;
    Vector3 originPosition;

    bool warpIn=true;

    float warpInSpeed;

    bool dead;

    float fallSpeed; // for use when we DIE

	// Use this for initialization
	void Start () {
        startWarp = warpStartTransform.position;
        endWarp = warpEndTransform.position;

        originPosition = transform.position;
        transform.position = startWarp;
        originScale = transform.localScale;
        transform.localScale = Vector3.zero;

        warpInSpeed = Vector3.Distance(startWarp, endWarp) * 4; //makes it in 1/3rd second
	}
	
    public void Destroyed () {
        if (!dead)
        {
            dead = true;

            // knock us up into the air a little if we're not landed yet
            if (Vector3.Distance(transform.position, originPosition) > 5)
            {
                fallSpeed = -30;
                transform.Rotate(Util.Pick(-10, 10), 0, 0);
                transform.Rotate(0,  Util.Pick(-20, 20), 0);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (!triggered) return;
        timer -= Time.deltaTime;
        if(timer <=0){
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 5f * Time.deltaTime);

            if(warpIn) {
                transform.position = Vector3.MoveTowards(transform.position, endWarp, warpInSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position,endWarp) < 1) {
                    warpIn = false;
                }
            } else {
                if (!dead){
                    transform.position = Vector3.MoveTowards(transform.position, originPosition, 8f * Time.deltaTime);
                } else {
                    fallSpeed += Time.deltaTime * 90;
                    transform.position = Vector3.MoveTowards(transform.position, originPosition, fallSpeed* Time.deltaTime);
                }

            }
        }
	}

    public void Trigger () {
        triggered = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if(warpEndTransform) {
            Gizmos.DrawLine(transform.position, warpEndTransform.position);  

            if(warpStartTransform){
                Gizmos.DrawLine(warpStartTransform.position, warpEndTransform.position);
            }
        }

    }
}
