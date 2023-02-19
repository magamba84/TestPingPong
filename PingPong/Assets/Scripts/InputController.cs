using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	[SerializeField] private Camera camera;

	private int savedTouch = -1;

	public event Action<Vector3> TouchDown;
	public event Action<Vector3> TouchMove;
	public event Action<Vector3> TouchUp;

	private Plane plane;
	private Rect margin;

	private void Start()
	{
		margin = new Rect(0, 0, 1, 1);
		plane = new Plane(Vector3.zero, Vector3.right, Vector3.up);
	}

	void Update()
	{
		var inputTouches = Input.touches;

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
		{
			inputTouches = new Touch[1];

			inputTouches[0] = new Touch();
			inputTouches[0].fingerId = 0;
			inputTouches[0].position = Input.mousePosition;

			if (Input.GetMouseButtonDown(0))
			{
				inputTouches[0].phase = TouchPhase.Began;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				inputTouches[0].phase = TouchPhase.Ended;
			}
			else if (Input.GetMouseButton(0))
			{
				inputTouches[0].phase = TouchPhase.Moved;
			}
		}
		else
		{
			inputTouches = new Touch[0];
		}
#endif

		foreach (var touch in inputTouches)
		{
			switch (touch.phase)
			{
				case TouchPhase.Began:
					CreateTouch(touch);
					break;
				case TouchPhase.Moved:
					MoveTouch(touch);
					break;
				case TouchPhase.Stationary:
					MoveTouch(touch);
					break;
				case TouchPhase.Ended:
					EndTouch(touch);
					break;
				case TouchPhase.Canceled:
					EndTouch(touch);
					break;
			}
		}
	}

	private void CreateTouch(Touch touch)
	{
		if (savedTouch != -1)
			return;

		savedTouch = touch.fingerId;
		TouchDown?.Invoke(GetCoordinates(touch.position));
	}

	private void MoveTouch(Touch touch)
	{
		if (savedTouch != touch.fingerId)
			return;

		TouchMove?.Invoke(GetCoordinates(touch.position));

	}

	private void EndTouch(Touch touch)
	{
		if (savedTouch != touch.fingerId)
			return;

		savedTouch = -1;
		TouchUp?.Invoke(GetCoordinates(touch.position));
	}

	public Vector3 GetCoordinates(Vector3 position)
	{
		var w = Screen.width;
		var h = Screen.height;
		position = new Vector3(Mathf.Clamp(position.x, margin.x * w, margin.xMax * w), Mathf.Clamp(position.y, margin.y * h, margin.yMax * h), 0);

		Ray ray = camera.ScreenPointToRay(position);
		float range;
		plane.Raycast(ray, out range);

		var point = ray.GetPoint(range);
		return point;

	}
}
