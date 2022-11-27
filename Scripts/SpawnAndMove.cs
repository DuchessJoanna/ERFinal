using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour {
	public GameObject toSpawn, spawnLocation, moveLocation, despawnLocation;
	public int waitStart, waitMove;
	public float speed;
	private bool despawn = false;
	private bool spawned = false;
	private bool moved = false;
	private GameObject ObjectSpawnedOne;
	private float step;
	private bool waiting = false;
	
	// Start is called before the first frame update
	void Start()
	{
		step = speed * Time.deltaTime;
	}

	// Update is called once per frame
	void Update()
	{
		if (!spawned && !waiting) {
			ObjectSpawnedOne = Instantiate(toSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
			waiting = true;
			StartCoroutine(waiter(true));
		}
		else if (!moved && !waiting) {
			if (ObjectSpawnedOne.transform.position != moveLocation.transform.position) {
				ObjectSpawnedOne.transform.position = Vector3.MoveTowards(ObjectSpawnedOne.transform.position, moveLocation.transform.position, step);
			}
			else {
				waiting = true;
				StartCoroutine(waiter(false));
			}
		}
		else if (!despawn && !waiting) {
			if (ObjectSpawnedOne.transform.position != despawnLocation.transform.position) {
				ObjectSpawnedOne.transform.position = Vector3.MoveTowards(ObjectSpawnedOne.transform.position, despawnLocation.transform.position, step);
			}
			else despawn = true;
		}
		else if (!waiting) {
			Destroy(ObjectSpawnedOne);
			despawn = false;
			moved = false;
			spawned = false;
		}
	}
	
	IEnumerator waiter(bool start) {
		int waitTime;
		if (start) waitTime = waitStart;
		else waitTime = waitMove;
		
		yield return new WaitForSeconds(waitTime);
		
		if (start) spawned = true;
		else moved = true;
		waiting = false;
	}
}
