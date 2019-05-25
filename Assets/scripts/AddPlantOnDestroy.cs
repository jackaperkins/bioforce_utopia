using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlantOnDestroy : MonoBehaviour {
    Material material;
    public Texture2D texture;
	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Destroyed()
    {
        //material.EnableKeyword("_DETAIL_MULX2");
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", new Color(0,0.15f,0));
        //material.SetTexture("_DetailAlbedoMap", texture);
        material.SetTexture("_MainTex", texture);
        material.SetColor("_Color", Color.white);
    }
}
