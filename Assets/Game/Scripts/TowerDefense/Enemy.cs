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
		_life -= damage;

		// Se o dano é suficiente para matá-lo, toca a animação de morte
		// Se não, toca a animação de dano.
		if(_life <= 0)
		{
			_animator.SetTrigger("Die");
			// Exercício: Usar Coroutine para destruir o corpo morto do inimigo após alguns segundos.
		}
		else
		{
			_animator.SetTrigger("Damage");
			// Exercício: Usar Coroutine para parar a movimentação do inimigo por alguns segundos quando atingido.
		}
	}
}