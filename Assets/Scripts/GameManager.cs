using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public Transform player;
	///public CountdownTimer countdownTimer;
	[HideInInspector]
	public int current_level = 0;
	public int[] time_per_level;
	Coroutine level_update_coroutine;
	Coroutine handle_adrenaline_coroutine;
	int player_health = 3;
	int enemy_health = 6;
	public Transform player_health_transform;
	public Transform enemy_health_transform;
	public int[] adrenaline_milestones;
	public int adrenaline_milestone_index = 0;
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
		//handle_adrenaline_coroutine = StartCoroutine(HandleAdrenalineMode());
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
		enemy_health--;
		if (enemy_health <= 0)
		{
			SceneManager.LoadScene(0);
		}

		Destroy(enemy_health_transform.GetChild(enemy_health_transform.childCount - 1).gameObject);  
	}

	public void DecreasePlayerHealth() {
		player_health--;
		if (player_health <= 0)
		{
			SceneManager.LoadScene(0);
		}
		Destroy(player_health_transform.GetChild(player_health_transform.childCount - 1).gameObject);
	}

	public IEnumerator HandleAdrenalineMode() {
		//yield return new WaitForSeconds(10f);
		is_in_adreanaline_mode = true;
		Debug.Log("Adrenaline mode enabled");
		yield return StartCoroutine(EnemySpawner.instance.SpawnAdrenalineModeEnemies());
		TimeController.instance.SlowDownTime();
		GameObject.Find("Drone").transform.GetComponent<Animator>().enabled = false;
		GameObject.Find("AdrenalineRedFlashImage").GetComponent<Animator>().enabled = true;
		//yield return new WaitForSeconds(3f);
		//is_in_adreanaline_mode = false;
	}

	public void ResetHandleAdrenalineCoroutine()
	{
		Rotate.rot_mul = 1f;
		is_in_adreanaline_mode = false;
		GameObject.Find("Drone").transform.GetComponent<Animator>().enabled = true;
		GameObject.Find("AdrenalineRedFlashImage").GetComponent<Animator>().enabled = false;
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

			if (last_enemy == null)
				break;

			if(last_enemy.GetComponent<Enemy>().swipeType == InputManager.instance.list_swipe_type[i])
				last_enemy.GetComponent<Enemy>().Deflect();

		}
		StartCoroutine(EnemySpawner.instance.EnemySpawnCoroutine());
	}
}
