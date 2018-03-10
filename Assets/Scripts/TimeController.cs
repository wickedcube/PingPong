using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public static TimeController instance;
	public bool can_slow_time = false;


	void Awake() {
		instance = this;
	}

	public void SlowDownTime(float time_to_slow_down) {
		StartCoroutine(SlowDownTimeCoroutine(time_to_slow_down));
	}

	IEnumerator SlowDownTimeCoroutine(float time_to_slow_down) {
		
	}


	public void ResetTimerOnEnemies() {

	}
}
