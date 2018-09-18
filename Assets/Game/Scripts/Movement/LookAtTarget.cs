using UnityEngine;

namespace Workshop.Movement
{
	/// <summary>
	/// Rotaciona um objeto para que fique de frente para o objeto target.
	/// </summary>
	public class LookAtTarget : MonoBehaviour
	{
		public Transform target;

		private void Update()
		{
			Vector3 directionToTarget = target.transform.position - transform.position;

			transform.rotation = Quaternion.LookRotation(directionToTarget);
		}
	}
}