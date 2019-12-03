using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistOsillation : MonoBehaviour
{
    float rota;
    public float speed = 6;
    public float amplitude = 10f;
    float startAngle;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = transform.localEulerAngles.z;   
    }

    // Update is called once per frame
    void Update()
    {
        rota += Time.deltaTime * speed;
        transform.localEulerAngles = new Vector3(0,0,startAngle + Mathf.Sin(rota)*amplitude);
    }
}
