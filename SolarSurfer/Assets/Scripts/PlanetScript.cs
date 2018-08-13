using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {
    float rotateSpeed = 100;
    float randScale;
    public float gravity;
    bool beginDestruction = false;
    float destTimer = 10;
    int spinDir;
    Rigidbody rb;
    Rigidbody playerRb;
    Vector3 eulerAngleVelocity;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        randScale = Random.Range(.75f, 3.5f);
        spinDir = Random.Range(0, 2) * 2 - 1;
        transform.localScale = new Vector3(randScale, randScale, randScale);
        eulerAngleVelocity = new Vector3(0, 0, rotateSpeed * 1 / randScale * spinDir);
    }

    private void Update() {
        if(beginDestruction) {
            destTimer -= Time.deltaTime;
            if(destTimer < 0) {
                gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate() {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCharacter") {
            playerRb = other.gameObject.GetComponent<Rigidbody>();
            if(!playerRb) {
                return;
            }
            float dist = Vector3.Distance(transform.position, playerRb.transform.position);
            playerRb.AddForce((transform.position - playerRb.position) * (1 / dist) * gravity * (randScale / 2), ForceMode.Acceleration);
        }

    }

    public void PlanetDestruction() {
        beginDestruction = true;
    }

}