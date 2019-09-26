using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlipbookFakeLoader : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameDelay;

    bool forward = true;

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
        if (Random.value > 0.4f)
        {
            timer += Time.deltaTime;
        }
        if(timer > frameDelay) {
            timer -= frameDelay;
            if (forward)
            {
                if(spriteIndex < sprites.Length -1)
                {
                    spriteIndex++;
                }
                else
                {
                    forward = false;
                }
            }
            else
            {
                if(spriteIndex > 0) {
                    spriteIndex--;
                }
                else
                {
                    forward = true;
                }
            }
        }
    }
}
