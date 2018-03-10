using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance;

	public Transform[] enemy_spawn_pos_arr;
	public int[] enemy_spawn_per_second_per_level;
	public Coroutine enemy_spawn_coroutine;
	public Queue<Transform> enemy_queue = new Queue<Transform>();
	public Transform[] enemy_prefabs_arr;

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
		enemy_spawn_coroutine = StartCoroutine(EnemySpawnCoroutine());
	}

	IEnumerator EnemySpawnCoroutine() {
		yield return new WaitForSeconds(enemy_spawn_per_second_per_level[GameManager.instance.current_level]);
		Transform enemy_instance = Instantiate(enemy_prefabs_arr[Random.Range(0,enemy_prefabs_arr.Length)],enemy_spawn_pos_arr[Random.Range(0,enemy_spawn_pos_arr.Length)]);
		enemy_queue.Enqueue(enemy_instance);
		enemy_spawn_coroutine = StartCoroutine(EnemySpawnCoroutine());
	}


	public Transform OldestEnemyAlive() {
		return enemy_queue.Dequeue();
	}
}
