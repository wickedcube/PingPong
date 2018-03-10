using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	// Use this for initialization
	float velocity_mul;
	public float time_slow_mul;
	Vector3 dir_vec;
	public Vector3 bounce_point;
	public Vector3 drone_pos;
	void Start () {
		velocity_mul = Random.Range(1f,2f);
		time_slow_mul = 1f;
		dir_vec = bounce_point - transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("BouncePoint"))
		{
			velocity_mul = Random.Range(0.1f, 0.3f);
			dir_vec = GameManager.instance.player.position - transform.position;
		}
		else if (other.gameObject.CompareTag("DebrisWall"))
		{
			GameManager.instance.DecreasePlayerHealth();
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		Deflect();
		transform.Translate(dir_vec * velocity_mul * time_slow_mul * Time.deltaTime);
	}

	void Deflect() {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			dir_vec = drone_pos - transform.position;
			Destroy(gameObject, 10f);
		}
	}

	void OnDestory() {
		EnemySpawner.instance.DequeueLastEnemy();
	}
}
