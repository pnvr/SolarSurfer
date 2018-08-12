using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    public GameManager gm;
    Rigidbody rb;
    public float playerRotationSpeed;
    public float speed;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            transform.parent = null;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            rb.position += transform.up * .15f;
            rb.velocity = transform.up * speed;
        }
    }

    private void FixedUpdate() {
        if(gm.nearestPlanet && rb) { 
            Vector3 rotationDirection = transform.position - gm.nearestPlanet.transform.position;
            Quaternion rotationGoal = Quaternion.LookRotation(Vector3.forward, rotationDirection);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, playerRotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        transform.parent = collision.transform;
        Destroy(rb);
    }
}
