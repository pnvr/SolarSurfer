using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public GameObject nearestPlanet;
    public float distToPlanet;
    public GameObject[] planets;
    public GameObject player;


	void Start () {
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
                distToPlanet = shortestDist;
            }
        }

        //nearestPlanet = planets.Min(planet => Vector3.Distance(player.transform.position, planet.transform.position));
        nearestPlanet = planetCandidate;
	}
}
