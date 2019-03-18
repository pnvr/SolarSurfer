using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour {
    public GameObject nearestPlanet;
    public string thrustEndAudio;
    public float distToPlanet;
    public GameObject[] planets;
    public GameObject player;
    public GameObject planetFolder;
    public List<GameObject> planetPrefabs;
    public GameObject earth;
    public TextMeshProUGUI statusText;
    float textTimer = 5;
    public bool gameOver = false;

	void Awake () {
        for(int i = 0; i < 25; i++) {
            if(i < 24) {
                var prefab = planetPrefabs[Random.Range(0, planetPrefabs.Count)];
                Vector3 newPos = new Vector3(i * 75 + Random.Range(10f, 20f), Random.Range(-40, 40), 0);
                GameObject planet = Instantiate(prefab);
                planet.transform.position = newPos;
                planet.transform.parent = planetFolder.transform;
            } else {
                GameObject planet = Instantiate(earth);
                Vector3 newPos = new Vector3(i * 77, 0, 0);
                planet.transform.position = newPos;
            }

        }
        planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	void Update () {
        textTimer += Time.deltaTime;

        if(!gameOver) {
            if(textTimer > 20) {
                statusText.text = "";
            } else if(textTimer > 12) {
                statusText.text = "";
            } else if(textTimer > 8.8f) {
                statusText.text = "- A Long Way From Home -";
            } else if(textTimer > 8) {
                statusText.text = "";
            } else if(textTimer > 0) {
                statusText.text = "Solar Surfer";
            }
        }

        float shortestDist = float.MaxValue;
        GameObject planetCandidate = null;
        foreach(GameObject planet in planets) {
            if(planet.GetComponent<SphereCollider>()) {
                float radius = planet.GetComponent<SphereCollider>().radius;
                var dist = Vector3.Distance(player.transform.position, planet.transform.position) - radius;

                if(dist < shortestDist) {
                    planetCandidate = planet;
                    shortestDist = dist;
                    distToPlanet = Mathf.Max(1f, shortestDist);
                }
            }
        }
        nearestPlanet = planetCandidate;
	}

    public void GameOver() {
        Time.timeScale = 0;
        statusText.text = "Game over\n \n- Tap to retry! -";
        Fabric.EventManager.Instance.PostEvent(thrustEndAudio);
        gameOver = true;
    }

    public void GameWon() {
        statusText.text = "This is the last livable planet in the universe, please take good care of it!\n\n - Tap to go at it again! -";
        gameOver = true;
    }
}
