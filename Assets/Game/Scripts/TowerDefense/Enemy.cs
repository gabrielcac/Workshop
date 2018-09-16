using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Classe com os comportamentos de um inimigo.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
	private float _maximumSpeed = 2;
	private float _life = 100;
	private NavMeshAgent _navMeshAgent;
	private Animator _animator;

	public float Life
	{
		get
		{
			return _life;
		}
		set
		{
			_life = Mathf.Max(value, 0);

			// Se o dano é suficiente para matá-lo, toca a animação de morte
			// Se não, toca a animação de dano.
			if (_life == 0)
			{
				_animator.SetTrigger("Die");
				_navMeshAgent.enabled = false;
				// Exercício: Usar Coroutine para destruir o corpo morto do inimigo após alguns segundos.
			}
			else
			{
				_animator.SetTrigger("Damage");
				// Exercício: Usar Coroutine para parar a movimentação do inimigo por alguns segundos quando atingido.
			}
		}
	}

	public Vector3 ColliderPosition
	{
		get
		{
			return transform.TransformPoint(GetComponentInChildren<SphereCollider>().center);
		}
	}

	public Vector3 Velocity
	{
		get
		{
			return _navMeshAgent.velocity;
		}
	}

	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_navMeshAgent.speed = _maximumSpeed;

		_animator = GetComponent<Animator>();
	}
	
	private void Update()
	{
		_animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
	}

	private void OnDisable()
	{
		if (_navMeshAgent.isOnNavMesh)
		{
			_navMeshAgent.isStopped = true;
		}
	}

	/// <summary>
	/// Sets the <see cref="Gate"/> where this enemy is trying to go.
	/// </summary>
	/// <param name="gate"></param>
	public void SetTarget(Gate gate)
	{
		_navMeshAgent.Warp(transform.position);
		_navMeshAgent.isStopped = false;
		_navMeshAgent.SetDestination(gate.transform.position);
	}

	/// <summary>
	/// Função chamada quando o inimigo é atingido por um disparo de uma das torres.
	/// </summary>
	/// <param name="damage"></param>
	public void Damage(float damage)
	{
		Life -= damage;
	}
}