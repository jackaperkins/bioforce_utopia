using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnSurface : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int number = 80;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjectOnMesh(gameObject, prefabToSpawn, number);
    }


    public static void SpawnObjectOnMesh(GameObject target, GameObject toSpawn, int number)
    {
        Mesh mesh = target.GetComponent<MeshFilter>().mesh;

        // because we think that the array of all verts in a mesh are ordered in some kind of 
        // logical computer brain layout, we sample points uniformly across the array to get our locations
        int skipStep = mesh.vertices.Length / number;
        if (mesh.vertices.Length < number)
        {
            skipStep = 1;
        }

        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < mesh.vertices.Length; i += skipStep)
        {
            Vector3 point = mesh.vertices[i];

            GameObject g = Instantiate(toSpawn, target.transform.TransformPoint(point), Quaternion.identity);
            g.transform.SetParent(target.transform);
            g.transform.localScale *= Random.Range(0.8f, 1.2f);

            Vector3 away = g.transform.position - target.transform.position;

            g.transform.LookAt(g.transform.position + away);
        }
    }
}
