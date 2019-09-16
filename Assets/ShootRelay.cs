using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRelay : MonoBehaviour, IShootable
{

    void Start()
    {

    }

    public void Shoot ()
    {
        //
        SendMessageUpwards("GetShot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
