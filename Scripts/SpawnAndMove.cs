using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour {
	public GameObject Self, toSpawn, spawnLocation, moveLocation, despawnLocation, BookkeeperObj;
	public int waitStart, waitMove;
	public float speed;
	public bool isWheel;
	public bool isCar;
	public bool isBolt;
	public int wheelBoltGoesToNum = 0;
	public string helper = "DO NOT CHANGE BELOW THIS IS JANK";
	public bool despawn = false;
	public bool spawned = false;
	public bool moved = false;
	private GameObject ObjectSpawnedOne;
	private float step;
	public bool waiting = false;
	private Component[] despawners;
	private Component[] bookkeepers;
	private Component[] attachers;
	public bool wheelReady = false;
	public bool boltReady = false;
	private bool isScrewed = false;
	
	// Start is called before the first frame update
	void Start()
	{
		step = speed * Time.deltaTime;
		
		despawners = toSpawn.GetComponents(typeof(Despawner));
		
		bookkeepers = BookkeeperObj.GetComponents(typeof(Bookkeeper));
	}

	// Update is called once per frame
	void Update()
	{	
		if (isBolt) {
			foreach(Bookkeeper book in bookkeepers) {
				GameObject wheel0 = book.getInstantiatedObj(wheelBoltGoesToNum, "wheel");
				Transform despawnLocationTrans = wheel0.transform.Find("SpawnPointDespawnBolt");
				despawnLocation = despawnLocationTrans.gameObject;
			}
		}
		
		if (!spawned && !waiting) {
			ObjectSpawnedOne = Instantiate(toSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
			
			//lol bolt
			if (isBolt) {
				speed = 1.0F;
				
				attachers = ObjectSpawnedOne.GetComponents(typeof(Attacher));
				foreach(Attacher att in attachers) {
					isScrewed = att.screwed;
				}
			}
			
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
		else if (!despawn && !waiting && !isWheel && (!isCar || wheelReady) && (!isBolt || boltReady)) {
			//lol bolt
			if (isBolt) {
				speed = 0.1F;
			}
			
			if ((ObjectSpawnedOne.transform.position != despawnLocation.transform.position) && (!isBolt || !isScrewed)) {
				ObjectSpawnedOne.transform.position = Vector3.MoveTowards(ObjectSpawnedOne.transform.position, despawnLocation.transform.position, step);
			}
			else {
				despawn = true;
				wheelReady = false;
				boltReady = false;
				Debug.Log("uhh", Self);
			}
		}
		else if (despawn && !waiting && (!isWheel && !isBolt )) {
			Destroy(ObjectSpawnedOne);
			despawn = false;
			moved = false;
			spawned = false;
		}
	}
	
	public GameObject getObjectSpawnedOne() {
		return ObjectSpawnedOne;
	}
	
	public void setWheelReady() {
		Debug.Log("setWheelReady()", Self);
		
		this.wheelReady = true;
	}
	
	public void setBoltReady() {
		Debug.Log("setBoltReady()", Self);
		
		this.boltReady = true;
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
