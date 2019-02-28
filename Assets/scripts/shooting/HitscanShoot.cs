using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitscanShoot : MonoBehaviour {
    public SOBulletProfile defaultProfile;
    public SOBulletProfile currentProfile;

    float profileTimer;


    public AudioClip shoot;
    public AudioClip reload;

    public bool reloading;

    //int maxBullets = 6;
    public int currentBullets = 6;

    float bulletCooldown;

    public GameObject gunShootBurst;

	// Use this for initialization
	void Start () {
        LoadNewProfile(defaultProfile);
	}

	
	// Update is called once per frame
	void Update () {
        if(currentProfile != defaultProfile) {
            if(profileTimer > 0) {
                profileTimer -= Time.deltaTime;
            } else {
                LoadNewProfile(defaultProfile);
            }
        }

        if (!Input.GetMouseButton(0)){
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
        currentProfile = profile;
        currentBullets = currentProfile.maxAmmo;
        if(profile.announceUI != null) {
            Instantiate(profile.announceUI, UIRoot.root);
        }

        if(profile != defaultProfile) {
            profileTimer = profile.lifeTime;
        }
    }

    void FireShot(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Instantiate(gunShootBurst, Input.mousePosition, Quaternion.identity, UIRoot.root);
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask(new string[] { "Target", "Powerup" }))) {
            GameObject g = hit.collider.gameObject;
            IShootable[] shootables = g.GetComponentsInChildren<IShootable>();
            foreach (IShootable shootable in shootables) {
                shootable.Shoot();
            }

       
            // spawn nature

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target")) { 
                GameObject toSpawn = currentProfile.billboardPrefabs[(int)(Random.value * currentProfile.billboardPrefabs.Length)];
                GameObject bill = Instantiate(toSpawn, hit.point - 0.3f * Camera.main.transform.forward, Quaternion.identity);
                bill.transform.SetParent(hit.collider.transform);
            } else {
                PowerUp power = hit.collider.gameObject.GetComponent<PowerUp>();
                LoadNewProfile(power.bulletProfile);
                Destroy(hit.collider.gameObject);
           }
        }
    }

    IEnumerator Reloading(){
        reloading = true;
        yield return new WaitForSeconds(0.4f);

        reloading = false;
        currentBullets = currentProfile.maxAmmo;


    }
}
