using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlidein : MonoBehaviour {
    public float lifetime = 3;
    RectTransform rect;
    Vector2 origin;
    Vector2 outPosition;

    bool inOut;

	void Start () {
        rect = GetComponent<RectTransform>();
        origin = rect.anchoredPosition;

        Vector2 newPos = rect.anchoredPosition;
        newPos.x += 1000;

        outPosition = origin;
        outPosition.x -= 1000;
        rect.anchoredPosition = newPos;
	}

	void Update () {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            inOut = true;
        }

        if (inOut) {
            // slide out
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, outPosition, 3000 * Time.deltaTime);
            if(Vector2.Distance(rect.anchoredPosition, outPosition) < 5) {
                Destroy(gameObject);
            }
        } else {
            //s ide in
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, origin, 3000 * Time.deltaTime);
        }



    }
}
