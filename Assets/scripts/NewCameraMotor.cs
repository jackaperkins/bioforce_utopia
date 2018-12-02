using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraMotor : MonoBehaviour {
    public float moveWidth = 10;
    public float moveHeight = 8;


    float mouseEdgeLimit = 0.15f; //what percent of the width we consider 100% of the width, so player doesnt need to move mouse all the way to the end
    float maxSpeed = 15f;

    float shootTimer = 0;

    public GameObject playerRig;
    public Camera theCamera;
    Vector3 lastPosition;
    Vector3 lookLerp;

    public GameObject laserPrefab;
    
    public GameObject monkeyPrefab;
    public GameObject treePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // move the whole box forward
        transform.Translate(0, 0, maxSpeed *Time.deltaTime);
        Vector3 halfExtents = new Vector3(moveWidth/2.0f, moveHeight/2.0f, 0);

        Vector3 pos = RelativeMousePosition();
        pos.y =  Mathf.Pow(pos.y, 3); // bias towards the bottom of the screen with pow^2 curve
        //pos.x = pos.x;
        //pos.y = pos.y;
        pos.Scale(halfExtents*2);

        //lastPosition += (pos - lastPosition) * 8 * Time.deltaTime;
        Vector3 goal = transform.position - halfExtents + pos;
        float dist = Vector3.Distance(playerRig.transform.position, goal);
        
        playerRig.transform.position = Vector3.MoveTowards(playerRig.transform.position, goal, dist*Time.deltaTime);
        playerRig.transform.eulerAngles = new Vector3(0,RelativeMousePosition().y,0);

        lookLerp += ((transform.position + (-halfExtents + pos) * 0.25f) -lookLerp)*Time.deltaTime*8;
        theCamera.transform.LookAt(lookLerp);

        // shoot
        shootTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && shootTimer < 0)
        {
            shootTimer = 0.2f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject g = (GameObject)Instantiate(laserPrefab, playerRig.transform.position, Quaternion.identity);
            Laser laser = g.GetComponent<Laser>();
            if (Random.value > 0.5f)
            {
                laser.Setup(ray.direction, monkeyPrefab);
            } else {
                laser.Setup(ray.direction, treePrefab);
            }
        }
	}

    // returns 0-1 range of mouse on screen, with edge limits
    Vector3 RelativeMousePosition () {
        Vector3 pos = Input.mousePosition;
        float safeZone = 1 - 2 * mouseEdgeLimit;  // 0.7 if edge limit is 0.15
        pos.x /= Screen.width;
        pos.y /= Screen.height;

        pos.x = Mathf.Clamp01((pos.x - mouseEdgeLimit) / safeZone);
        pos.y = Mathf.Clamp01((pos.y - mouseEdgeLimit) / safeZone);

        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 sizeCube = new Vector3(moveWidth, moveHeight, 100);
        Gizmos.DrawWireCube(transform.position + Vector3.forward * (sizeCube.z / 2.0f), sizeCube);
    }
}
