using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LoopAnimateSprite : MonoBehaviour {

    public Sprite[] sprites;
    public int frameRate;

    float wait;
    public int index;
    float timer;

    Image image;

	// Use this for initialization
	void Start () {
        wait = 1.0f / frameRate;

        image = GetComponent<Image>();
        image.sprite = sprites[0];

        index = (index ) % sprites.Length;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            timer = wait;

            index = (index + 1) % sprites.Length;

            image.sprite = sprites[index];
        }
    }
}
