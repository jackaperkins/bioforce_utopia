using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorFlash : MonoBehaviour {
    Material material;
    public Sprite[] sprites;
    Image image;
    public bool randomSize = false;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.Rotate(0, 0, Random.value * 300);
        if(randomSize) {
            transform.localScale *= Random.Range(1f, 1.5f);
        }
	}
	
	// Update is called once per frame
	void Update () {
        image.color = Random.value > 0.5f ? Color.red : Color.white;
	}
}
