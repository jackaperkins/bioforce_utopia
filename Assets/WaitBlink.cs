using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBlink : MonoBehaviour
{
    float countdown;
    bool isEnabled;

    Transform child;


    private void Awake()
    {
        child = transform.GetChild(0);
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        countdown = 0.5f;
        isEnabled = true;
        child.gameObject.SetActive(isEnabled);
     
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            isEnabled = !isEnabled;
            if(isEnabled)
            {
                countdown = 0.5f;
            }
            else
            {
                countdown = 0.25f;
            }
            child.gameObject.SetActive(isEnabled);
        }
    }
}
