using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance;

	public Transform[] enemy_spawn_pos_arr;
	public Transform[] enemy_bounce_point_arr;
	public int[] enemy_spawn_per_second_per_level;
	public Coroutine enemy_spawn_coroutine;
	public Queue<Transform> enemy_queue = new Queue<Transform>();
	public Transform[] enemy_prefabs_arr;

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
	void Start () {
		enemy_spawn_coroutine = StartCoroutine(EnemySpawnCoroutine());
	}

	public IEnumerator EnemySpawnCoroutine() {
		yield return new WaitForSeconds(enemy_spawn_per_second_per_level[GameManager.instance.current_level]);
		Transform enemy_instance = Instantiate(enemy_prefabs_arr[Random.Range(0,enemy_prefabs_arr.Length)]);
		enemy_instance.position = enemy_spawn_pos_arr[Random.Range(0, enemy_spawn_pos_arr.Length)].transform.position;
		enemy_instance.GetComponent<Enemy>().bounce_point = enemy_bounce_point_arr[Random.Range(0,enemy_bounce_point_arr.Length)].position;
		enemy_instance.GetComponent<Enemy>().drone_pos = transform.root.position;
		enemy_queue.Enqueue(enemy_instance);
		enemy_spawn_coroutine = StartCoroutine(EnemySpawnCoroutine());
	}


	public Transform OldestEnemyAlive() {
		return enemy_queue.Dequeue();
	}

	public void DequeueLastEnemy() {
		enemy_queue.Dequeue();
	}

	public IEnumerator SpawnAdrenalineModeEnemies() {

		StopCoroutine(enemy_spawn_coroutine);
		for (int i = 0; i < 4; i++)
		{
			Transform enemy_instance = Instantiate(enemy_prefabs_arr[Random.Range(0, enemy_prefabs_arr.Length)]);
			enemy_instance.position = enemy_spawn_pos_arr[Random.Range(0, enemy_spawn_pos_arr.Length)].transform.position;
			enemy_instance.GetComponent<Enemy>().bounce_point = enemy_bounce_point_arr[Random.Range(0, enemy_bounce_point_arr.Length)].position;
			enemy_instance.GetComponent<Enemy>().drone_pos = transform.root.position;
			enemy_queue.Enqueue(enemy_instance);
			yield return new WaitForSeconds(0.5f);
		}
		//enemy_spawn_coroutine = StartCoroutine(EnemySpawnCoroutine());
	}
}
