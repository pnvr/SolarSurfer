using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    public GameManager gm;
    public Rigidbody rb;
    public float playerRotationSpeed;
    public float speed;

	void Update () {

        if(transform.parent == null) {
            Vector3 rotationDirection = transform.position - gm.nearestPlanet.transform.position;
            Quaternion rotationGoal = Quaternion.LookRotation(Vector3.forward, rotationDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, playerRotationSpeed * 1/gm.distToPlanet * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            transform.parent = null;
            rb.velocity = transform.up * speed;
            
            //rb.AddForce(transform.up, ForceMode.Acceleration);
        }
	}

    private void OnCollisionEnter(Collision collision) {
        transform.parent = collision.transform;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
}
