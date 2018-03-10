using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	float velocity_mul;
	Vector3 dir_vec;
	public Vector3 bounce_point;
	public Vector3 drone_pos;
	void Start () {
		velocity_mul = Random.Range(1f,2f);
		dir_vec = bounce_point - transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("BouncePoint"))
		{
			velocity_mul = Random.Range(0.1f,0.3f);
			dir_vec = GameManager.instance.player.position - transform.position;
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
		{
			dir_vec = drone_pos - transform.position;
		}
		transform.Translate(dir_vec * velocity_mul * Time.deltaTime);
	}
}
