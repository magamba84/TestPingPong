using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	[SerializeField] private float minYSpeed = 2f;
	[SerializeField] private float maxYSpeed = 10f;
	[SerializeField] private float maxXSpeed = 3f;

	[SerializeField] private BallColorer colorer;

	private GameObject playerPad;
	private GameObject aIPad;

	private Rigidbody2D rigidBody;

	private bool movingDown;
	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	public void StartPlay(GameObject playerPad, GameObject aIPad)
	{
		this.playerPad = playerPad;
		this.aIPad = aIPad;
		rigidBody.velocity = new Vector2(Random.value * 0.5f - 1, 2);//.AddForce(new Vector2(1, 2) * 300);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == playerPad || collision.gameObject == aIPad)
		{
			var to = transform.position - collision.gameObject.transform.position;
			to.x *= 1.5f;

			rigidBody.AddForce(to * 200);

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

}
