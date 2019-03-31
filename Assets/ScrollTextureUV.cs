using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTextureUV : MonoBehaviour {
    public string textureName = "_MainTex";
    public Vector2 speed;

    Vector2 current;

    Material material;
	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        current += speed * Time.deltaTime;
        material.SetTextureOffset(textureName, current);
	}
}
