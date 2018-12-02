using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplinePoint))]
public class SplinePointEditor : Editor {

    private void OnSceneGUI()
    {

        Event current = Event.current;
        if(current.type == EventType.KeyDown) {
            if(current.keyCode == KeyCode.N) {
                Transform transform = ((SplinePoint) target).transform;
              
                InsertNext(transform);
            }
        }
    }

    void InsertNext (Transform current) {
        Debug.Log("searching");

        List<Transform> children = new List<Transform>();
        foreach (Transform child in current.parent)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++){
            if (children[i] == current)
            {
                Debug.Log("I FOUND MYSELF,, index  = " + i);


                Vector3 position = new Vector3(0, 0, 0);
                if (i < children.Count - 1)
                {
                    position = Vector3.Lerp(current.position, children[i+1].position, 0.5f);
                } else {
                    if (i > 0)
                    {
                        position = current.position + (current.position - children[i - 1].position);

                    } else {
                        position = current.position + Vector3.forward * 5;
                    }
                }
                CreatePoint(position, i + 1);
                return;
            }
        }
    }

    void CreatePoint(Vector3 position, int index) {
        Transform parent = ((SplinePoint)target).transform.parent;

        GameObject newbie = new GameObject("spline point");
        newbie.transform.position = position;
        newbie.transform.SetParent(parent);
        newbie.transform.SetSiblingIndex(index);
        newbie.AddComponent<SplinePoint>();
        Undo.RegisterCreatedObjectUndo(newbie, "Create spline point");
    }
}
