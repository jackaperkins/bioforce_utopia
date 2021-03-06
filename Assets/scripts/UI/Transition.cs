﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {
    public static Transition instance;
    public CanvasGroup fader;

    public static string hopScene; 

    bool transitioning;
    string nextScene;
	// Use this for initialization
	void Awake () {
        // set that framerate
        Screen.fullScreen = true; // force it

        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
        fader.gameObject.SetActive(true);
        fader.alpha = 1;
	}

    public void ChangeScene (string scenename) {
        if (!transitioning)
        {
            nextScene = scenename;
            transitioning = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(transitioning){
            fader.alpha += Time.deltaTime;
            if(fader.alpha >= 1){
                SceneManager.LoadScene(nextScene);
                transitioning = false;
            }
        } else {
            fader.alpha -= Time.deltaTime;
        }
	}
}
