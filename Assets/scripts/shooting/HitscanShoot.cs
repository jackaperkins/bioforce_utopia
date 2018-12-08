using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitscanShoot : MonoBehaviour {
    public GameObject tempMonkey;
    public GameObject tempTree;

    public GameObject bulletUIPrefab;
    public RectTransform bulletFrame;
    public GameObject bulletsReloadingUI;

    public AudioClip shoot;
    public AudioClip reload;

    int maxBullets = 6;
    int currentBullets = 6;

    public GameObject gunShootBurst;

	// Use this for initialization
	void Start () {
        bulletsReloadingUI.SetActive(false);
	}



	
	// Update is called once per frame
	void Update () {
        if (bulletFrame.childCount > currentBullets)
        {
            Destroy(bulletFrame.GetChild(0).gameObject);
        } else if (bulletFrame.childCount < currentBullets) {
            Instantiate(bulletUIPrefab, bulletFrame); 
        }



        if (Input.GetMouseButtonDown(0) && AStageDirector.instance.inAction)
        {

            if (currentBullets <= 0)
            {
                AudioManager.PlaySound(reload); // click
            }
            else
            {
                currentBullets--;
                AudioManager.PlaySound(shoot);
                //shoot her


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Target")))
                {


                    GameObject g = hit.collider.gameObject;
                    IShootable shootable = g.GetComponent<IShootable>();
                    shootable.Shoot();//
                    //FlashScreen.instance.Flash();

                    Instantiate(gunShootBurst, Input.mousePosition, Quaternion.identity, UIRoot.root);
                    // spawn nature
                    GameObject bill = Instantiate(Random.value > 0.5f ? tempMonkey : tempTree, hit.point - 0.3f * Camera.main.transform.forward, Quaternion.identity);
                    bill.transform.SetParent(hit.collider.transform);
                }

                if (currentBullets <= 0)
                {
                    StartCoroutine(Reloading());
                }
            }
        } 

	}

    IEnumerator Reloading(){
        bulletsReloadingUI.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        currentBullets = maxBullets;
        bulletsReloadingUI.SetActive(false);
    }
}
