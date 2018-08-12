using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {
    float rotateSpeed = 20;
    float randomScale;
    int spinDir;

    Rigidbody player;

    private void Start() {
        randomScale = Random.Range(.75f, 3.5f);
        spinDir = Random.Range(0, 2) * 2 - 1;
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    void Update () {
        transform.Rotate(0, 0, rotateSpeed * 1/randomScale * spinDir * Time.deltaTime);
	}

    private void OnCollisionStay(Collision collision) {

    }

    private void OnTriggerStay(Collider other) {
        print("Vetovoiman pitäisi vaikuttaa");
        player = other.gameObject.GetComponent<Rigidbody>();
        float dist = Vector3.Distance(transform.position, player.transform.position);
       // player.AddForce(other.gameObject.transform.up * -dist, ForceMode.Acceleration);
        player.MovePosition(transform.position + transform.up * -dist * Time.deltaTime);
  
    }

}