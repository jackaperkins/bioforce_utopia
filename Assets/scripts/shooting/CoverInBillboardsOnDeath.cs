using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverInBillboardsOnDeath : MonoBehaviour {
    public GameObject billboardPrefab;
    public int desiredPoints = 25;

    List<Vector3> points;

    Mesh mesh;
	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;


        // because we think that the array of all verts in a mesh are ordered in some kind of 
        // logical computer brain layout, we sample points uniformly across the array to get our locations
        int skipStep = mesh.vertices.Length / desiredPoints;
        if(mesh.vertices.Length < desiredPoints) {
            skipStep = 1;
        }

        points = new List<Vector3>();
        for (int i = 0; i < mesh.vertices.Length; i += skipStep) {
            points.Add(mesh.vertices[i]);
        }
    }
	
    public void Destroyed() {
        StartCoroutine(Spawn());
    }



    // spawn the billboards on the selected points over a few frames for cool effect
    IEnumerator Spawn () {
        yield return new WaitForSeconds(0.026f);
        if (points.Count <= 0) yield break;

        int index = Random.Range(0, points.Count);
        Vector3 point = points[index];
        points.RemoveAt(index);

        GameObject g = Instantiate(billboardPrefab, transform.TransformPoint(point), Quaternion.identity);
        //g.transform.localPosition
        g.transform.SetParent(transform);
        g.transform.localScale *= Random.Range(0.8f, 1.2f);

        StartCoroutine(Spawn());
    }
}
