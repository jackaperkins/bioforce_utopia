using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Transform> Children (){
        List<Transform> output = new List<Transform>();
        foreach(Transform child in transform) {
            output.Add(child);
        }
        return output;
    }

    public Transform GetNext(Transform current) {
        return null;
    }


    public Vector3 GetPoint(int index) {
        return transform.GetChild(index).position;
    }


    void OnDrawGizmos () {
        List<Transform> children = Children();
        for (int i = 0; i < children.Count-1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(children[i].position, children[i + 1].position);
        }
    }
}
