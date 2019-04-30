using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTarget : MonoBehaviour, IDestructable
{

    bool triggered;



    Vector3 originSize;
    // Start is called before the first frame update
    void Start()
    {
        originSize = transform.localScale;
        transform.localScale = Vector3.zero;
    
    }

    public bool IsDestroyed () {
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered) {
            return;
        }

        transform.localScale = Vector3.MoveTowards(transform.localScale, originSize, Time.deltaTime * 4);

        transform.Translate(transform.forward * 20 * Time.deltaTime);
        transform.Rotate(new Vector3(0,Random.Range(-3f, 3f),0), Space.World);
        transform.Rotate(new Vector3(7f*Time.deltaTime,0,0), Space.Self);
    }

    public void Trigger () {
        triggered = true;
    }
}
