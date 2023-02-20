using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorer : MonoBehaviour
{
	[SerializeField] private Color baseColor;

	private Mesh mesh;
	private Vector3[] vertices;
	private Vector3 lastCollisionPont;

	private bool collided = false;
	private float intencity;
	private float intencityDecreaseSpeed = 2f;

	void Awake()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		SetColorInstant(baseColor);
	}

	public void SetColorInstant(Color color) 
	{
		Color[] colors = new Color[vertices.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			colors[i] = baseColor;
		}
		mesh.colors = colors;
	}

	void Update()
	{
		if (collided && intencity > 0)
		{
			intencity -= Time.deltaTime * intencityDecreaseSpeed;
			ColorFromPos(lastCollisionPont, intencity);
		}
	}

	public void CollisionEnter(Collision2D collision)
	{
		collided = true;
		intencity = 1f;
		var pos1 = collision.contacts[0].point;
		var pos2 = new Vector3(pos1.x, pos1.y, transform.position.z); ;
		lastCollisionPont = transform.position - pos2;

	}

	private void ColorFromPos(Vector3 pos, float intencity)
	{
		Color[] colors = new Color[vertices.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			float r = Mathf.Max(((vertices[i] - pos).magnitude) * intencity, 0);
			colors[i] = Color.Lerp(baseColor, Color.red, r);
		}
		mesh.colors = colors;
	}

}
