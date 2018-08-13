using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {
    float rotateSpeed = 100;
    float randScale;
    public float gravity;
    bool beginDestruction = false;
    bool destroyed = false;
    float destTimer = 10;
    int spinDir;
    Rigidbody rb;
    GameObject player;
    Rigidbody playerRb;
    Vector3 eulerAngleVelocity;
    SpriteRenderer sr;
    public Sprite Murut;

    private void Start() {
        player = GameObject.Find("PlayerCharacter");
        rb = GetComponent<Rigidbody>();
        randScale = Random.Range(.75f, 3.5f);
        spinDir = Random.Range(0, 2) * 2 - 1;
        transform.localScale = new Vector3(randScale, randScale, randScale);
        eulerAngleVelocity = new Vector3(0, 0, rotateSpeed * 1 / randScale * spinDir);
    }

    private void Update() {
        if(beginDestruction) {
            destTimer -= Time.deltaTime;
            // Tähän animaatio presupernova
            if(destTimer < 0) {
                var sc = gameObject.GetComponent<SphereCollider>();
                var cc = gameObject.GetComponent<CapsuleCollider>();
                // Tähän supernova ennen Fabricia
                // Tähän myös murut ja colliderien ja rigidbodyn poisto
                sr = GetComponent<SpriteRenderer >();
                sr.sprite = Murut;
                //Destroy(sc);
                Destroy(cc);
                if(!destroyed) {
                    destroyed = true;
                    if(player.transform.parent != null) {
                        player.GetComponent<PlayerCharacter>().PlayerWithoutAPlanet();
                    }
                    
                }
                //gameObject.SetActive(false); // Tämä pois kun edelliset tehty
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