using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ITriggerable
{
    MobileTarget[] destructables;
    // Start is called before the first frame update
    void Start()
    {
        destructables = GetComponentsInChildren<MobileTarget>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger () {
        foreach(MobileTarget d in destructables) {
            d.Trigger();
        }
    }
}
