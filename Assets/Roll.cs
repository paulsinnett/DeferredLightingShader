using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
	Vector3 position;

	void Start()
	{
		position = transform.position;
	}

	void Update()
	{
		transform.position = position + Vector3.left * Mathf.Sin(Time.time * Mathf.PI) * 2.0f;
		transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Sin(Time.time * Mathf.PI) * 180.0f);
	}
}