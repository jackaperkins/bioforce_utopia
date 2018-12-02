using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogCanvasController : MonoBehaviour {
    public Transform dialogBoxTopLevel;
    public Text textBox;
    public Image portrait;
    public Image background;

    string currentText;
    int textIndex;
    float textPrintTimer;
    public bool printing = false;

    public void Start()
    {
        background.gameObject.SetActive(false);
        portrait.gameObject.SetActive(false);
    }

    public void ShowText (string text, Sprite portraitSprite) {
        if (!portraitSprite) {
            portrait.gameObject.SetActive(false); 
        } else {
            portrait.sprite = portraitSprite;   
            portrait.gameObject.SetActive(true);   
        }

        currentText = text;
        textBox.text = "";
        textIndex = 0;
        printing = true;
    }

    public void Skip () {
        printing = false;
        textBox.text = currentText;
    }

	// Update is called once per frame
	void Update () {
        if (!printing) return;

        if (textIndex < currentText.Length) {
            textPrintTimer -= Time.deltaTime;
            if(textPrintTimer <= 0) {
                textPrintTimer = 0.03f;
                textIndex++;
                textBox.text = currentText.Substring(0, textIndex);
            }
        } else {
            printing = false;
        }
	}
}
