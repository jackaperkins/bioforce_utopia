using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuilding : MonoBehaviour, IShootable {
    public int health = 5;
    public bool parent = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool ShouldParent () {
        return parent;
    }

    public void Shoot() {
        print("got shot");
        if(health > 0) {
            // peng peng
            health--;
            if (health <= 0){
                // DIE
                CameraShake.Shake(0.3f);
                gameObject.SendMessage("Destroyed", SendMessageOptions.DontRequireReceiver);
            }
        } else {
            //dont do ntohign we dead bitch
        }
    }
}
