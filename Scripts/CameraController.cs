using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Camera[] Cameras;
	private int index = 0;
	
	// Start is called before the first frame update
	void Start()
	{	
		for (int i = 1; i < Cameras.Length; i++) {
			Cameras[i].gameObject.SetActive(false);
		}
		
	}
		// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			index++;
			
			if (index < Cameras.Length) {
				Cameras[index - 1].gameObject.SetActive(false);
				Cameras[index].gameObject.SetActive(true);
			}
			else {
				Cameras[index - 1].gameObject.SetActive(false);
				index = 0;
				Cameras[index].gameObject.SetActive(true);
			}
		}
	}
	
}