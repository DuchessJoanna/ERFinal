using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTiming : MonoBehaviour {
	public GameObject Self, BoltSpawner, CarSpawner, WheelSpawner, BookkeeperObj;
	private Component[] spawnAndMovesBolt;
	private Component[] spawnAndMovesWheel;
	private Component[] spawnAndMovesCar;
	private Component[] bookkeepers;
	private GameObject Bolt;
	private GameObject Wheel;
	//Implement 4 other private Bolt objects for the other bolts. Have them go in when the wheel collides THEN the send in Bolt and when it goes in do the if gameobject bolt stuff.
	
	// Start is called before the first frame update
	void Start()
	{
		bookkeepers = BookkeeperObj.GetComponents(typeof(Bookkeeper));
		
		spawnAndMovesBolt = BoltSpawner.GetComponents(typeof(SpawnAndMove));
		//
		
		spawnAndMovesWheel = WheelSpawner.GetComponents(typeof(SpawnAndMove));
		//
		
		spawnAndMovesCar = CarSpawner.GetComponents(typeof(SpawnAndMove));
	}
		// Update is called once per frame
	void Update()
	{
		foreach (SpawnAndMove spawnControl in spawnAndMovesBolt) {
			foreach (Bookkeeper book in bookkeepers) {
				Bolt = book.getInstantiatedObj(0, "bolt");
			}
			//Bolt = spawnControl.getObjectSpawnedOne();
		}
		foreach (SpawnAndMove spawnControl in spawnAndMovesWheel) {
			foreach (Bookkeeper book in bookkeepers) {
				Wheel = book.getInstantiatedObj(0, "wheel");
			}
			//Wheel = spawnControl.getObjectSpawnedOne();
		}
	}
	
	public void wheelEnter() {
		Debug.Log("wheelEnter()", Self);
		
		var posSaveBolt1 = Bolt.transform.position;
		//Bolt.transform.parent = Self.transform;
		Bolt.transform.parent = null;
		Bolt.transform.position = posSaveBolt1;
		
		foreach (SpawnAndMove spawnControl in spawnAndMovesBolt) {
			spawnControl.setBoltReady();
		}
	}
	
	public void boltEnter() {
		Debug.Log("boltEnter()", Self);
		
		foreach (SpawnAndMove spawnControl in spawnAndMovesCar) {
			spawnControl.setWheelReady();
		}
	}
}