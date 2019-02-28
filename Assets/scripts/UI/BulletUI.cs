using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour {
    HitscanShoot shooter;

    public GameObject bulletUIPrefab;
    public RectTransform bulletFrame;
    public GameObject bulletsReloadingUI;

    int maxAmmo = 0;
    int currentShots = -1;

    // Use this for initialization
    void Start () {
        shooter = GetComponent<HitscanShoot>();
        bulletsReloadingUI.SetActive(false);

        RespawnUIElements();

    }
	
	// Update is called once per frame
	void Update () {
        bulletsReloadingUI.SetActive(shooter.reloading);

        if(shooter.currentProfile.maxAmmo != maxAmmo) {
            maxAmmo = shooter.currentProfile.maxAmmo;
            RespawnUIElements();
        }

        if (currentShots != shooter.currentBullets) {
            currentShots = shooter.currentBullets;
            // count backwards (last child first as they're right alligned
            for (int i = bulletFrame.childCount -1; i >= 0; i--) {
                bulletFrame.GetChild(i).GetComponentInChildren<Image>().enabled = ((bulletFrame.childCount - i -1) < currentShots);
            }
        }
    }

    void RespawnUIElements () {
        // kill em all
        for(int i = bulletFrame.transform.childCount-1; i >=0; i--) {
            Destroy(bulletFrame.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < shooter.currentProfile.maxAmmo; i++) {
            GameObject b = Instantiate(bulletUIPrefab, bulletFrame);
            b.GetComponentInChildren<LoopAnimateSprite>().index = i;
        }

        HorizontalLayoutGroup layout = bulletFrame.GetComponent<HorizontalLayoutGroup>();

        RectTransform prefabTransform = bulletUIPrefab.GetComponent<RectTransform>();
        Vector2 scale = prefabTransform.sizeDelta;

        scale.x = (bulletFrame.rect.width / (float)shooter.currentProfile.maxAmmo) - layout.spacing;

        scale.x = Mathf.Min(scale.x, 80);

        foreach (RectTransform rect in bulletFrame.transform) {
            rect.sizeDelta = scale;
        }
    }
}
