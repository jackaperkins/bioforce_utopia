using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    static CameraShake instance;

    float shaking = 0;

    float lookUp;

    public static void Shake (float time) {
        if (instance != null){
            instance.shaking = time;
        }
    }
	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        lookUp += Time.deltaTime * 20;
        if (shaking > 0)
        {
            shaking -= Time.deltaTime;
            transform.localEulerAngles = new Vector3(TwoSine(lookUp) * 1, TwoSine(lookUp *2)*2,0);
        } else {
            transform.localEulerAngles = Vector3.zero;
        }
	}

    float TwoSine (float input){
        return (Mathf.Sin(input) + Mathf.Sin(2.3f * input) * 0.5f) / 1.5f;
    }
}
