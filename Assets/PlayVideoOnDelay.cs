using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideoOnDelay : MonoBehaviour
{
    public Image coverImage;
    public AudioSource soundz;

    public string nextSceneName;


    public IntroBlock[] blocks;
    public Text subtitles;
    public Text nextCursor;

    bool blinkCursor;
    float cursorTimer;

    public VideoPlayer vidPlayer;


    void Start()
    {
        StartCoroutine(PlayIt());
        subtitles.text = "";
        nextCursor.enabled = false;
    }

    IEnumerator PlayIt()
    {
        yield return new WaitForSeconds(0.5f);
        

        // load em


        for (int i = 0; i < blocks.Length; i++)
        {
  
           
            if(blocks[i].clip != null)
            {

                vidPlayer.clip = blocks[i].clip;
                vidPlayer.time = 0; // rewind
                vidPlayer.Play();
                subtitles.text = "";
                yield return new WaitForSeconds(0.1f);
                coverImage.enabled = false;
                yield return new WaitForSeconds(0.3f);
            }

            //reset
            subtitles.text = "";
            yield return new WaitForSeconds(0.2f);
           
            for(int k = 0; k < blocks[i].text.Length; k++)
            {
                subtitles.text = blocks[i].text.Substring(0, k+1);
                yield return new WaitForSeconds(0.02f);
            }

            // yield
            blinkCursor = true;
            nextCursor.enabled = false;
            cursorTimer = 1f;
            while (true)
            {
                if (Input.GetMouseButton(0))
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            blinkCursor = false;
            nextCursor.enabled = false;

        }

        subtitles.text = "";
        coverImage.enabled = true;

       yield return new WaitForSeconds(0.9f);
        if (nextSceneName.Length > 0)
        {
            Transition.hopScene = nextSceneName;
            SceneManager.LoadScene("fakeload");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(blinkCursor)
        {
            cursorTimer -= Time.deltaTime;
            if(cursorTimer < 0)
            {
                cursorTimer = 0.5f;
                nextCursor.enabled = !nextCursor.enabled;
            }
        }
    }
}
