using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    public AudioClip clip;
	// Use this for initialization
	void Start () {
        AudioManager.PlaySound(clip);
	}
	

}
