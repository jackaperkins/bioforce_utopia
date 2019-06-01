using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlipbook : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameDelay;

    int spriteIndex;
    float timer;
    Image spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.sprite = sprites[spriteIndex];
        spriteRenderer.sprite = sprites[spriteIndex];

        spriteRenderer.color = Color.white;
        timer += Time.deltaTime;
        if(timer > frameDelay) {
            timer -= frameDelay;
            spriteIndex = (spriteIndex + 1) % sprites.Length;
        }
    }
}
