using System;
using UnityEngine;

namespace Workshop.TowerDefense
{
	/// <summary>
	/// Guarda as configurações de uma wave de inimigos.
	/// </summary>
	[CreateAssetMenu(fileName = "NewWave", menuName = "Workshop/Tower Defense/Wave", order = 1)]
	public class Wave : ScriptableObject
	{
		public WaveEnemies[] enemies;
		public float enemyPlacementInterval;

		/// <summary>
		/// Como a classe é Serializable é posível editá-la no Editor da Unity.
		/// </summary>
		[Serializable]
		public class WaveEnemies
		{
			public int quantity;
			public Enemy enemyPrefab;
		}
	}
}