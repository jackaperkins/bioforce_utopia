using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = transform.localScale * (0.9f + Random.value * .2f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = GameCamera.current.transform.rotation;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 3);
    }
}
