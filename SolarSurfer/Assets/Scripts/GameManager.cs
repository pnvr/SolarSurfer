using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public GameObject nearestPlanet;
    public float distToPlanet;
    public GameObject[] planets;
    public GameObject player;
    public GameObject planetFolder;
    public List<GameObject> planetPrefabs;

	void Awake () {
        for (int i = 0; i < 10; i++) {
            var prefab = planetPrefabs[Random.Range(0, planetPrefabs.Count)];
            Vector3 newPos = new Vector3(i * 75 + Random.Range(5f, 10f), Random.Range(-25, 25), 0);
            GameObject planet = Instantiate(prefab);
            planet.transform.position = newPos;
            planet.transform.parent = planetFolder.transform;
        }
        planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	void Update () {
        float shortestDist = float.MaxValue;
        GameObject planetCandidate = null;
        foreach(GameObject planet in planets) {
            float radius = planet.GetComponent<SphereCollider>().radius;
            var dist = Vector3.Distance(player.transform.position, planet.transform.position) - radius;

            if(dist < shortestDist) {
                planetCandidate = planet;
                shortestDist = dist;
                distToPlanet = Mathf.Max(1f,shortestDist);
            }
        }
        nearestPlanet = planetCandidate;
	}
}
