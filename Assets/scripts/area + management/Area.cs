using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {
    ITriggerable[] actors;

    bool triggered;
    bool done;
    public float length = 8;
	// Use this for initialization
	void Start () {
        
	}
	

    public void Trigger () {
        triggered = true;
        actors = GetComponentsInChildren<ITriggerable>();
        foreach (ITriggerable actor in actors)
        {
            actor.Trigger();
        }
    }


	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Trigger();
        }

        if (done){
            return;
        }

        if(triggered){
            length -= Time.deltaTime;
            if(length <=0)
            {
                done = true;
                StageDirector.AreaFinished();
            }
        }
	}
}
