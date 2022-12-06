using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookkeeper : MonoBehaviour {
	public GameObject Self, Car0Spawner, Wheel0Spawner, Bolt0Spawner;
	//public GameObject Bolt1Spawner, Bolt2Spawner, Bolt3Spawner, Bolt4Spawner;
	private Component[] CarMove;
	private Component[] WheelMove;
	private Component[] BoltMove;
	private GameObject[] Wheels;
	private GameObject[] Bolts;
	private GameObject Car0;
	
	// Start is called before the first frame update
	void Start()
	{
		Wheels = new GameObject[1];
		Bolts = new GameObject[1];
		CarMove = new Component[1];
		WheelMove = new Component[1];
		BoltMove = new Component[1];
		
	}
		// Update is called once per frame
	void Update()
	{
		CarMove[0] = Car0Spawner.GetComponent(typeof(SpawnAndMove));
		WheelMove[0] = Wheel0Spawner.GetComponent(typeof(SpawnAndMove));
		BoltMove[0] = Bolt0Spawner.GetComponent(typeof(SpawnAndMove));
		/*
		BoltMove[1] = Bolt1Spawner.GetComponent(typeof(SpawnAndMove));
		BoltMove[2] = Bolt2Spawner.GetComponent(typeof(SpawnAndMove));
		BoltMove[3] = Bolt3Spawner.GetComponent(typeof(SpawnAndMove));
		BoltMove[4] = Bolt4Spawner.GetComponent(typeof(SpawnAndMove));
		*/
		
		int i = 0;
		foreach (SpawnAndMove carControl in CarMove) {
			Car0 = carControl.getObjectSpawnedOne();
			i++;
		}
		foreach (SpawnAndMove wheelControl in WheelMove) {
			Wheels[0] = wheelControl.getObjectSpawnedOne();
			i++;
		}
		i = 0;
		foreach (SpawnAndMove boltControl in BoltMove) {
			Bolts [0]  = boltControl.getObjectSpawnedOne();
			i++;
		}
	}
	
	public GameObject getInstantiatedObj(int i, string type) {
		if (type == "car") {
			return Car0;
		}
		else if (type == "wheel") {
			return Wheels[i];
		}
		else if (type == "bolt") {
			return Bolts[i];
		}
		return null;
	}
}