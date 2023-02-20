using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour, IPausable
{
	[SerializeField] private GameObject PlayerPad;
	[SerializeField] private GameObject AIPad;

	[SerializeField] private float minYSpeed = 2f;
	[SerializeField] private float maxYSpeed = 10f;
	[SerializeField] private float maxXSpeed = 10f;

	[SerializeField] private BallColorer colorer;

	private Rigidbody2D rigidBody;

	private bool movingDown;
	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	public void StartPlay() 
	{
		rigidBody.AddForce(new Vector2(1, 2) * 200);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == PlayerPad || collision.gameObject == AIPad)
		{
			var to = transform.position - collision.gameObject.transform.position;
			to.x *= 1.5f;
			rigidBody.AddForce(to * 100);

			movingDown = to.y < 0;
		}

		colorer.CollisionEnter(collision);
	}

	private void Update()
	{
		var velocity = rigidBody.velocity;
		if (Mathf.Abs(velocity.y) < minYSpeed)
			velocity.y = minYSpeed * (movingDown ? -1f : 1f);

		if (Mathf.Abs(velocity.y) > maxYSpeed)
			velocity.y = maxYSpeed * Mathf.Sign(velocity.y);

		if (Mathf.Abs(velocity.x) > maxXSpeed)
			velocity.x = maxXSpeed * Mathf.Sign(velocity.x);

		rigidBody.velocity = velocity;
	}

	public void SetPause(bool pause)
	{

	}
}
