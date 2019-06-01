using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggle : MonoBehaviour {


    public Vector3 direction;
    public float speed = 10;
    public bool stopOnDestroy = true;



    float rota;
    Vector3 origin;
	// Use this for initialization
	void Start () {
        origin = transform.localPosition;
	}

    public void Destroyed () {
        if (stopOnDestroy) {
            direction = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update () {
        rota += Time.deltaTime * speed;

        transform.localPosition = origin;
        Vector3 offset = Mathf.Sin(rota) * direction;
        transform.Translate(offset, Space.Self);
	}
}
