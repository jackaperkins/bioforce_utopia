using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour {
	public static Transform root;
    public static RectTransform rootRect;
    static UIRoot instance;

    public GameObject actionPrefab;
    public GameObject timesUpPrefab;
    public GameObject greatPrefab;
	// Use this for initialization
	void Awake () {
        root = this.transform;
        rootRect = GetComponent<RectTransform>();
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ShowAction() {
        Instantiate(instance.actionPrefab, root);
    }
    public static void ShowTimesUp()
    {
        Instantiate(instance.timesUpPrefab, root);
    }
    public static void ShowGreat()
    {
        Instantiate(instance.greatPrefab, root);
    }
}
