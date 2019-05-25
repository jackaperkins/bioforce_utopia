using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnDestroyed : MonoBehaviour
{
    public Vector3 toRotate;
    public bool randomSign;

    // Start is called before the first frame update
    public void Destroyed()
    {
        if(randomSign && Random.value > 0.5f){
            toRotate *= -1;
        }
        transform.Rotate(toRotate);

    }
}
