using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {
    ITriggerable[] actors;
	// Use this for initialization
	void Start () {
        actors = GetComponentsInChildren<ITriggerable>();
        foreach(ITriggerable actor in actors) {
            actor.Trigger();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
