using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour {
    public GameManager gm;
    Rigidbody rb;
    public float playerRotationSpeed;
    public float speed;
    public string jumpAudio;
    public string thrustAudio;
    public string thrustEndAudio;
    public string landAudio;
    public string stopAudio;
    float waitTimer = 1;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        rb.velocity = transform.up * speed;
    }

    private void Update() {

        if(!gm.gameOver) {
            if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space) && rb) {
                Fabric.EventManager.Instance.PostEvent(thrustAudio);

                var anim = GetComponent<Animator>();
                anim.Play("AstronautJetAnimation");

                PlayerWithoutAPlanet();
            }
            if(Input.GetKeyUp(KeyCode.Mouse0)) {
                Fabric.EventManager.Instance.PostEvent(thrustEndAudio);
            }
        } else {
            waitTimer -= Time.unscaledDeltaTime;
            if(waitTimer < 0) {
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    Time.timeScale = 1;
                    Fabric.EventManager.Instance.PostEvent(stopAudio);
                    SceneManager.LoadScene(0);
                }
            }

        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

    }

    private void FixedUpdate() {
        if(!gm.gameOver) {
            if(Input.GetKey(KeyCode.Mouse0) && rb || Input.GetKey(KeyCode.Space) && rb) {
                rb.AddForce(transform.up * speed, ForceMode.Acceleration);
                var anim = GetComponent<Animator>();
                anim.Play("AstronautJetAnimation");
            }
        }
        if(gm.nearestPlanet && rb) {
            Vector3 rotationDirection = transform.position - gm.nearestPlanet.transform.position;
            Quaternion rotationGoal = Quaternion.LookRotation(Vector3.forward, rotationDirection);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, playerRotationSpeed * 1 / gm.distToPlanet * Time.deltaTime);
        }
        
    }

    public void PlayerWithoutAPlanet() {
        if(transform.parent != null) {  // if (!(transform.parent == null))
            transform.parent = null;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            rb.position += transform.up * .25f;
            rb.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Planet") {
            transform.parent = collision.transform;
            Destroy(rb);

            if(collision.gameObject.name != "PlanetGreen2(Clone)") {
                collision.gameObject.GetComponent<PlanetScript>().PlanetDestruction();
            } 
            
            Fabric.EventManager.Instance.PostEvent(landAudio);
        }

        if(collision.gameObject.name == "PlanetGreen2(Clone)") {
            transform.parent = collision.transform;
            Destroy(rb);
            Fabric.EventManager.Instance.PostEvent(landAudio);
            gm.GameWon();

        }
    }
}
