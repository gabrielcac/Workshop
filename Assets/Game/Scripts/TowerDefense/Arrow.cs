using UnityEngine;

namespace Workshop.TowerDefense
{
	[RequireComponent(typeof(Rigidbody))]
	public class Arrow : MonoBehaviour
	{
		public float damage;

		private Rigidbody _rigidbody;
		private Vector3 _velocity;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			_rigidbody.velocity = _velocity;
		}

		private void OnTriggerEnter(Collider other)
		{
			Enemy enemy = other.GetComponentInParent<Enemy>();
			if (enemy != null)
			{
				enemy.Damage(damage);
			}

			Destroy(gameObject);
		}

		public void Fire(Vector3 velocity)
		{
			transform.rotation = Quaternion.LookRotation(velocity);
			_velocity = velocity;
		}
	}
}