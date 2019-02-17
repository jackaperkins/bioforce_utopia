using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour, IShootable {
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

    public void Shoot () {
        flash = 1;
        float h, s, v;
        material.EnableKeyword("_EMISSION");
        Color.RGBToHSV(material.GetColor("_Color"), out h, out s, out v);
        flashColor = Color.HSVToRGB(h,0.3f,1);
    }
}
