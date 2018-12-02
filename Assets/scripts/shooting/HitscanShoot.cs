using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanShoot : MonoBehaviour {
    public GameObject tempMonkey;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot her
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Target")))
            {
                print("shooting a target");
                GameObject g = hit.collider.gameObject;
                IShootable shootable = g.GetComponent<IShootable>();
                shootable.Shoot();//
                FlashScreen.instance.Flash();
     

                // spawn nature
                GameObject bill = Instantiate(tempMonkey, hit.point + -1 * Camera.main.transform.forward, Quaternion.identity);
                bill.transform.SetParent(hit.collider.transform);
            }
        }
	
	}
}
