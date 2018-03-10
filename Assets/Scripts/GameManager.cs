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
	Coroutine handle_adrenaline_coroutine;
	int player_health;
	int enemy_health;
	public bool is_in_adreanaline_mode;
	void Awake() {
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

	}

	// Use this for initialization
	void Start() {
		level_update_coroutine = StartCoroutine(UpdateLevelTime());
		handle_adrenaline_coroutine = StartCoroutine(HandleAdrenalineMode());
	}

	IEnumerator UpdateLevelTime() {

		if (current_level == time_per_level.Length)
			yield break;

		//countdownTimer.StartCountdown(time_per_level[current_level]);
		yield return new WaitForSeconds(time_per_level[current_level]);

		current_level++;
		level_update_coroutine = StartCoroutine(UpdateLevelTime());
	}

	public void DecreaseEnemyHealth() {


	}

	public void DecreasePlayerHealth() {


	}

	IEnumerator HandleAdrenalineMode() {
		yield return new WaitForSeconds(10f);
		is_in_adreanaline_mode = true;
		Debug.Log("Adrenaline mode enabled");
		yield return StartCoroutine(EnemySpawner.instance.SpawnAdrenalineModeEnemies());
		TimeController.instance.SlowDownTime();
		//yield return new WaitForSeconds(3f);
		//is_in_adreanaline_mode = false;
	}

	public void ResetHandleAdrenalineCoroutine()
	{
		is_in_adreanaline_mode = false;
		StopCoroutine(handle_adrenaline_coroutine);
		foreach (var item in EnemySpawner.instance.enemy_queue)
		{
			if (item != null) {
				item.GetComponent<Enemy>().time_slow_mul = 1f;
			}
		}

		for (int i = 0; i < InputManager.instance.list_swipe_type.Count; i++)
		{
			Transform last_enemy = EnemySpawner.instance.OldestEnemyAlive();
			last_enemy.GetComponent<Enemy>().Deflect();
		}
	}
}
