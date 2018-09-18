using System.Collections.Generic;
using UnityEngine;

namespace Workshop.TowerDefense
{
	/// <summary>
	/// A torre atira flechas nos inimigos em seu alcance. Ela escolhe os inimigos mais próximos.
	/// </summary>
	public class Tower : MonoBehaviour
	{
		public float reloadTime;
		public float arrowSpeed;
		public Arrow arrowPrefab;
		public Transform arrowOrigin;

		private List<Enemy> _enemies = new List<Enemy>();
		private float _reloadTimer;

		private void Update()
		{
			_reloadTimer -= Time.deltaTime;
			if (_reloadTimer <= 0)
			{
				Enemy target = GetNearestTarget();
				if (target != null)
				{
					FireArrow(target);
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			Enemy enemy = other.GetComponentInParent<Enemy>();
			if (enemy != null)
			{
				_enemies.Add(enemy);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			Enemy enemy = other.GetComponentInParent<Enemy>();
			if (enemy != null)
			{
				_enemies.Remove(enemy);
			}
		}

		/// <summary>
		/// Encontra o inimigo dentro do alcance da torre que está mais próxima.
		/// </summary>
		/// <returns></returns>
		private Enemy GetNearestTarget()
		{
			Enemy nearestEnemy = null;
			float nearestDistance = float.MaxValue;
			for (int i = _enemies.Count - 1; i >= 0; i--)
			{
				if (_enemies[i].Life == 0)
				{
					_enemies.RemoveAt(i);
					continue;
				}

				float distance = (_enemies[i].transform.position - transform.position).magnitude;
				if (distance < nearestDistance)
				{
					nearestDistance = distance;
					nearestEnemy = _enemies[i];
				}
			}
			return nearestEnemy;
		}

		/// <summary>
		/// Atira uma flecha onde o <see cref="Enemy"/> estará.
		/// </summary>
		/// <param name="target"></param>
		private void FireArrow(Enemy target)
		{
			_reloadTimer = reloadTime;
			Arrow arrow = Instantiate(arrowPrefab, arrowOrigin.position, Quaternion.identity);
			arrow.Fire(arrowSpeed * (CalculateTargetPosition(target) - arrowOrigin.position).normalized);
		}

		/// <summary>
		/// Calcula a posição em que o inimigo estará no futuro, de acordo com o tempo que a flecha leva para atingí-lo.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private Vector3 CalculateTargetPosition(Enemy target)
		{
			return FirstOrderIntercept(arrowOrigin.position, Vector3.zero, arrowSpeed,
				target.ColliderPosition, target.Velocity);
		}

		/// <summary>
		/// Função para cacular onde um alvo estará quando o projétil atingi-lo.
		/// Retirada de http://wiki.unity3d.com/index.php/Calculating_Lead_For_Projectiles
		/// </summary>
		/// <param name="shooterPosition"></param>
		/// <param name="shooterVelocity"></param>
		/// <param name="shotSpeed"></param>
		/// <param name="targetPosition"></param>
		/// <param name="targetVelocity"></param>
		/// <returns></returns>
		public static Vector3 FirstOrderIntercept(Vector3 shooterPosition, Vector3 shooterVelocity, float shotSpeed,
			Vector3 targetPosition, Vector3 targetVelocity)
		{
			Vector3 targetRelativePosition = targetPosition - shooterPosition;
			Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
			float t = FirstOrderInterceptTime(shotSpeed, targetRelativePosition, targetRelativeVelocity);
			return targetPosition + t * (targetRelativeVelocity);
		}

		/// <summary>
		/// Função para cacular onde um alvo estará quando o projétil atingi-lo.
		/// Retirada de http://wiki.unity3d.com/index.php/Calculating_Lead_For_Projectiles
		/// </summary>
		/// <param name="shotSpeed"></param>
		/// <param name="targetRelativePosition"></param>
		/// <param name="targetRelativeVelocity"></param>
		/// <returns></returns>
		public static float FirstOrderInterceptTime(float shotSpeed, Vector3 targetRelativePosition,
			Vector3 targetRelativeVelocity)
		{
			float velocitySquared = targetRelativeVelocity.sqrMagnitude;
			if (velocitySquared < 0.001f)
			{
				return 0f;
			}

			float a = velocitySquared - shotSpeed * shotSpeed;

			//handle similar velocities
			if (Mathf.Abs(a) < 0.001f)
			{
				float t = -targetRelativePosition.sqrMagnitude / (2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition));
				return Mathf.Max(t, 0f); //don't shoot back in time
			}

			float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
			float c = targetRelativePosition.sqrMagnitude;
			float determinant = b * b - 4f * a * c;

			if (determinant > 0f)
			{ //determinant > 0; two intercept paths (most common)
				float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a);
				float t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
				if (t1 > 0f)
				{
					if (t2 > 0f)
					{
						return Mathf.Min(t1, t2); //both are positive
					}
					else
					{
						return t1; //only t1 is positive
					}
				}
				else
				{
					return Mathf.Max(t2, 0f); //don't shoot back in time
				}
			}
			else if (determinant < 0f) //determinant < 0; no intercept path
			{
				return 0f;
			}
			else //determinant = 0; one intercept path, pretty much never happens
			{
				return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
			}
		}
	}
}