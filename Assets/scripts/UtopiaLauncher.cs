using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UtopiaLauncher : MonoBehaviour
{
    public static UtopiaLauncher instance;

    public enum GameTitle
    {
        fedya,
        merle,
        troy,
        gabriel,
        neu,
        chloe,
        jessica,
        matias,
        jack //itsa me
    };

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

    public void LaunchProject (GameTitle title)
    {
        string projectName = title.ToString();
        Debug.Log("launch new game: " + title);

        DirectoryInfo info = UtopiaUtilities.GetDataPath();

        // look for a folder in data called the same as projectName
        DirectoryInfo[] folders = info.GetDirectories(projectName);
        if(folders.Length == 0)
        {
            throw new System.Exception("there's no directory called " + projectName + " in " + info.FullName);
        }

        DirectoryInfo[] files = folders[0].GetDirectories("*.app");
        // on mac so find a directory called wahtever.app!
        if (files.Length == 0)
        {
            throw new System.Exception("There's no file matching *.app in " + folders[0].FullName);
        }

        // EXIT FULLSCREEN, START LAUNCHING
        Screen.fullScreen = false;
        StartCoroutine(ActuallyLaunch(files[0].FullName));
    }

    IEnumerator ActuallyLaunch(string fullPath)
    {
        yield return new WaitForEndOfFrame();

        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = fullPath;
        Debug.Log("starting new game process");
        proc.Start();

        yield return new WaitForEndOfFrame();
        Debug.Log("quitting current game");
        Application.Quit(0);
    }
}
