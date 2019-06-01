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

    public AudioClip actionSound;

    public GameObject actionUIElement;
    public GameObject waitUIElement;

	// Use this for initialization
	void Awake () {
        root = this.transform;
        rootRect = GetComponent<RectTransform>();
        instance = this;
        HideAction();
        HideWait();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ShowAction() {
        AudioManager.PlaySound(instance.actionSound);
        instance.Invoke("HideAction", 1f);
        instance.actionUIElement.SetActive(true);
    }


    void HideAction()
    {
        actionUIElement.SetActive(false);
    }

    //----

    public static void ShowWait ()
    {
        instance.waitUIElement.SetActive(true);        
    }

    public static void HideWait ()
    {
        instance.waitUIElement.SetActive(false);
    }

    //---

    public static void ShowTimesUp()
    {
        Instantiate(instance.timesUpPrefab, root);
    }
    public static void ShowGreat()
    {
        Instantiate(instance.greatPrefab, root);
    }
}
