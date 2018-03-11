using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public static TimeController instance;
	public bool can_slow_time = false;


	void Awake() {
		instance = this;
	}

	public void SlowDownTime() {
		Rotate.rot_mul = 0.1f;
		foreach (var item in EnemySpawner.instance.enemy_queue)
		{
			if (item != null)
			{
				item.GetComponent<Enemy>().time_slow_mul = 0.1f;
			}
		}
	}


	public void ResetTimerOnEnemies() {

	}
}
