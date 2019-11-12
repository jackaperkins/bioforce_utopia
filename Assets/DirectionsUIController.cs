using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DirectionsUIController : MonoBehaviour
{

    public Sprite[] sprites;
    public string[] strings;

    public Image image;
    public Text text;
    public CanvasGroup group;

    bool fadingOut = true;

    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        group.alpha = 0;
        StartCoroutine(NextSlide());
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingOut)
        {
            group.alpha -= Time.deltaTime * 1.5f;
        }
        else
        {
            group.alpha += Time.deltaTime * 1.5f;
        }

        if (Input.GetMouseButtonDown(0) && !fadingOut)
        {
            index++;

            if(index < sprites.Length)
            {
                StartCoroutine(NextSlide());
            }
            else
            {
                fadingOut = true;
                Invoke("LoadScene", 1.5f);
            }
        }
    }

    void LoadScene ()
    {
        Transition.hopScene = "level 1";
        SceneManager.LoadScene("fakeload");
    }


    IEnumerator NextSlide ()
    {
        yield return new WaitForEndOfFrame();
        fadingOut = true;
        while(group.alpha > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        image.sprite = sprites[index];
        text.text = strings[index];

        yield return new WaitForEndOfFrame();

        fadingOut = false;

        
    }

}
