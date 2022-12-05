using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWallControl : MonoBehaviour {
	public GameObject Self, Despawn;
	
	// Start is called before the first frame update
	void Start()
	{
		Despawn.SetActive(false);
	}
		// Update is called once per frame
	void Update()
	{
		
	}
	
	void OnTriggerEnter(Collider other) {
		Despawn.SetActive(true);
		StartCoroutine(waiter());
	}
	
	
	IEnumerator waiter() {
		int waitTime = 1;
		
		yield return new WaitForSeconds(waitTime);
		
		Despawn.SetActive(false);
	}
}