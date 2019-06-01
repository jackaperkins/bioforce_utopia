using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public static void PlaySound(AudioClip clip) {
        GameObject g = new GameObject();
        AudioSource audio = g.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        LifeTimer timer = g.AddComponent<LifeTimer>();
        timer.timer = clip.length;
    }
	// Update is called once per frame
	void Update () {
		
	}


}
