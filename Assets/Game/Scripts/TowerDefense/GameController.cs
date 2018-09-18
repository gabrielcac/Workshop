using UnityEngine;

namespace Workshop.TowerDefense
{
	/// <summary>
	/// Classe que controla o jogo. Diz quando o jogo termina e se o jogador ganhou ou perdeu.
	/// Implementa o padrão Singleton.
	/// </summary>
	public class GameController : MonoBehaviour
	{
		private static GameController _singleton;

		public GameConfiguration configuration;

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

		private void OnDestroy()
		{
			if(_singleton == this)
			{
				_singleton = null;
			}
		}
	}
}