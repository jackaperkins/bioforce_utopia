using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaleInOut : MonoBehaviour
{
    public float speed = 8f;
    public float holdTime = 1.5f;


    Vector3 originScale;
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        originScale = rect.localScale;
        rect.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        holdTime -= Time.deltaTime;

        if(holdTime > 0) {
            rect.localScale = Vector3.Lerp(rect.localScale, originScale, Time.deltaTime * speed);
        } else {
            rect.localScale = Vector3.Lerp(rect.localScale, Vector3.zero, Time.deltaTime * speed);
        }
    }
}
