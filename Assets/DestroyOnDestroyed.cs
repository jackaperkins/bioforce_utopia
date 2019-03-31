using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDestroyed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void Destroyed () {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        if(particle != null) {
            particle.Stop();
        } else {
            Destroy(gameObject);
        }
 
    }
}
