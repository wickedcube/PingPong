using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public static TimeController instance;


	void Awake() {
		instance = this;
	}

	public void SlowDownTime(float time_to_slow_down) {
		StartCoroutine(SlowDownTimeCoroutine(time_to_slow_down));
	}

	IEnumerator SlowDownTimeCoroutine(float time_to_slow_down) {
		float timer = time_to_slow_down;
		while (timer>0) {
			Time.timeScale = 0.5f*(1 + (timer / time_to_slow_down));
			timer -= Time.unscaledDeltaTime;
			yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
		}

		Time.timeScale = 1f;
	}
}
