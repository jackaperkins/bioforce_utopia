using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

public class StageDirector : MonoBehaviour {
    static StageDirector instance;

    PlayableDirector director;

    public static void AreaFinished () {
        
        instance.NextArea();
    }

    private void Awake()
    {
        paused = true;
        instance = this;
        director = GetComponent<PlayableDirector>();
    }

    bool paused;
	// Use this for initialization
	void Start () {
		
	}

    void NextArea () {
        if(paused){
            paused = false;
            director.Play();
        }   
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
