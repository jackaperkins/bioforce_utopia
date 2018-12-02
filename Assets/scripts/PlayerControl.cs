using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject[] smallObjects;
    public GameObject[] trees;

    Vector3 twist;

	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
        //twist.y += (((Input.mousePosition.x / Screen.width) - 0.5f) * 40 - twist.y) * Time.deltaTime * 4;
        //transform.localEulerAngles = twist;

        if(Input.GetMouseButton(0)) {
            if(Random.value > 0.2f)
            for (int i = 0; i < 1; i++)
            {
                   
                Vector3 vector = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition + vector);
                    Debug.DrawRay(r.origin, r.direction*20, Color.red, 0.2f);
                RaycastHit hit;
                if (Physics.Raycast(r, out hit, 300, LayerMask.GetMask("Target")))
                //if (Physics.Raycast(r, out hit, 100))
                {
                    print("hit!!");
                    GameObject toSpawn = smallObjects[Random.Range(0, smallObjects.Length)];
                    if (Random.value > 0.8f)
                    {
                        toSpawn = trees[Random.Range(0, trees.Length)];
                    }  

                    GameObject bill = (GameObject) Instantiate(toSpawn, hit.point, Quaternion.identity);
                    bill.transform.SetParent(hit.collider.transform);

                }
            }
        }
    }

}