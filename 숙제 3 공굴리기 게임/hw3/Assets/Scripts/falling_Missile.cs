using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_Missile : MonoBehaviour {

	public GameObject missile_prefab;
	List<GameObject> missile_list = new List<GameObject>();
	public float spawn_time;

	// Use this for initialization
	void Start () {
		InvokeRepeating("spawn_missile",spawn_time,spawn_time);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawn_missile() {
		float randomX = Random.Range(-9.3f,9.3f);
		float randomZ = Random.Range(-49.3f,49.3f);
		Vector3 pos = new Vector3(randomX, 15, randomZ);

		missile_list.Add(Instantiate(missile_prefab, pos, Quaternion.Euler(180,0,0)));
	}

	public void stop_spawn_missile(){
		CancelInvoke("spawn_missile");
	}
}