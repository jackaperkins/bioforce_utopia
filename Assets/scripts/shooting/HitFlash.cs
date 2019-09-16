using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour, IShootable {
    Material material;
    float flash = 0;
    Color originColor;
    Color flashColor;
    bool isDead;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
	}

    public void Destroyed()
    {
        isDead = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (flash > 0){

            // decrement clamp
            flash -= Time.deltaTime * 6;
            flash = Mathf.Clamp01(flash);
  
            material.SetColor("_EmissionColor", Color.Lerp(flashColor, originColor, (float)(1 - flash)));
            if(flash <= 0) {
                material.DisableKeyword("_EMISSION");
            }
        }
	}

    public void Shoot () {
        if(isDead){
            return;
        }
        flash = 1;  
        float h, s, v;
        material.EnableKeyword("_EMISSION");
        Color.RGBToHSV(material.GetColor("_EmissionColor"), out h, out s, out v);
        originColor = Color.HSVToRGB(h, s, v);

        flashColor = Color.HSVToRGB(h,0.3f,1);
    }
}
