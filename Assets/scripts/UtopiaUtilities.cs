using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UtopiaUtilities
{
    public static DirectoryInfo GetDataPath()
    {   

#if PLATFORM_STANDALONE_OSX
        string dataPath = Application.dataPath; // this is "..../Program.app/Contents"
        // we need to go up two directories to get to data

        Debug.Log("standalone OSX path getting:");
        DirectoryInfo info = Directory.GetParent(dataPath); // now in the main .app folder
        info = info.Parent;

#if UNITY_EDITOR
        Debug.Log("editor osx path getting:");
        info = info.Parent; // now in the directory of game file, e.g. neu
    #endif

#else
        // windows
        // this is on troys computer
#endif
        return info;
    }

    public static string[] ReadLogFile()
    {
        DirectoryInfo dataPath = GetDataPath();
        FileInfo[] files = dataPath.GetFiles("log.txt");

        // array of files should be either 0 or 1 long
        if(files == null || files.Length == 0)
        {
            // return an empty array of string
            return new string[0];
        }

        string[] lines = File.ReadAllLines(files[0].FullName);

        return lines;
    }

    // append a line to the log, creating it if it doesnt exist yet
    public static void WriteLogFile (string myText)
    {
        DirectoryInfo dataPath = GetDataPath();
        File.AppendAllText(Path.Combine(dataPath.FullName, "log.txt"), myText + "\n");
    }

    // if we already have log.txt with content, move it to log_DATE.txt
    public static void CycleLogFile ()
    {
        DirectoryInfo dataPath = GetDataPath();
        FileInfo[] files = dataPath.GetFiles("log.txt");

        // array of files should be either 0 or 1 long
        if (files == null || files.Length == 0)
        {
            return;
        }
       
        string currentFullPath = files[0].FullName;
        DateTime date = DateTime.Now;
        string dateFormatter = "_yyyy-MM-dd_H-mm";
        string newPath = currentFullPath.Replace(".txt", date.ToString(dateFormatter) + ".txt");

        File.Move(currentFullPath, newPath);
    }


    public static bool HasGameBeenPlayed(UtopiaLauncher.GameTitle theGame)
    {
        DirectoryInfo dataPath = GetDataPath();
        FileInfo[] files = dataPath.GetFiles("save.txt");

        // array of files should be either 0 or 1 long
        if (files == null || files.Length == 0)
        {
            return false;
        }

        string[] lines = File.ReadAllLines(files[0].FullName);

        foreach(string line in lines)
        {
            if(line.Contains(theGame.ToString()))
            {
                return true;
            }
        }
        return false;
    }

    // append a line to the log, creating it if it doesnt exist yet
    public static void WriteSaveFile(UtopiaLauncher.GameTitle game)
    {
        DirectoryInfo dataPath = GetDataPath();
        string logFile = Path.Combine(dataPath.FullName, "save.txt");
        Debug.Log(logFile);

        File.AppendAllText(logFile, game.ToString() + "\n");
    }

    public static void ClearSaveFile()
    {
        DirectoryInfo dataPath = GetDataPath();
        FileInfo[] files = dataPath.GetFiles("save.txt");

        // array of files should be either 0 or 1 long
        if (files == null || files.Length == 0)
        {
            return;
        }

        File.Delete(files[0].FullName);
    }
}
