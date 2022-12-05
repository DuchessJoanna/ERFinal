//E
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacher : MonoBehaviour {
	public GameObject Self, Arm, CarSpawner, BookkeeperObj;
	public bool isBolt;
	private GameObject Car;
	public bool grabbed = false;
	public bool screwed = false;
	private Component[] spawnAndMoves;
	private Component[] wheelTimer;
	private Component[] bookkeepers;
	private Transform CarHoleTrans;
	private GameObject CarHole;
	private Transform ScrewHoleTrans;
	private GameObject ScrewHole;
	
	// Start is called before the first frame update
		void Start()
		{
			bookkeepers = BookkeeperObj.GetComponents(typeof(Bookkeeper));
			
			spawnAndMoves = CarSpawner.GetComponents(typeof(SpawnAndMove));
		}

		// Update is called once per frame
		void Update()
		{
			//Get New relevant objects
			
			
			foreach (SpawnAndMove spawnControl in spawnAndMoves) {
				foreach (Bookkeeper book in bookkeepers) {
					Car = book.getInstantiatedObj(0, "car");
				}
				//Car = spawnControl.getObjectSpawnedOne();
				
				wheelTimer = Car.GetComponents(typeof(WheelTiming));
				CarHoleTrans = Car.transform.Find("CarHole1");
				CarHole = CarHoleTrans.gameObject;
				ScrewHoleTrans = Car.transform.Find("ScrewHole1");
				ScrewHole = ScrewHoleTrans.gameObject;
				/*
				try {
					
				}
				catch (NullReferenceException ex) {
					Debug.LogException(ex, this);
				}
				*/
			}
		}
		
		void OnTriggerEnter(Collider other) {
			if ((other.gameObject == Arm && !grabbed) || (!isBolt && other.gameObject == CarHole && !screwed)) {
				var posSave = Self.transform.position;
				Self.transform.parent = other.transform;
				Self.transform.position = posSave;
				if (other.gameObject == Arm) grabbed = true; //Self.transform.localPosition = Vector3.zero;
				else if (other.gameObject == CarHole) {
					foreach (WheelTiming wheelControl in wheelTimer) {
						wheelControl.wheelEnter();
					}
					screwed = true;
				}
			}
			else if (isBolt && other.gameObject == ScrewHole && !screwed) {
				var posSave = Self.transform.position;
				Self.transform.parent = other.transform;
				Self.transform.position = posSave;
				foreach (WheelTiming wheelControl in wheelTimer) {
						wheelControl.boltEnter();
					}
				screwed = true;
			}
		}
}