using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		Vector3 directionToTarget = target.transform.position - transform.position;

		transform.rotation = Quaternion.LookRotation(directionToTarget);
	}
}
