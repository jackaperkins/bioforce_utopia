using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour {
    public TextAsset dialogJson;
    DialogLine[] lines;
	// Use this for initialization
	void Start () {
        lines = JsonUtility.FromJson<DialogWrapper>(dialogJson.text).lines;
        foreach(DialogLine line in lines){
            print(line.text);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
