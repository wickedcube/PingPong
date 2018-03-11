using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public static float rot_mul = 1f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, rot_mul));
	}
}
