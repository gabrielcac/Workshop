using UnityEngine;

namespace Workshop.TowerDefense
{
	/// <summary>
	/// Um billboard é uma imagem 2D em um mundo 3D, e fica sempre com a face virada para a câmera.
	/// </summary>
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