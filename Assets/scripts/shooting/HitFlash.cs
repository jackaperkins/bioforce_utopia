using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour {
    Material material;
    float flash = 0;
    Color flashColor;
	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        if (flash > 0){
            material.SetColor("_EmissionColor", flashColor);
            flashColor = Color.Lerp(flashColor, Color.black, Time.deltaTime * 10);
        }
	}

    public void Hit () {
        flash = 1;
        float h, s, v;
        Color.RGBToHSV(material.GetColor("_Color"), out h, out s, out v);
        material.EnableKeyword("_EMISSION");
        flashColor = Color.HSVToRGB(h,0.3f,1);
    }
}
