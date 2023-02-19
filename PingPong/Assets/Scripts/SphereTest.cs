using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{

	void Start()
	{
		GetComponent<Rigidbody2D>().AddForce(new Vector2(2,1) * 200);
	}

	void Update()
	{

	}
}
