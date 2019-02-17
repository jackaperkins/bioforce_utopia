using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitscanShoot : MonoBehaviour {
    public SOBulletProfile currentProfile;

    public GameObject tempMonkey;
    public GameObject tempTree;

    public GameObject bulletUIPrefab;
    public RectTransform bulletFrame;
    public GameObject bulletsReloadingUI;

    public AudioClip shoot;
    public AudioClip reload;

    //int maxBullets = 6;
    int currentBullets = 6;

    float bulletCooldown;

    public GameObject gunShootBurst;

	// Use this for initialization
	void Start () {
        bulletsReloadingUI.SetActive(false);
        LoadNewProfile(currentProfile);
	}



	
	// Update is called once per frame
	void Update () {
        for (int x = 0; x < 5; x++) {
            if (bulletFrame.childCount > currentBullets) {
                Destroy(bulletFrame.GetChild(0).gameObject);
            } else if (bulletFrame.childCount < currentBullets) {
                Instantiate(bulletUIPrefab, bulletFrame);
            } else {
                break;
            }
        }

        if(!Input.GetMouseButton(0)){
            bulletCooldown = currentProfile.repeateRate + 0.1f;
        }

        if (Input.GetMouseButton(0) && AStageDirector.instance.inAction)
        {
            if (bulletCooldown < currentProfile.repeateRate) {
                bulletCooldown += Time.deltaTime;
            } else {
                if (currentBullets <= 0) {
                    AudioManager.PlaySound(reload); // click
                } else {
                    currentBullets--;
                    AudioManager.PlaySound(shoot);
                    //shoot her

                    bulletCooldown = 0;

                    FireShot();

                    if (currentBullets <= 0) {
                        StartCoroutine(Reloading());
                    }
                }
            }
        } 

	}

    void LoadNewProfile (SOBulletProfile profile){
        HorizontalLayoutGroup layout = bulletFrame.GetComponent<HorizontalLayoutGroup>();

        currentProfile = profile;
        RectTransform prefabTransform = bulletUIPrefab.GetComponent<RectTransform>();
        Vector2 scale = prefabTransform.sizeDelta;
        //Debug.Log(bulletFrame.rect.width);
        scale.x = (bulletFrame.rect.width / (float)profile.maxAmmo) - layout.spacing;
        //scale.x = 10;
        //scale.y = 30;
        prefabTransform.sizeDelta = scale;
        currentBullets = currentProfile.maxAmmo;

    }

    void FireShot(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Target"))) {  
            GameObject g = hit.collider.gameObject; 
            IShootable[] shootables = g.GetComponentsInChildren<IShootable>();
            foreach (IShootable shootable in shootables) {
                shootable.Shoot();//
            }
                              //FlashScreen.instance.Flash();

            Instantiate(gunShootBurst, Input.mousePosition, Quaternion.identity, UIRoot.root);
            // spawn nature
            GameObject toSpawn = currentProfile.billboardPrefabs[(int)(Random.value * currentProfile.billboardPrefabs.Length)];
            GameObject bill = Instantiate(toSpawn, hit.point - 0.3f * Camera.main.transform.forward, Quaternion.identity);
            bill.transform.SetParent(hit.collider.transform);
        }
    }

    IEnumerator Reloading(){
        bulletsReloadingUI.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        currentBullets = currentProfile.maxAmmo;
        bulletsReloadingUI.SetActive(false);

    }
}
