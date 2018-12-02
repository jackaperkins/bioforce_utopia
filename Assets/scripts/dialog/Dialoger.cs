using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;


[Serializable]
public class DialogLine {
    public string portrait;
    public string text;
    public string sound;
    public bool hideBackground = false;
}

[Serializable]
public class DialogWrapper {
    public DialogLine[] lines;
}

public class Dialoger : MonoBehaviour {
    public DialogLine currentDialogLine;
    public DialogCanvasController canvas;
    public string nextScene;

    int dialogIndex;
    private List<DialogLine> dialogLines;

    public TextAsset dialogJSON;
    public Sprite[] portraits;

	void Start () {
        dialogIndex = -1;
        dialogLines = new List<DialogLine>(JsonUtility.FromJson<DialogWrapper>(dialogJSON.text).lines);
	}


    Sprite FindPortait(string portraitName) {
        if (portraitName == "") {
            return null;
        }
        foreach(Sprite t in portraits) {
            if (t.name.ToLower() == portraitName.ToLower()) {
                return t;
            }
        }
        return null;
    }
	
	void Update () {
        if(Input.GetMouseButtonDown(0)) {
            if (canvas.printing)
            {
                canvas.Skip(); //fast forward text printing
            }
            else
            {
                dialogIndex++;
                if (dialogIndex >= dialogLines.Count)
                {
                    // next scene
                    Transition.instance.ChangeScene(nextScene);

                }
                else
                {
                    currentDialogLine = dialogLines[dialogIndex];
                    Sprite finalPortrait = FindPortait(currentDialogLine.portrait);

                    canvas.ShowText(currentDialogLine.text, finalPortrait);
                    //if (currentDialogLine.sound != "") {
                        //AudioSource a = GetComponent<AudioSource>();
                        //a.clip = currentDialogLine.sound;
                        //a.Play();
                    //}
                }
            }
        }
	}
}
