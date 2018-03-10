using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public Transform player;
	///public CountdownTimer countdownTimer;
	[HideInInspector]
	public int current_level = 0;
	public int[] time_per_level;
	Coroutine level_update_coroutine;
	void Awake() {
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		level_update_coroutine = StartCoroutine(UpdateLevelTime());
	}

	IEnumerator UpdateLevelTime() {

		if (current_level == time_per_level.Length)
			yield break;

		//countdownTimer.StartCountdown(time_per_level[current_level]);
		yield return new WaitForSeconds(time_per_level[current_level]);
		
		current_level++;
		level_update_coroutine = StartCoroutine(UpdateLevelTime());
	}
}
