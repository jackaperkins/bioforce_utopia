using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{

    public Text textLog;

    public void Start()
    {
        Screen.fullScreen = true; // FORCE IT
    }

    public void LaunchProject (string projectName /* "neu", "chloe" etc */)
    {
        string dataPath = "";

        //Print("Wants to launch game for '" + projectName + "'");
#if PLATFORM_STANDALONE_OSX
        dataPath = Application.dataPath; // this is "..../Program.app/Contents"
        // we need to go up two directories to get to data

        DirectoryInfo info = Directory.GetParent(dataPath); // now in the main .app folder
        info = info.Parent; // now in the directory of game file, e.g. neu
        info = info.Parent; // data!
#else
        // windows
        dataPath = Application.dataPath;

        DirectoryInfo info = Directory.GetParent(dataPath); // now we're in the folder with the exe and not the data
        info = info.Parent; // data!
#endif

        // look for a folder in data called the same as projectName
        DirectoryInfo[] folders = info.GetDirectories(projectName);
        if(folders.Length == 0)
        {
           
            throw new System.Exception("there's no directory called " + projectName + " in " + info.FullName);
        }

        string executableFullPath = "";
#if PLATFORM_STANDALONE_OSX
        // on mac so find a directory called wahtever.app!
        DirectoryInfo[] files = folders[0].GetDirectories("*.app");
        
        if (files.Length == 0)
        {
            throw new System.Exception("There's no file matching *.app in " + folders[0].FullName);
        }
        executableFullPath = files[0].FullName;
#else
        FileInfo[] files = folders[0].GetFiles();
        foreach(FileInfo file in files)
        {
            if(file.FullName.Contains(".exe") && !file.FullName.Contains("Unity"))
            {
                executableFullPath = file.FullName;
                break;
            }
        }
#endif


        Screen.fullScreen = false;
        StartCoroutine(ActuallyLaunch(executableFullPath));
    }

    IEnumerator ActuallyLaunch(string fullPath)
    {
        yield return new WaitForEndOfFrame();

        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = fullPath;
        //proc.StartInfo.Arguments = "-screen-fullscreen 1";
        proc.Start();

        yield return new WaitForEndOfFrame();
        Application.Quit(0);
    }

    void Print(string ss)
    {
        //textLog.text += ss + "\n";
    }
}
