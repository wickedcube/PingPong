using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager instance;
	public enum SwipeType { Left, Right, Staright };
	public List<SwipeType> list_swipe_type = new List<SwipeType>();
	Vector2 init_touch_position;
	float input_y_delta = 120f;
	float input_x_delta = 120f;

	void Awake() {
		instance = this;
  }
	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			init_touch_position = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			var curr_touch_pos = Input.mousePosition;
			var slope = (curr_touch_pos.y - init_touch_position.y) / (curr_touch_pos.x -  init_touch_position.x);
			var theta = Mathf.Atan(slope) * Mathf.Rad2Deg;


			if (theta < 0)
				theta += 180;

			//theta += 360;
			//theta %= 360;

			if (curr_touch_pos.x > init_touch_position.x && curr_touch_pos.y > init_touch_position.y)
			{
				if (theta < 45)
				{
					if (GameManager.instance.is_in_adreanaline_mode)
					{
						list_swipe_type.Add(SwipeType.Right);
					}
					else
					{
						EnemySpawner.instance.CheckAndDeflectLastEnemy(SwipeType.Right);
					}
				}
				else
				{
					if (GameManager.instance.is_in_adreanaline_mode)
					{
						list_swipe_type.Add(SwipeType.Staright);
					}
					else
					{
						EnemySpawner.instance.CheckAndDeflectLastEnemy(SwipeType.Staright);
					}
				}
			}
			else if (curr_touch_pos.x > init_touch_position.x && curr_touch_pos.y < init_touch_position.y) {
					if (GameManager.instance.is_in_adreanaline_mode)
					{
						list_swipe_type.Add(SwipeType.Right);
					}
					else
					{
						EnemySpawner.instance.CheckAndDeflectLastEnemy(SwipeType.Right);
					}
			}
			else if (curr_touch_pos.x < init_touch_position.x && curr_touch_pos.y < init_touch_position.y)
			{
					if (GameManager.instance.is_in_adreanaline_mode)
					{
						list_swipe_type.Add(SwipeType.Left);
					}
					else
					{
						EnemySpawner.instance.CheckAndDeflectLastEnemy(SwipeType.Left);
					}
			}
			else if (curr_touch_pos.x < init_touch_position.x && curr_touch_pos.y > init_touch_position.y)
			{
				if (theta > 135)
				{
					if (GameManager.instance.is_in_adreanaline_mode)
					{
						list_swipe_type.Add(SwipeType.Left);
					}
					else
					{
						EnemySpawner.instance.CheckAndDeflectLastEnemy(SwipeType.Left);
					}
				}
				else
				{
					if (GameManager.instance.is_in_adreanaline_mode)
					{
						list_swipe_type.Add(SwipeType.Staright);
					}
					else
					{
						EnemySpawner.instance.CheckAndDeflectLastEnemy(SwipeType.Staright);
					}
				}
			}


			init_touch_position = new Vector3(-1f, -1f);
			if (list_swipe_type.Count == 4)
			{
				GameManager.instance.ResetHandleAdrenalineCoroutine();
			}
		}
	}
}
