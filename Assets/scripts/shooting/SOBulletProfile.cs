using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Bullet Profile")]
public class SOBulletProfile : ScriptableObject {
    public int maxAmmo = 6;
    public float repeateRate = 0.4f;
    public float lifeTime = -1; //
    public GameObject announceUI;
    public AudioClip announceSound;
    public GameObject[] billboardPrefabs;
}
