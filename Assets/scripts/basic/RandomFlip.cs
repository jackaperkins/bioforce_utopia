using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Random.value > 0.5f)
        {
            transform.Rotate(0, 180, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
