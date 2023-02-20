using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorer : MonoBehaviour
{
	[SerializeField] private Color baseColor;
	Mesh mesh;
	private Vector3[] vertices;
	private Vector3 lastCollisionPont;

	private bool collided = false;
	private float intencity;
	private float intencityDecreaseSpeed = 2f;

	void Start()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
	}

	void Update()
	{
		if (collided && intencity > 0)
		{
			intencity -= Time.deltaTime * intencityDecreaseSpeed;
			ColorFromPos(lastCollisionPont, intencity);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
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
		//float min = float.MaxValue;
		//float max = float.MinValue;
		for (int i = 0; i < vertices.Length; i++)
		{
			float r = Mathf.Max(((vertices[i] - pos).magnitude) * intencity, 0);
			//min = Mathf.Min(r, min);
			//max = Mathf.Max(r, max);
			colors[i] = Color.Lerp(baseColor, Color.red, r);
			//Debug.Log((vertices[i] - pos).magnitude);
		}
		//Debug.Log(min + " / " + max);
		mesh.colors = colors;
	}
}
