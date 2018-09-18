using UnityEngine;

namespace Workshop.TowerDefense
{
	/// <summary>
	/// Classe que controla o jogo. Diz quando o jogo termina e se o jogador ganhou ou perdeu.
	/// Implementa o padrão Singleton.
	/// </summary>
	public class GameController : MonoBehaviour
	{
		private enum StateId { Initial, Running, Over }

		private static GameController _singleton;

		public GameConfiguration configuration;
		public Gate gate;
		public EnemyPlacer enemyPlacer;

		private StateId _state;
		private int _money;

		/// <summary>
		/// Instância única dessa classe.
		/// </summary>
		public static GameController Instance
		{
			get
			{
				return _singleton;
			}
		}

		/// <summary>
		/// Dinheiro do jogador.
		/// </summary>
		public int Money
		{
			get
			{
				return _money;
			}
			set
			{
				_money = value;
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
				if(_state == value)
				{
					return;
				}

				_state = value;
				switch(_state)
				{
					case StateId.Running:
						enemyPlacer.enabled = true;
						break;
					case StateId.Over:
						enemyPlacer.enabled = false;
						break;
				}
			}
		}

		private void Awake()
		{
			if(_singleton != null)
			{
				Destroy(gameObject);
				return;
			}
			_singleton = this;

			_money = configuration.startingMoney;
		}

		private void Start()
		{
			State = StateId.Running;
		}

		private void Update()
		{
			if(State == StateId.Running)
			{
				if(gate.Life == 0)
				{
					Debug.Log("Você perdeu!");
					State = StateId.Over;
				}
				else if(enemyPlacer.Finished && Enemy.AllAlive.Count == 0)
				{
					Debug.Log("Parabéns! Você ganhou!");
					State = StateId.Over;
				}
			}
		}

		private void OnDestroy()
		{
			if(_singleton == this)
			{
				_singleton = null;
			}
		}
	}
}