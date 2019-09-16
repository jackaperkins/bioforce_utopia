using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingOnShoot : MonoBehaviour,IShootable
{
    public Vector3 maxAngleLean;

    Vector3 currentAngle;

    Vector3 originAngle;

    // Start is called before the first frame update
    void Start()
    {
        originAngle = transform.localEulerAngles;
        currentAngle = originAngle;
    }

    public void Shoot () {
        currentAngle = originAngle + maxAngleLean * ((Random.value > 0.5f) ? -1 : 1);   
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = Vector3.Lerp(currentAngle, originAngle, Time.deltaTime * 3);
        transform.localEulerAngles = currentAngle;
    }
}
