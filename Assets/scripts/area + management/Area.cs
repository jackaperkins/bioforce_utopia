using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {
    ITriggerable[] actors;

    IDestructable[] destructables;

    public bool showAction;

    bool triggered;
    bool done;
    public float length = 8;
	// Use this for initialization
	void Start () {
        
	}

    public void EndEarly (){
        done = true;
    }
	

    public void Trigger () {
        triggered = true;
        actors = GetComponentsInChildren<ITriggerable>();
        destructables = GetComponentsInChildren<IDestructable>();

        foreach (ITriggerable actor in actors)
        {
            actor.Trigger();
        }
        if(showAction) {
            UIRoot.ShowAction();
        }
    }


	// Update is called once per frame
	void Update () {

        if (done){
            return;
        }

        if(triggered){
            length -= Time.deltaTime;

            if(destructables.Length > 0){
                bool allDone = true;
                foreach(IDestructable d in destructables){
                    if(!d.IsDestroyed()){
                        allDone = false;
                        break;
                    }
                }
                if (allDone){
                    done = true;
                    AStageDirector.FinishAreaGlobal();
                    UIRoot.ShowGreat(); // finisbed early
                }

            }

            if(length <=0)
            {
                done = true;
                AStageDirector.FinishAreaGlobal(); // it was late
                UIRoot.ShowTimesUp();
            }
        }
	}
}
