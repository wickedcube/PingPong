﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	// Use this for initialization
	float velocity_mul;
	public float time_slow_mul = 1f;
	Vector3 to_vec;
	Vector3 dir_vec;
	public Vector3 bounce_point;
	public Vector3 final_point;
	public Vector3 drone_pos;
	public bool is_going_to_bounce;
	public bool is_going_to_player;
	public bool is_going_to_drone;
	public InputManager.SwipeType swipeType;
	void Start () {
		velocity_mul = Random.Range(1f,2f);
		time_slow_mul = 1f;
		to_vec = bounce_point;
		is_going_to_bounce = true;
		dir_vec = to_vec - transform.position;
  }

	void OnTriggerEnter	(Collider other)
	{
		if (other.gameObject.CompareTag("BouncePoint"))
		{
			AudioManager.instance.playBallBounceSound();
			Debug.Log("hit bounce point");
			velocity_mul = Random.Range(0.1f, 0.3f);
			to_vec = final_point;
			dir_vec = to_vec - transform.position;
			is_going_to_bounce = false;
			is_going_to_player = true;
		}
		else if (other.gameObject.CompareTag("DebrisWall"))
		{
			GameManager.instance.DecreasePlayerHealth();
			Destroy(gameObject);
		}
		else if (other.gameObject.CompareTag("Drone"))
		{
			GameManager.instance.DecreaseEnemyHealth();
			Destroy(gameObject);
		}
		else if (other.gameObject.CompareTag("DroneDefense"))
		{
			var explosion_instance = Instantiate(GameManager.instance.explosion_prefab[Random.Range(0, GameManager.instance.explosion_prefab.Length)]);
			explosion_instance.position = transform.position;
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Deflect();
		}

		if(is_going_to_player || is_going_to_drone)
			dir_vec = (to_vec - transform.position)*3f;


		transform.Translate(dir_vec * velocity_mul * time_slow_mul * Time.deltaTime);
	}

	public void Deflect() {
			to_vec = drone_pos;
			dir_vec = to_vec - transform.position;
			EnemySpawner.instance.enemy_list.Remove(transform);
			Destroy(gameObject, 10f);
	}
}
