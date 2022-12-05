using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour {
	public GameObject Self, DespawnWall, SpawnPoint;
	private Component[] spawnAndMoves;
	
	// Start is called before the first frame update
		void Start()
		{
			spawnAndMoves = SpawnPoint.GetComponents(typeof(SpawnAndMove));
		}

		// Update is called once per frame
		void Update()
		{
			
		}
		
		void OnTriggerEnter(Collider other) {
			if (other.gameObject == DespawnWall) {
				foreach (SpawnAndMove spawnControl in spawnAndMoves) {
					spawnControl.despawn = false; spawnControl.spawned = false; spawnControl.moved = false;
				}
				Destroy(Self);
			}
		}
}