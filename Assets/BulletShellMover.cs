using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellMover : MonoBehaviour
{
    float xSpeed, ySpeed;
    RectTransform rect;


    void Start()
    {
        rect = GetComponent<RectTransform>();
        ySpeed = Random.Range(2450, 3000);
        xSpeed = Random.Range(-650, -1200);
        
    }

    void Update()
    {
        ySpeed -= 12000 * Time.deltaTime;
        rect.anchoredPosition += new Vector2(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime);

        float scale = rect.localScale.x;
        scale += 0.5f * Time.deltaTime;
        scale = Mathf.Clamp(scale, 0, 1.5f);
        rect.localScale = new Vector2(scale, scale);

        if(rect.anchoredPosition.y < -200) {
            Destroy(gameObject);
        }
    }
}
