using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

public class AStageDirector : MonoBehaviour {
    public static AStageDirector instance;

    PlayableDirector director;

    Area currentArea;


    public bool inAction = false;

    public Transform areaRoot;

    private void Awake()
    {
        inAction = false;
        instance = this;
        director = GetComponent<PlayableDirector>();
    }

	// Use this for initialization
	void Start () {
		
	}

    public static void FinishAreaGlobal (){
        instance.FinishArea();
    }

    public void FinishArea () {
        inAction = false;
        StartCoroutine(DelayedFinishArea());
    }


    public void BeginArea (string areaName) {
        director.Pause();
        foreach(Transform t in areaRoot) {
            if (t.name.ToLower() == areaName) {
                currentArea = t.gameObject.GetComponent<Area>();
                currentArea.Trigger();
                inAction = true;
                return;
            }
        }
        print("couldn't find area " + areaName);
    }

    IEnumerator DelayedFinishArea () {
        yield return new WaitForSeconds(0.7f);
        director.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if(inAction && Input.GetKeyDown(KeyCode.Tab)) {
            currentArea.EndEarly();
            director.Play();
            inAction = false;
        }
	}


}
