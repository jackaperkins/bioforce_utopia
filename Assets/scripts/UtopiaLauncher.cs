using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UtopiaLauncher : MonoBehaviour
{
    // VERSION 1.2
    // 
    // 

    public static UtopiaLauncher instance;

    public AudioMixer mixer;

    public static bool didLaunch = false;

    public void Awake()
    {
        // if we're a clone!! clones must die, only one remains
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Screen.fullScreen = true;
        // fisrt time waking up, copy ourselves to the global static varaible instance
        instance = this;
    }


    public static void LaunchProject (UtopiaUtilities.GameTitle title)
    {
        instance.Launch(title);
    }

    public void Launch (UtopiaUtilities.GameTitle title)
    {
        if (didLaunch)
        {
            Debug.LogError("Already tried to launch a game... did this get triggered more than once????");
            return;
        }

        didLaunch = true;

        // EXIT FULLSCREEN, START LAUNCHING
        Screen.fullScreen = false;
        StartCoroutine(ActuallyLaunch());
    }

    IEnumerator ActuallyLaunch()
    {
        yield return new WaitForEndOfFrame();

        mixer.FindSnapshot("fadeout").TransitionTo(0.5f);

        yield return new WaitForSeconds(0.5f);

        yield return new WaitForEndOfFrame();
        Debug.Log("quitting current game");
        Application.Quit();
    }
}
