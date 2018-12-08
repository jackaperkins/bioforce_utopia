using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding : MonoBehaviour, IShootable, IDestructable {
    public int health = 5;
    public bool parent = true;

    public bool destroyed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool ShouldParent () {
        return parent;
    }

    public bool IsDestroyed () {
        return destroyed;
    }

    public void Shoot() {
        if(health > 0) {
            // peng peng
            health--;
            if (health <= 0){
                // DIE
                CameraShake.Shake(0.3f);
                FlashScreen.instance.Flash(2f); // 3 "frames"
                destroyed = true;
                print("was destroyed");
                gameObject.BroadcastMessage("Destroyed", SendMessageOptions.DontRequireReceiver);
            }
        } else {
            //dont do ntohign we dead bitch
        }
    }
}
