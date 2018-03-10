using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager instance;
	public enum SwipeType { Left, Right, Staright };
	public List<SwipeType> list_swipe_type = new List<SwipeType>();
	Vector2 init_touch_position;
	float input_y_delta = 2f;
	float input_x_delta = 2f;

	void Awake() {
		instance = this;
  }
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.is_in_adreanaline_mode)
		{
			if (Input.GetMouseButton(0))
			{
				init_touch_position = Input.mousePosition;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				if (Input.mousePosition.y - init_touch_position.y > input_y_delta)
				{
					list_swipe_type.Add(SwipeType.Right);
				}
				else if (init_touch_position.y - Input.mousePosition.y > input_y_delta) {
					list_swipe_type.Add(SwipeType.Left);
				}
				else if (Input.mousePosition.x - init_touch_position.x > input_x_delta)
				{
					list_swipe_type.Add(SwipeType.Staright);
				}
				init_touch_position = new Vector3(-1f, -1f);
				if (list_swipe_type.Count == 4)
				{
					GameManager.instance.ResetHandleAdrenalineCoroutine();
				}
      }
		}
	}
}
