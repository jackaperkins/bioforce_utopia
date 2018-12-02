using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSprite : MonoBehaviour {
    public enum SpeedOption {
        i8=8,
        i16= 16,
        i24 = 24
    }

    public SpeedOption speed = SpeedOption.i24;
    public bool randomFlip = false;

    public Texture2D[] sprites;
    int spriteIndex = 0;
    Material material;

    float timer;
	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
        spriteIndex = Random.Range(0, sprites.Length);
        if(randomFlip) {
            if (Random.value > 0.5f)
            {
                transform.Rotate(0, 0, 180, Space.Self);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0){
            timer = (1 / (float)(speed));
            spriteIndex = (spriteIndex + 1) % sprites.Length;
            material.SetTexture("_MainTex", sprites[spriteIndex]);
        }
	}
}
