using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ÑolorsTest : MonoBehaviour
{
	void Update()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;

		// create new colors array where the colors will be created.
		Color[] colors = new Color[vertices.Length];

		for (int i = 0; i < vertices.Length; i++)
		{
			colors[i] = Color.Lerp(Color.white, Color.red, vertices[i].y / 0.3f);
			Debug.Log(vertices[i]);
		}


		Debug.Log(vertices.Length + " !!!");
		// assign the array of colors to the Mesh.
		mesh.colors = colors;
	}
}
