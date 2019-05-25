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
    public GameObject powerupPrefab;

    public Transform areaRoot;

    public bool debugStartFromOffset;
    public float debugOffsetTime;

    private void Awake()
    {
        inAction = false;
        instance = this;
        director = GetComponent<PlayableDirector>();
        if (debugStartFromOffset)
        {
            director.time = debugOffsetTime;
        }
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
        Area area = GetAreaByName(areaName);
        if (area) {
            currentArea = area;
            currentArea.Trigger();
            inAction = true;
        }
    }

    public void SpawnUIPrefab (string name) {
        if (name == "powerup")
        {
            Instantiate(powerupPrefab, UIRoot.root);
        }
    }

    public void BeginAreaAndContinue (string areaName) {
        Area area = GetAreaByName(areaName);
        if (area) {
            currentArea = area;
            currentArea.Trigger();
            inAction = true;
        }
    }

    Area GetAreaByName (string areaName) {
        foreach (Transform t in areaRoot) {
            if (t.name.ToLower() == areaName) {
                return  t.gameObject.GetComponent<Area>();
            }
        }
        throw new System.Exception("couldn't find area " + areaName);
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
