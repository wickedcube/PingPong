using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance;

	public Transform[] enemy_spawn_pos_arr;
	public Transform[] enemy_bounce_point_arr;
	public int[] enemy_spawn_per_second_per_level;
	public Coroutine enemy_spawn_coroutine;
	public List<Transform> enemy_list = new List<Transform>();
	public Queue<Transform> enemy_queue = new Queue<Transform>();
	public Transform[] enemy_prefabs_arr;
	public Transform[] final_pos_arr;
	public int total_enemies_spawned = 0;

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

		var enemy_ref = enemy_instance.GetComponent<Enemy>();

		enemy_ref.bounce_point = enemy_bounce_point_arr[Random.Range(0,enemy_bounce_point_arr.Length)].position;
		enemy_ref.drone_pos = transform.root.position;

		var rand = Random.Range(0,3);
		enemy_ref.final_point = final_pos_arr[rand].position;

		if (rand == 0)
		{
			enemy_ref.swipeType = InputManager.SwipeType.Right;
    }
		else if (rand == 1) {
			enemy_ref.swipeType = InputManager.SwipeType.Staright;
		}
		else if (rand == 2) {
			enemy_ref.swipeType = InputManager.SwipeType.Left;
		}

		enemy_queue.Enqueue(enemy_instance);
		enemy_list.Add(enemy_instance);
		total_enemies_spawned++;


		if (GameManager.instance.adrenaline_milestone_index < GameManager.instance.adrenaline_milestones.Length && total_enemies_spawned == GameManager.instance.adrenaline_milestones[GameManager.instance.adrenaline_milestone_index])
		{
			StartCoroutine(GameManager.instance.HandleAdrenalineMode());
			GameManager.instance.adrenaline_milestone_index++;
		}
		else
		{
			enemy_spawn_coroutine = StartCoroutine(EnemySpawnCoroutine());
		}
	}


	public Transform OldestEnemyAlive() {
		//if (enemy_queue.Count != 0)
		//	return enemy_queue.Dequeue();
		//else
		//	return null;
		if (enemy_list.Count != 0)
			return enemy_list[0];

		return null;
	}

	//public void DequeueLastEnemy() {
	//	//if (enemy_queue.Count != 0)
	//	//	enemy_queue.Dequeue();
	//	enemy_list.RemoveAt(0);
	//}

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

	public void CheckAndDeflectLastEnemy(InputManager.SwipeType swipe_type) {
		Debug.Log(swipe_type);
		Transform last_enemy = OldestEnemyAlive();

		if (last_enemy == null)
			return;

		if(last_enemy.GetComponent<Enemy>().swipeType == swipe_type)
		{
			last_enemy.GetComponent<Enemy>().Deflect();
		}
	}
}
