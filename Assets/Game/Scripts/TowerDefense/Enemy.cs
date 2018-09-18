using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Workshop.TowerDefense
{
	/// <summary>
	/// Classe com os comportamentos de um inimigo.
	/// Implementa o padrão máquina de estados.
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Animator))]
	public class Enemy : MonoBehaviour
	{
		private enum StateId { Initial, Moving, Attacking, Dead }

		// Exercício: Para poder criar diferentes inimigosé necessário transform alguns de seus atribtos
		// privados em públicos. Se preferir, separe-os em um ScriptableObject.
		private float _maximumSpeed = 2;
		private float _life = 100;
		private float _attackDamage = 10;
		private float _attackDistance = 1;
		private NavMeshAgent _navMeshAgent;
		private Animator _animator;
		private Gate _gate;
		private StateId _state;

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
					State = StateId.Dead;
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

		/// <summary>
		/// Posição central do collider deste inimigo.
		/// Função usada pelas torres para saber onde mirar seus disparos.
		/// </summary>
		public Vector3 ColliderPosition
		{
			get
			{
				return transform.TransformPoint(GetComponentInChildren<SphereCollider>().center);
			}
		}

		/// <summary>
		/// Velocidade de movimentação do inimigo.
		/// Colocando este atributo em uma Property evitamos que outras classes que precisem saber a
		/// velocidade do inimigo tenham que saber se ele usa Physics ou não.
		/// </summary>
		public Vector3 Velocity
		{
			get
			{
				return _navMeshAgent.velocity;
			}
		}

		private StateId State
		{
			get
			{
				return _state;
			}
			set
			{
				if(value == _state)
				{
					return;
				}
				_state = value;
				switch(_state)
				{
					case StateId.Moving:
						_navMeshAgent.Warp(transform.position);
						_navMeshAgent.isStopped = false;
						_navMeshAgent.SetDestination(_gate.transform.position);
						StopCoroutine("AttackCoroutine");
						break;
					case StateId.Attacking:
						_animator.SetFloat("Speed", 0);
						_navMeshAgent.isStopped = true;
						StartCoroutine(AttackCoroutine());
						break;
					case StateId.Dead:
						_animator.SetFloat("Speed", 0);
						_navMeshAgent.enabled = false;
						StopCoroutine("AttackCoroutine");
						break;
				}
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
			if(State == StateId.Moving)
			{
				_animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
				if ((_gate.transform.position - transform.position).magnitude <= _attackDistance)
				{
					State = StateId.Attacking;
				}
			}
			if(State == StateId.Attacking)
			{
				if((_gate.transform.position - transform.position).magnitude > _attackDistance)
				{
					State = StateId.Moving;
				}
			}
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
			_gate = gate;
			State = StateId.Moving;
		}

		/// <summary>
		/// Função chamada quando o inimigo é atingido por um disparo de uma das torres.
		/// </summary>
		/// <param name="damage"></param>
		public void Damage(float damage)
		{
			Life -= damage;
		}

		/// <summary>
		/// Deals damage at the end of the attack animation.
		/// </summary>
		/// <returns></returns>
		private IEnumerator AttackCoroutine()
		{
			_animator.SetTrigger("Attack");
			yield return new WaitForSeconds(1f);
			_gate.Damage(_attackDamage);
		}
	}
}