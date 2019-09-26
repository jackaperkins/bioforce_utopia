using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UtopiaLauncher : MonoBehaviour
{
    // VERSION 1.2
    // 
    // 

    public static UtopiaLauncher instance;


    public static bool didLaunch = false;

    public void Awake()
    {
        // if we're a clone!! clones must die, only one remains
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

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

        }

        UtopiaUtilities.WriteSaveFile(title);

        string projectName = title.ToString();
        Debug.Log("launch new game: " + title);

        DirectoryInfo info = UtopiaUtilities.GetDataPath();

        // look for a folder in data called the same as projectName
        DirectoryInfo[] folders = info.GetDirectories(projectName);
        if(folders.Length == 0)
        {
            throw new System.Exception("there's no directory called " + projectName + " in " + info.FullName);
        }


        string executablePath = "";
#if PLATFORM_STANDALONE_OSX

        DirectoryInfo[] dirs = folders[0].GetDirectories("*.app");
        // on mac so find a directory called wahtever.app!
        if (dirs.Length == 0)
        {
            throw new System.Exception("There's no file matching *.app in " + folders[0].FullName);
        }
        else
        {
            executablePath = dirs[0].FullName;
        }
#else
        FileInfo[] files = folders[0].GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.FullName.Contains(".exe") && !file.FullName.Contains("Unity"))
            {
                executablePath = file.FullName;
                break;
            }
        }

        if (executablePath.Length == 0)
        {
            throw new System.Exception("Couldn't find the exe in " + folders[0].FullName);
        }

#endif



        // EXIT FULLSCREEN, START LAUNCHING
        Screen.fullScreen = false;
        StartCoroutine(ActuallyLaunch(executablePath));
    }

    IEnumerator ActuallyLaunch(string fullPath)
    {
        if(fullPath.Length == 0)
        {
            throw new System.Exception("No executable provided to launch?");
        }

        yield return new WaitForEndOfFrame();

        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = fullPath;
        Debug.Log("starting new game process");
        proc.Start();

        yield return new WaitForEndOfFrame();
        Debug.Log("quitting current game");
        Application.Quit();
    }
}
