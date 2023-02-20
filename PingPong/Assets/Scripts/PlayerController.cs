using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private InputController inputController;


	[SerializeField] private float speed = 1f;

	private bool isDragging;
	private float desiredX;
	private float xMin, xMax;

	private GameObject ball;

	public event Action HitBall;


	void Start()
	{
		desiredX = transform.position.x;
		inputController.TouchDown += OnTouchDown;
		inputController.TouchMove += OnTouchMove;
		inputController.TouchUp += OnTouchUp;
	}

	public void Init(GameObject ball)
	{
		this.ball = ball;
	}

	private void OnTouchDown(Vector3 pos)
	{
		isDragging = true;
		desiredX = pos.x;
	}

	private void OnTouchMove(Vector3 pos)
	{
		desiredX = pos.x;
	}

	private void OnTouchUp(Vector3 pos)
	{
		isDragging = false;
	}

	private void Update()
	{
		if (isDragging)
		{
			var pos = transform.position;
			pos.x = Mathf.MoveTowards(pos.x, desiredX, speed * Time.deltaTime);
			transform.position = pos;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == ball)
			HitBall?.Invoke();
	}
}
