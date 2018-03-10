using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	int velocity_mul;
	Vector2 dir_vec;
	void Start () {
		velocity_mul = Random.Range(5,15);
		dir_vec = transform.position - GameManager.instance.player.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(dir_vec * velocity_mul * Time.deltaTime);
	}
}
