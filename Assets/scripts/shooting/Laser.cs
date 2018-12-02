using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    Vector3 forwardDirection;
    LineRenderer line;
    float speed = 70;
    float maxDistance = 20;
    float lifeTime = 0.5f;
    bool onOff;
    float strobeTimer;

    bool dieNextFrame;

    public GameObject prefab;
    Vector3 startPoint;

	void Start () {
       
	}

    public void Setup (Vector3 forwards, GameObject toSpawn) {
        // instantiated position is origin, forwards is where the beam heads
        forwardDirection = forwards.normalized;
        prefab = toSpawn;
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        startPoint = transform.position;
        line.SetPosition(1, transform.position + forwardDirection * speed * 0.05f);
    }
	
    void Update () {
        if (dieNextFrame) {
            Destroy(gameObject);
            return;
        }
        if (Vector3.Distance(startPoint, transform.position) > maxDistance) {
            dieNextFrame = true;
        }

        strobeTimer -= Time.deltaTime;
        if (strobeTimer < 0)
        {
            strobeTimer = 0.04f;
            onOff = !onOff;
        }
        line.material.SetColor("_Color", onOff ? Color.red : Color.yellow);


        RaycastHit hit;
        if(Physics.Raycast(line.GetPosition(1), forwardDirection, out hit,Time.deltaTime * speed * 1.5f,LayerMask.GetMask("Target"))) {
            dieNextFrame = true;

            GameObject g = (GameObject) Instantiate(prefab, hit.point, Quaternion.identity);
            HitFlash flash = hit.collider.gameObject.GetComponent<HitFlash>();
            if(flash){
                flash.Hit();
            }
            if (prefab.tag == "Parent")
            {
                g.transform.SetParent(hit.collider.transform);
            }
        }

        line.SetPosition(0, line.GetPosition(0) + forwardDirection * speed * Time.deltaTime);
        line.SetPosition(1, line.GetPosition(1) + forwardDirection * speed * Time.deltaTime * 1.5f);

        if (Vector3.Distance(line.GetPosition(1), transform.position) > 200){
            dieNextFrame = true;
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
	}
}
