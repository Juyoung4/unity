using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
	private Transform tr;
	public GameObject particleprefab;

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FadeOut() {
		for(int i = 0; i < 20; i++) {
			tr.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
			yield return new WaitForSeconds(0.05f);
		}
		Destroy(gameObject);
	}

	public void OnCollisionEnter (Collision collision)
    {
		StartCoroutine("FadeOut");
        if (particleprefab)
        {
            GameObject explosion = (GameObject)Instantiate (particleprefab, transform.position, particleprefab.transform.rotation);
            Destroy (explosion, explosion.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
        }
    }
}
