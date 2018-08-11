using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameManager gm;
    public GameObject player;
    public float camDist;
    public float camSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player.transform.parent == null) {
            Vector3 playerPos = new Vector3(player.transform.position.x, 0, camDist);
            transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * camSpeed);
        } else {
            Vector3 planetPos = new Vector3(gm.nearestPlanet.transform.position.x, 0, camDist);
            transform.position = Vector3.MoveTowards(transform.position, planetPos, Time.deltaTime * gm.distToPlanet * camSpeed/5);
        }
        
	}
}
