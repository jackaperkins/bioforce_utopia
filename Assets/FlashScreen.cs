using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour {
    public static FlashScreen instance;

    CanvasGroup group;
    float alpha;

	// Use this for initialization
	void Start () {
        instance = this;
        group = GetComponent<CanvasGroup>();
	}

    public void Flash (float amount) {
        alpha = amount;
        group.alpha = alpha;
    }
	
	// Update is called once per frame
	void Update () {
        alpha -= Time.deltaTime * 30;
        group.alpha = alpha;
	}
}
