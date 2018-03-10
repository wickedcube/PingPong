using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampCamera : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x,-30f,30f), Mathf.Clamp(transform.localEulerAngles.y, -40f, 40f),transform.localEulerAngles.z);
	}
}
