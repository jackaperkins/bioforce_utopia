﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitscanShoot : MonoBehaviour {
    public SOBulletProfile defaultProfile;
    public SOBulletProfile currentProfile;

    float profileTimer;

    Vector2 originPosition;
    RectTransform thisTransform;

    public AudioClip shoot;
    public AudioClip reload;
    public AudioClip richocet1;
    public AudioClip richocet2;

    public bool reloading;

    public RectTransform crosshair;

    //int maxBullets = 6;
    public int currentBullets = 6;

    float bulletCooldown;

    public GameObject gunShootBurst;

	// Use this for initialization
	void Start () {
        LoadNewProfile(defaultProfile);
        thisTransform = GetComponent<RectTransform>();
        originPosition = thisTransform.anchoredPosition;
        thisTransform.anchoredPosition = originPosition + Vector2.down * 300;

    }


    // Update is called once per frame
    void Update () {
        Cursor.visible = false;
        if (!AStageDirector.instance.inAction)
        {
            //hide
            thisTransform.anchoredPosition = Vector2.MoveTowards(thisTransform.anchoredPosition, originPosition + Vector2.down * 300, 900*Time.deltaTime);
        }
        else
        {
            thisTransform.anchoredPosition = Vector2.MoveTowards(thisTransform.anchoredPosition, originPosition, 1200 * Time.deltaTime);
        }

        if (!AStageDirector.instance.inAction) {
            currentBullets = currentProfile.maxAmmo;

            if(crosshair.gameObject.activeInHierarchy){
                crosshair.gameObject.SetActive(false);
              
            }
           
        } else {
            
            if (!crosshair.gameObject.activeInHierarchy){
                crosshair.gameObject.SetActive(true);
               
            }
            crosshair.anchoredPosition = GetScreenToUIPosition();

        }

        if (currentProfile != defaultProfile) {
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
                    if (Input.GetMouseButtonDown(0))
                    {
                        AudioManager.PlaySound(reload); // click
                    }
                } else {
                    currentBullets--;
                   
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

    Vector2 GetScreenToUIPosition () {
        Vector2 uiPos = new Vector2(Input.mousePosition.x / ((float)Screen.width), Input.mousePosition.y / ((float)Screen.height));

        uiPos *= UIRoot.rootRect.rect.size;
        return uiPos;
    }

    void SpawnShootFlash(){

        Vector2 uiPos = GetScreenToUIPosition();


        // do we need to convert input mouseposition to screenspace... yes?
        GameObject shootEffect = (GameObject)Instantiate(gunShootBurst, UIRoot.root);
        RectTransform effectTransform = shootEffect.GetComponent<RectTransform>();
        effectTransform.anchoredPosition = uiPos;
    
    }

    void FireShot(){
        // convert main cam to render cam space;
        Vector2 pos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.origin + 50 * ray.direction, Color.red, 1f);


        SpawnShootFlash();


        if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask(new string[] { "Target", "Powerup" }))) {
            GameObject g = hit.collider.gameObject;
       
            IShootable[] shootables = g.GetComponentsInChildren<IShootable>();
            foreach (IShootable shootable in shootables) {
                shootable.Shoot();

            }
  
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target")) {
                GameObject toSpawn = currentProfile.billboardPrefabs[(int)(Random.value * currentProfile.billboardPrefabs.Length)];
                GameObject bill = Instantiate(toSpawn, hit.point - 0.3f * Camera.main.transform.forward, Quaternion.identity);
                bill.transform.SetParent(hit.collider.transform);
            } else {
                PowerUp power = hit.collider.gameObject.GetComponent<PowerUp>();
                LoadNewProfile(power.bulletProfile);
                Destroy(hit.collider.gameObject);
           }
            AudioManager.PlaySound(shoot);
        } else {
            AudioManager.PlaySound(shoot);
            if(Random.value > 0.5f) {
                AudioManager.PlaySound(richocet1);
            } else {
                AudioManager.PlaySound(richocet2);
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
