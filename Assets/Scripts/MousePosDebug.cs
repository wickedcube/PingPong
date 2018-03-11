using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Down " + Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Up" + Input.mousePosition);
		}
	}
}
