using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperScript : MonoBehaviour {

    public GameObject player;
    void Update() {
        Vector3 newPos = new Vector3(player.transform.position.x - 250, 0, 0);
        transform.position = newPos;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Planet") {
            if(other.gameObject.name == "PlanetGreen2(Clone)") {

            } else { other.gameObject.SetActive(false); }
        }

    }
}
