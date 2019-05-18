using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
	Quaternion rotation;

	void Start()
	{
		rotation = transform.rotation;
	}

	void Update()
	{
		transform.rotation = rotation * Quaternion.AngleAxis(Mathf.Sin(Time.time * Mathf.PI * 0.5f) * 45.0f, Vector3.forward);
	}
}