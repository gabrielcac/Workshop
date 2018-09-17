﻿using UnityEngine;

namespace Workshop.TowerDefense
{
	public class Billboard : MonoBehaviour
	{
		private Camera _camera;

		private void Start()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
							 _camera.transform.rotation * Vector3.up);
		}
	}
}